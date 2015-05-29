using Assets.Scripts;
using UnityEngine;

public class Pacman : MonoBehaviour
{
	private Transform _transform;
	private int _xPosition;
	private int _yPosition;
	private char[,] _map;
	private State _currentState = State.Stationary;
	private const float _SPEED = 0.05f;

	private void Start()
	{
		_transform = GetComponent<Transform>();
		_xPosition = MapGenerator.Instance.PacmanX;
		_yPosition = MapGenerator.Instance.PacmanY;
		_map = MapGenerator.Instance.Map;
		//		Debug.Log("y" + MapGenerator.Instance.PacmanY);
		//		Debug.Log("x" + MapGenerator.Instance.PacmanX);
	}

	private enum State
	{
		Up,
		Down,
		Left,
		Right,
		Stationary
	}

	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_transform.rotation = Quaternion.Euler(0, 0, 90);
			_transform.localScale = new Vector3(1, 1, 1);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			_transform.rotation = Quaternion.Euler(0, 0, -90);
			_transform.localScale = new Vector3(1, 1, 1);
		}
		else if (Input.GetKey(KeyCode.RightArrow) && (_currentState == State.Stationary || _currentState == State.Left))
		{
			_transform.rotation = Quaternion.Euler(0, 0, 0);
			_transform.localScale = new Vector3(1, 1, 1);
			if (_map[_xPosition + 1, _yPosition] == ' ')
			{
				_currentState = State.Right;
			}
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && (_currentState == State.Stationary || _currentState == State.Right))
		{
			_transform.rotation = Quaternion.Euler(0, 0, 0);
			_transform.localScale = new Vector3(-1, 1, 1);
			if (_map[_xPosition - 1, _yPosition] == ' ')
			{
				_currentState = State.Left;
			}
		}

		switch (_currentState)
		{
			case State.Left:
				_transform.AddX(-_SPEED);
				break;
			case State.Right:
				_transform.AddX(_SPEED);
				break;
		}
	}
}