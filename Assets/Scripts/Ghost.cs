using System;
using Assets.Scripts;
using UnityEngine;
using Random = System.Random;

public class Ghost : MonoBehaviour
{
	private Transform _transform;
	private int _xPosition;
	private int _yPosition;
	private char[,] _map;
	private State _currentState = State.Stationary;
	private const float _SPEED = 0.05f;
	private const float _TOLERANCE = 0.01f;
	private SimulateKey _simulateKey;
	private static int _ghostCounter;
	private Random _rnd = new Random();

	private void Start()
	{
		_transform = GetComponent<Transform>();
		_map = MapGenerator.Instance.Map;
		_rnd = new Random(++_ghostCounter*((int) DateTime.Now.Ticks & 0x0000FFFF));
	}

	private enum State
	{
		Up,
		Down,
		Left,
		Right,
		Stationary
	}

	private enum SimulateKey
	{
		Up,
		Down,
		Left,
		Right
	}

	private void FixedUpdate()
	{
		float modX = Mathf.Abs(_transform.X()%1);
		float modY = Mathf.Abs(_transform.Y()%1);
		if ((modX < _TOLERANCE || modX > 1 - _TOLERANCE) && (modY < _TOLERANCE || modY > 1 - _TOLERANCE))
		{
			_xPosition = Convert.ToInt32(_transform.X() + 8);
			_yPosition = Convert.ToInt32(_transform.Y() + 8);
			_currentState = State.Stationary;
			_simulateKey = (SimulateKey) _rnd.Next(0, 4);
		}
		if (_simulateKey == SimulateKey.Up && (_currentState == State.Stationary || _currentState == State.Down))
		{
			_transform.localScale = new Vector3(1, 1, 1);
			if (_map[_xPosition, _yPosition + 1] == ' ')
			{
				_currentState = State.Up;
			}
		}
		else if (_simulateKey == SimulateKey.Down && (_currentState == State.Stationary || _currentState == State.Up))
		{
			_transform.localScale = new Vector3(1, 1, 1);
			if (_map[_xPosition, _yPosition - 1] == ' ')
			{
				_currentState = State.Down;
			}
		}
		else if (_simulateKey == SimulateKey.Left && (_currentState == State.Stationary || _currentState == State.Right))
		{
			_transform.localScale = new Vector3(-1, 1, 1);
			if (_map[_xPosition - 1, _yPosition] == ' ')
			{
				_currentState = State.Left;
			}
		}
		else if (_simulateKey == SimulateKey.Right && (_currentState == State.Stationary || _currentState == State.Left))
		{
			_transform.localScale = new Vector3(1, 1, 1);
			if (_map[_xPosition + 1, _yPosition] == ' ')
			{
				_currentState = State.Right;
			}
		}

		switch (_currentState)
		{
			case State.Up:
				_transform.AddY(_SPEED);
				break;
			case State.Down:
				_transform.AddY(-_SPEED);
				break;
			case State.Left:
				_transform.AddX(-_SPEED);
				break;
			case State.Right:
				_transform.AddX(_SPEED);
				break;
		}
	}
}