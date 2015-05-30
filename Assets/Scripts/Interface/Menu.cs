using Assets.Scripts.Helpers;
using Assets.Scripts.Managed;
using UnityEngine;

namespace Assets.Scripts.Interface
{
	/// <summary>
	///     Предоставляет методы для главного меню
	/// </summary>
	public class Menu : MonoBehaviour
	{
		/// <summary>
		///     выход из игры
		/// </summary>
		public void ExitGame()
		{
			Application.Quit();
		}

		/// <summary>
		///     Загрузка уровня
		/// </summary>
		/// <param name="levelName"></param>
		public void LoadLevel(string levelName)
		{
			Application.LoadLevel(levelName);
		}

		/// <summary>
		///     Загрузка сцены с геймплеем
		/// </summary>
		/// <param name="savedState"></param>
		public void LoadGame(bool savedState)
		{
			Application.LoadLevel("Game");
			Data.BOOL.SavedState.Set(savedState);
		}

		/// <summary>
		///     Сброс очков
		/// </summary>
		public void ResetScores()
		{
			ScoreManager.ResetScores();
			Application.LoadLevel("Scores");
		}
	}
}