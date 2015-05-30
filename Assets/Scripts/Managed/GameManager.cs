using System;
using UnityEngine;

namespace Assets.Scripts.Managed
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		public AudioClip ExplosionAudioClip;

		private void Start()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				throw new Exception("Two or more Game Manager!");
			}
		}

		/// <summary>
		/// Произошел взрыв
		/// </summary>
		public void PlayExplosion()
		{
			GetComponent<AudioSource>().PlayOneShot(ExplosionAudioClip);
		}

		/// <summary>
		/// Игра окончена
		/// </summary>
		public static void GameOver()
		{
			Data.JSON.MapState.Set("");
			ScoreManager.Instance.PublishScore();
			Application.LoadLevel("Scores");
		}
	}
}