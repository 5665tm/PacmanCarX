using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

public class ShowValueFromKey : MonoBehaviour
{
	public Data.NUM KeyScore;
	public string Intro;

	private void Start()
	{
		GetComponent<Text>().text = Intro + KeyScore.Get();
	}

}