using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity3dAzure.MobileServices;
using RestSharp;

[CLSCompliant(false)]
public class PostGameScript : MonoBehaviour
{
    public Text playerText;
    public Text scoreText;
    public InputField nameField;
    public GameObject namePanel;
    public Button btnOk;

    [SerializeField]
    private string appUrl;
    [SerializeField]
    private string appKey;
    private MobileServiceClient client;
    private MobileServiceTable<Highscore> table;
    private int score;
    private List<Highscore> scores = new List<Highscore>();
    private PregameMenuSingle pgms;
    private bool high = false; // Is the user score a high score
    private bool written = false; // Has the high scores been written

    public void Start()
    {
        setCredentials();
        namePanel.SetActive(false);
        scoreText.transform.localPosition = new Vector2(0, 140);
        pgms = gameObject.AddComponent<PregameMenuSingle>();
        client = new MobileServiceClient(appUrl, appKey);
        table = client.GetTable<Highscore>("Highscores");
        score = pgms.getScore();
        playerText.text = "Your score: " + score;
        QueryItemsByScore();
    }

    public void FixedUpdate()
    {
        if (scores.Count != 0 && !written)
        {
            scoreText.text = "";
            foreach (Highscore item in scores)
                scoreText.text += item.username + " - " + item.score + "\n";
            if (high)
            {
                namePanel.SetActive(true);
                scoreText.transform.localPosition = new Vector2(0, 0);
            }
            written = true;
        }
    }

    private void setCredentials()
    {
        Constants constants = new Constants();
        appUrl = constants.getAppUrl();
        appKey = constants.getAppKey();
    }

    private void QueryItemsByScore()
    {
        string filter = string.Format("score gt {0}", 0);
        string orderBy = "score desc";
        CustomQuery query = new CustomQuery(filter, orderBy);
        QueryItems(query);
    }

    private void QueryItems(CustomQuery query)
    {
        table.Query<Highscore>(query, OnReadItemsCompleted);
    }

    private void OnReadItemsCompleted(IRestResponse<List<Highscore>> response)
    {
        scores = response.Data;
        if (scores[scores.Count - 1].score < score)
            high = true;
    }

    private IEnumerator tryUpload(string name, int score)
    {
        while (scores.Count == 0)
            yield return new WaitForSeconds(2.0f);

        Highscore newHs = new Highscore();
        newHs.score = score;
        newHs.username = name;

        if (score > scores[scores.Count - 1].score)
        {
            table.Insert(newHs);
            if(scores.Count >= 10)
            {
                table.Delete<Highscore>(scores[scores.Count - 1].id);
            }
        }
    }

    public void btnOkPressed()
    {
        StartCoroutine(tryUpload(nameField.text, score));
    }

    public void btnCancelPressed()
    {
        namePanel.SetActive(false);
        scoreText.transform.localPosition = new Vector2(0, 140);
    }

    public void onInputFieldChanged()
    {
        if (nameField.text != null)
            btnOk.interactable = true;
        else
            btnOk.interactable = false;
    }

    public void btnToMenuPressed()
    {
        Application.LoadLevel(0);
    }
}
