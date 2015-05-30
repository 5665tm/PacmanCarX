using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void LoadLevel(string levelName)
	{
		Application.LoadLevel(levelName);
	}

	public void ResetScores()
	{
		ScoreManager.ResetScores();
		Application.LoadLevel("Scores");
	}
}
