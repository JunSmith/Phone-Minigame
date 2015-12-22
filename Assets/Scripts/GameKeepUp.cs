using UnityEngine;
using System;
using System.Collections;

[CLSCompliant(false)]
public class GameKeepUp : MonoBehaviour, IMinigame{
    public UnityEngine.UI.Slider timeSlider;
    public GameObject target;

    private PregameMenuSingle pgms;
    private float missionTime = 4.0f;
    private float startTime;
    private float stopTime;
    private bool hasLost = false;
    private int scrVert = 6, scrHori = 8;

    public void Start()
    {
        pgms = new PregameMenuSingle();
        //pgms = gameObject.AddComponent<PregameMenuSingle>();
        setTimeBar();
        StartCoroutine(spawnObjects());
    }

    public void FixedUpdate()
    {
        if(!hasLost)
            updateSlider();
    }

    public void setTimeBar()
    {
        timeSlider.maxValue = missionTime;
        startTime = Time.time;
    }

    public void updateSlider()
    {
        timeSlider.value = (Time.time - startTime);
        if (timeSlider.value >= missionTime)
            win();
    }

    public void stopTimer()
    {
        stopTime = Time.time;
        stopTime = missionTime - (stopTime - startTime);
    }

    public void lose()
    {
        stopTimer();

        Application.LoadLevel(pgms.getPostLevel());
    }

    public void win()
    {
        pgms.addScore(100);
        Application.LoadLevel(UnityEngine.Random.Range(pgms.getMinLevel(), pgms.getMaxLevel()));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        hasLost = true;
        lose();
    }

    public IEnumerator spawnObjects()
    {
        while (!hasLost)
        {
            Instantiate(target, new Vector3(UnityEngine.Random.Range(-scrHori, scrHori), scrVert, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        lose();
    }
}
