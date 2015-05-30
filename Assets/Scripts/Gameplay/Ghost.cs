using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.Managed;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Gameplay
{
	/// <summary>
	///     Симулирует поведение приведения
	/// </summary>
	public class Ghost : PersonAbstract
	{
		/// <summary>
		///     Привидения симулируют нажатия на клавиши
		/// </summary>
		private SimulateKey _simulateKey;

		/// <summary>
		///     Привидения тупые, увы
		/// </summary>
		private Random _rnd = new Random();

		/// <summary>
		///     Устраиваем разный рандом для разных привидений
		/// </summary>
		private static int _ghostCounter;

		private void Start()
		{
			Init();
			_rnd = new Random(++_ghostCounter*((int) DateTime.Now.Ticks & 0x0000FFFF));
		}

		/// <summary>
		/// Привидения симулируют нажатия клавиш
		/// </summary>
		private enum SimulateKey
		{
			Up,
			Down,
			Left,
			Right
		}

		private void FixedUpdate()
		{
			CheckCoordinatesStationary();
			if (CurrentState == State.Stationary)
			{
				_simulateKey = (SimulateKey) _rnd.Next(0, 4);
			}
			// вверх
			if (_simulateKey == SimulateKey.Up && (CurrentState == State.Stationary || CurrentState == State.Down))
			{
				PersonTransform.localScale = new Vector3(1, 1, 1);
				if (MapManager.Map[XPosition, YPosition + 1] == ' ')
				{
					CurrentState = State.Up;
				}
			}
			// вниз
			else if (_simulateKey == SimulateKey.Down && (CurrentState == State.Stationary || CurrentState == State.Up))
			{
				PersonTransform.localScale = new Vector3(1, 1, 1);
				if (MapManager.Map[XPosition, YPosition - 1] == ' ')
				{
					CurrentState = State.Down;
				}
			}
			// налево
			else if (_simulateKey == SimulateKey.Left && (CurrentState == State.Stationary || CurrentState == State.Right))
			{
				PersonTransform.localScale = new Vector3(-1, 1, 1);
				if (MapManager.Map[XPosition - 1, YPosition] == ' ')
				{
					CurrentState = State.Left;
				}
			}
			// направо
			else if (_simulateKey == SimulateKey.Right && (CurrentState == State.Stationary || CurrentState == State.Left))
			{
				PersonTransform.localScale = new Vector3(1, 1, 1);
				if (MapManager.Map[XPosition + 1, YPosition] == ' ')
				{
					CurrentState = State.Right;
				}
			}

			switch (CurrentState)
			{
				case State.Up:
					PersonTransform.AddY(SPEED);
					break;
				case State.Down:
					PersonTransform.AddY(-SPEED);
					break;
				case State.Left:
					PersonTransform.AddX(-SPEED);
					break;
				case State.Right:
					PersonTransform.AddX(SPEED);
					break;
			}
		}
	}
}