using UnityEngine;
using UnityEngine.UI;

public class ShowValueFromKey : MonoBehaviour
{
	public Data.NUM KeyScore;

	private void Start()
	{
		GetComponent<Text>().text = KeyScore.Get().ToString();
	}

}