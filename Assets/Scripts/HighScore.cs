using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Unity3dAzure.MobileServices;
using RestSharp;

[CLSCompliant(false)]
public class HighScore : MonoBehaviour
{
    public Text display;

    [SerializeField]
    private string appUrl;
    [SerializeField]
    private string appKey;
    private MobileServiceClient client;
    private MobileServiceTable<Highscore> table;
    private Highscore score;
    private List<Highscore> scores = new List<Highscore>();
    private bool isLoaded = false;

    public void Start ()
    {
        setCredentials();
        client = new MobileServiceClient(appUrl, appKey);
        table = client.GetTable<Highscore>("Highscores");
        QueryItemsByScore();
        
	}

    public void Update()
    {
        if (scores.Count != 0 && !isLoaded)
        {
            display.text = "";

            foreach (Highscore item in scores)
            {


                display.text += item.username + " - " + item.score + "\n";
                isLoaded = true;
            }
        }
        else if (scores.Count == 0 && !isLoaded)
            display.text = "Fetching scores from the cloud...";
    }

    public void onMenuButtonClick()
    {

        Application.LoadLevel(0);
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

    private void OnLookupCompleted(IRestResponse<Highscore> response)
    {
        Debug.Log(response.Content);
    }

    private void OnReadItemsCompleted(IRestResponse<List<Highscore>> response)
    {
        scores = response.Data;
    }
}
