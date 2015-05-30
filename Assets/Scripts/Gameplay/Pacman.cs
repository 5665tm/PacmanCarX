using Assets.Scripts.Camera;
using Assets.Scripts.Helpers;
using Assets.Scripts.Managed;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
	/// <summary>
	///     Главный герой во всей красе
	/// </summary>
	public class Pacman : PersonAbstract
	{
		/// <summary>
		///     Игровой объект пакменна хиппи
		/// </summary>
		public GameObject PacmanNormalAnimation;

		/// <summary>
		///     Игровой объект пакмена убийцы
		/// </summary>
		public GameObject PacmanVampireAnimation;

		/// <summary>
		///     Пакмен любит устраивать взрывы
		/// </summary>
		public GameObject Explosion;

		/// <summary>
		///     Кривая цветового восприятия пакмена
		/// </summary>
		public AnimationCurve AnimateColor;

		/// <summary>
		///     В какой момент времени начался приступ агрессии
		/// </summary>
		private float _vampireModeStartTime = -4f;

		/// <summary>
		///     Длительность приступа агрессии
		/// </summary>
		private const float _VAMPRIRE_MODE_DURATION = 5f;

		private AudioSource _audioSource;
		private CameraShake _cameraShake;

		private void Start()
		{
			Init();
			_cameraShake = UnityEngine.Camera.main.GetComponent<CameraShake>();
			_audioSource = GetComponent<AudioSource>();
		}

		private void FixedUpdate()
		{
			CheckCoordinatesStationary();
			// вверх
			if (Input.GetKey(KeyCode.UpArrow) && (CurrentState == State.Stationary || CurrentState == State.Down))
			{
				PersonTransform.rotation = Quaternion.Euler(0, 0, 90);
				PersonTransform.localScale = new Vector3(1, 1, 1);
				if (MapManager.Map[XPosition, YPosition + 1] == ' ')
				{
					CurrentState = State.Up;
				}
			}
			// вниз
			else if (Input.GetKey(KeyCode.DownArrow) && (CurrentState == State.Stationary || CurrentState == State.Up))
			{
				PersonTransform.rotation = Quaternion.Euler(0, 0, -90);
				PersonTransform.localScale = new Vector3(1, 1, 1);
				if (MapManager.Map[XPosition, YPosition - 1] == ' ')
				{
					CurrentState = State.Down;
				}
			}
			// налево
			else if (Input.GetKey(KeyCode.LeftArrow) && (CurrentState == State.Stationary || CurrentState == State.Right))
			{
				PersonTransform.rotation = Quaternion.Euler(0, 0, 0);
				PersonTransform.localScale = new Vector3(-1, 1, 1);
				if (MapManager.Map[XPosition - 1, YPosition] == ' ')
				{
					CurrentState = State.Left;
				}
			}
			// направо
			else if (Input.GetKey(KeyCode.RightArrow) && (CurrentState == State.Stationary || CurrentState == State.Left))
			{
				PersonTransform.rotation = Quaternion.Euler(0, 0, 0);
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
					_audioSource.volume = Data.BOOL.Sound.Is() ? 1 : 0;
					break;
				case State.Down:
					PersonTransform.AddY(-SPEED);
					_audioSource.volume = Data.BOOL.Sound.Is() ? 1 : 0;
					break;
				case State.Left:
					PersonTransform.AddX(-SPEED);
					_audioSource.volume = Data.BOOL.Sound.Is() ? 1 : 0;
					break;
				case State.Right:
					PersonTransform.AddX(SPEED);
					_audioSource.volume = Data.BOOL.Sound.Is() ? 1 : 0;
					break;
				case State.Stationary:
					_audioSource.volume = 0;
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

		/// <summary>
		///     Игрок столкнулся с чем то
		/// </summary>
		/// <param name="coll2D"></param>
		public void OnTriggerEnter2D(Collider2D coll2D)
		{
			// Это  шар
			if (coll2D.tag == "Ball")
			{
				ScoreManager.Instance.AddScore(5);
				Destroy(coll2D.gameObject);
				MapManager.CounterBall--;
			}
			// Это огненный шар
			else if (coll2D.tag == "BallPower")
			{
				PacmanNormalAnimation.SetActive(false);
				PacmanVampireAnimation.SetActive(true);
				ScoreManager.Instance.AddScore(20);
				Destroy(coll2D.gameObject);
				_vampireModeStartTime = Time.timeSinceLevelLoad;
				MapManager.CounterBall--;
			}
			// Это привидение
			else if (coll2D.tag == "Ghost")
			{
				// Взрываем привидение, если мы вампир
				if (Time.timeSinceLevelLoad - _vampireModeStartTime < _VAMPRIRE_MODE_DURATION)
				{
					GameObject go = Instantiate(Explosion, coll2D.transform.position, coll2D.transform.rotation) as GameObject;
					Destroy(go, 0.6f);
					if (Data.BOOL.Sound.Is())
					{
						GameManager.Instance.PlayExplosion();
					}
					StartCoroutine(_cameraShake.Shake(0.5f, 0.6f));
					Destroy(coll2D.gameObject);
					ScoreManager.Instance.AddScore(80);
				}
				// В ином случае погибаем сами
				else
				{
					GameManager.GameOver();
				}
			}
			// Проверяем не закончились ли шары на карте
			Debug.Log(MapManager.CounterBall);
			if (MapManager.CounterBall <= 0)
			{
				GameManager.GameOver();
			}
		}
	}
}