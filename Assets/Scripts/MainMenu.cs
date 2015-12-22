using UnityEngine;
using System;

[CLSCompliant(false)]
public class MainMenu : MonoBehaviour {

	public void btnQuitClick()
	{
		Application.Quit();
	}

	public void btnPlayClick()
	{
		Application.LoadLevel(2);
	}

    public void btnHighScoresClick()
    {
        Application.LoadLevel(1);
    }
}
