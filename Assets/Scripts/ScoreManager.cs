using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;
	private int _score;
	private TextMesh _textMesh;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			throw new Exception("Two or more Map Generator!");
		}
		_textMesh = GetComponent<TextMesh>();
	}

	public void AddScore(int numberOfScore)
	{
		_score += numberOfScore;
		_textMesh.text = "Score\n" + _score;
	}

	public void PublishScore()
	{
		Data.NUM.ScoreLast.Set(_score);

		if (Data.NUM.ScoreLast.Get() > Data.NUM.Score1St.Get())
		{
			Data.NUM.Score3Rd.Set(Data.NUM.Score2Nd.Get());
			Data.NUM.Score2Nd.Set(Data.NUM.Score1St.Get());
			Data.NUM.Score1St.Set(Data.NUM.ScoreLast.Get());
		}
		else if (Data.NUM.ScoreLast.Get() > Data.NUM.Score2Nd.Get())
		{
			Data.NUM.Score3Rd.Set(Data.NUM.Score2Nd.Get());
			Data.NUM.Score2Nd.Set(Data.NUM.ScoreLast.Get());
		}
		else if (Data.NUM.ScoreLast.Get() > Data.NUM.Score3Rd.Get())
		{
			Data.NUM.Score3Rd.Set(Data.NUM.ScoreLast.Get());
		}
	}

	public static void ResetScores()
	{
		Data.NUM.Score3Rd.Set(0);
		Data.NUM.Score2Nd.Set(0);
		Data.NUM.Score1St.Set(0);
		Data.NUM.ScoreLast.Set(0);
	}
}