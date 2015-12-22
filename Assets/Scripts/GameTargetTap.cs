using System;
using UnityEngine;

[CLSCompliant(false)]
public class GameTargetTap : MonoBehaviour, IMinigame
{
	public GameObject blackSquare, whiteSquare;
    public UnityEngine.UI.Slider timeSlider;

    PregameMenuSingle pgms;
    private int maxSpawns = 5;
    private int scrVert = 3;
    private int scrHori = 7;
    private float missionTime = 3.0f;
    private float stopTime;
    private float startTime;

    public void Start()
    {
        pgms = new PregameMenuSingle();
        //pgms = gameObject.AddComponent<PregameMenuSingle>();
        setTimeBar();
        spawnTargets(maxSpawns);
    }

    public void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Dud").Length == maxSpawns)
        {
            if (GameObject.FindGameObjectsWithTag("Target").Length == 0)            
                win();            
            else
                updateSlider();

        }

        else
            lose();
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
        {
            lose();
        }
    }

    public void stopTimer()
    {
        stopTime = Time.time;
        stopTime = missionTime - (stopTime - startTime);
    }

    public void win()
    {
        int level = UnityEngine.Random.Range(pgms.getMinLevel(), pgms.getMaxLevel());
        Debug.Log("Score: " + pgms.getScore());
        pgms.addScore(100);
        Application.LoadLevel(level);
    }

    public void lose()
    {
        Application.LoadLevel(pgms.getPostLevel());
    }

    public void spawnTargets(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(blackSquare, new Vector3(UnityEngine.Random.Range(-scrHori, scrHori), UnityEngine.Random.Range(-scrVert, scrVert), 0), Quaternion.identity);
    		Instantiate(whiteSquare, new Vector3(UnityEngine.Random.Range(-scrHori, scrHori), UnityEngine.Random.Range(-scrVert, scrVert), 0), Quaternion.identity);
        }
    }
}
