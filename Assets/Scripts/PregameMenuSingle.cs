using UnityEngine;
using UnityEngine.UI;
using System;

[CLSCompliant(false)]
public class PregameMenuSingle : MonoBehaviour
{
    public Dropdown drpDifficulty;
    public Button btnReady;
    private static int score;
    private int min = 4, max = 6, post = 3;

	void Start ()
    {
        score = 0;
        //drpDifficulty.value = 0;
    }

    public void onReadyPress()
    {
        Application.LoadLevel(UnityEngine.Random.Range(min, max));
    }

    public void onCancelButtonPress()
    {
        Application.LoadLevel(0);   
    }

    public void setScore(int newScore)
    {
        score = newScore;
    }

    public int getScore()
    {
        return score;
    }

    public void addScore(int addition)
    {
        score += addition;
    }

    public int getMinLevel()
    {
        return min;
    }

    public int getMaxLevel()
    {
        return max;
    }

    public int getPostLevel()
    {
        return post;
    }
}
