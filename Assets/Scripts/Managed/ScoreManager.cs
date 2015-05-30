using System;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Managed
{
	public class ScoreManager : MonoBehaviour
	{
		public static ScoreManager Instance { get; private set; }

		/// <summary>
		///     Число очкно без учета времени
		/// </summary>
		private int _scoreWithoutTheTime;

		/// <summary>
		///     Число очков
		/// </summary>
		public int Score { get; private set; }

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

		public void Update()
		{
			Score = _scoreWithoutTheTime - (int) Time.timeSinceLevelLoad;
			_textMesh.text = "Score\n" + Score;
		}

		/// <summary>
		///     Добавляет число очков к имеющимся
		/// </summary>
		/// <param name="numberOfScore"></param>
		public void AddScore(int numberOfScore)
		{
			_scoreWithoutTheTime += numberOfScore;
		}

		/// <summary>
		///     Публикует заработанные очки в игровых данных
		/// </summary>
		public void PublishScore()
		{
			Data.NUM.ScoreLast.Set(Score);

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

		/// <summary>
		///     Выполняет сброс очков
		/// </summary>
		public static void ResetScores()
		{
			Data.NUM.Score3Rd.Set(0);
			Data.NUM.Score2Nd.Set(0);
			Data.NUM.Score1St.Set(0);
			Data.NUM.ScoreLast.Set(0);
		}
	}
}