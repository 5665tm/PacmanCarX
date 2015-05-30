using System;
using Assets.Scripts;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pacman : MonoBehaviour
{
	private Transform _transform;
	public GameObject PacmanNormalAnimation;
	public GameObject PacmanVampireAnimation;
	public GameObject Explosion;
	private int _xPosition;
	private int _yPosition;
	private char[,] _map;
	private State _currentState = State.Stationary;
	private const float _SPEED = 0.05f;
	private const float _TOLERANCE = 0.01f;
	public AnimationCurve AnimateColor;
	private float _vampireModeStartTime = -4f;
	private CameraShake _cameraShake;
	private const float _VAMPRIRE_MODE_DURATION = 5f;

	private void Start()
	{
		_transform = GetComponent<Transform>();
		_map = MapGenerator.Instance.Map;
		_cameraShake = Camera.main.GetComponent<CameraShake>();
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
		float modX = Mathf.Abs(_transform.X()%1);
		float modY = Mathf.Abs(_transform.Y()%1);
		if ((modX < _TOLERANCE || modX > 1 - _TOLERANCE) && (modY < _TOLERANCE || modY > 1 - _TOLERANCE))
		{
			_xPosition = Convert.ToInt32(_transform.X() + 8);
			_yPosition = Convert.ToInt32(_transform.Y() + 8);
			_currentState = State.Stationary;
		}
		if (Input.GetKey(KeyCode.UpArrow) && (_currentState == State.Stationary || _currentState == State.Down))
		{
			_transform.rotation = Quaternion.Euler(0, 0, 90);
			_transform.localScale = new Vector3(1, 1, 1);
			if (_map[_xPosition, _yPosition + 1] == ' ')
			{
				_currentState = State.Up;
			}
		}
		else if (Input.GetKey(KeyCode.DownArrow) && (_currentState == State.Stationary || _currentState == State.Up))
		{
			_transform.rotation = Quaternion.Euler(0, 0, -90);
			_transform.localScale = new Vector3(1, 1, 1);
			if (_map[_xPosition, _yPosition - 1] == ' ')
			{
				_currentState = State.Down;
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
		else if (Input.GetKey(KeyCode.RightArrow) && (_currentState == State.Stationary || _currentState == State.Left))
		{
			_transform.rotation = Quaternion.Euler(0, 0, 0);
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
		if (Time.timeSinceLevelLoad - _vampireModeStartTime > _VAMPRIRE_MODE_DURATION)
		{
			PacmanNormalAnimation.SetActive(true);
			PacmanVampireAnimation.SetActive(false);
		}
		else
		{
			RenderSettings.ambientLight = new Color(1, AnimateColor.Evaluate(Time.timeSinceLevelLoad - _vampireModeStartTime), AnimateColor.Evaluate(Time.timeSinceLevelLoad - _vampireModeStartTime));
		}
	}

	public void OnTriggerEnter2D(Collider2D coll2D)
	{
		if (coll2D.tag == "Ball")
		{
			ScoreManager.Instance.AddScore(5);
			Destroy(coll2D.gameObject);
			MapGenerator.Instance.CounterBall--;
		}
		else if (coll2D.tag == "BallPower")
		{
			PacmanNormalAnimation.SetActive(false);
			PacmanVampireAnimation.SetActive(true);
			ScoreManager.Instance.AddScore(20);
			Destroy(coll2D.gameObject);
			_vampireModeStartTime = Time.timeSinceLevelLoad;
			MapGenerator.Instance.CounterBall--;
		}
		else if (coll2D.tag == "Ghost")
		{
			if (Time.timeSinceLevelLoad - _vampireModeStartTime < _VAMPRIRE_MODE_DURATION)
			{
				GameObject go = Instantiate(Explosion, coll2D.transform.position, coll2D.transform.rotation) as GameObject;
				Destroy(go, 0.6f);
				StartCoroutine(_cameraShake.Shake(0.5f, 0.6f));
				Destroy(coll2D.gameObject);
			}
			else
			{
				GameOver();
			}
		}
		if (MapGenerator.Instance.CounterBall <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		ScoreManager.Instance.PublishScore();
		Application.LoadLevel("Scores");
	}
}