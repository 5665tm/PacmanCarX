using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.Managed;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
	/// <summary>
	///     Предсталвяет собой незавершенную реализацию персонажа
	/// </summary>
	public abstract class PersonAbstract : MonoBehaviour
	{
		/// <summary>
		///     Состояние движения персонажа
		/// </summary>
		protected enum State
		{
			Up,
			Down,
			Left,
			Right,
			Stationary
		}

		/// <summary>
		///     Скорость персонажей
		/// </summary>
		protected const float SPEED = 0.05f;

		/// <summary>
		///     Transform персонажа
		/// </summary>
		protected Transform PersonTransform;

		protected int XPosition;
		protected int YPosition;
		protected State CurrentState = State.Stationary;

		/// <summary>
		///     Допустимая погрешность при расчетах
		/// </summary>
		private const float _TOLERANCE = 0.01f;

		/// <summary>
		///     Проводит начальную инициализацию персонажа
		/// </summary>
		protected void Init()
		{
			PersonTransform = GetComponent<Transform>();
		}

		/// <summary>
		///     Проверяет координаты на стационарность
		/// </summary>
		protected void CheckCoordinatesStationary()
		{
			float modX = Mathf.Abs(PersonTransform.X()%1);
			float modY = Mathf.Abs(PersonTransform.Y()%1);
			if ((modX < _TOLERANCE || modX > 1 - _TOLERANCE) && (modY < _TOLERANCE || modY > 1 - _TOLERANCE))
			{
				XPosition = Convert.ToInt32(PersonTransform.X() + MapManager.MAP_OFFSET);
				YPosition = Convert.ToInt32(PersonTransform.Y() + MapManager.MAP_OFFSET);
				CurrentState = State.Stationary;
			}
		}
	}
}