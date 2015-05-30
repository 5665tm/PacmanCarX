// Last Change: 2015 01 24 21:02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Managed
{
	public partial class MapManager : MonoBehaviour
	{
		public GameObject[] Persons;
		public GameObject[] MapPersons { get; private set; }
		public List<GameObject> Balls { get; private set; }
		public List<GameObject> PowerBalls { get; private set; }

		public static MapManager Instance { get; private set; }
		public GameObject Wall;
		public GameObject Ball;
		public GameObject BallPower;
		public static int CounterBall;
		public static char[,] Map { get; private set; }
		private static readonly Random _rnd = new Random();
		private const int _MAP_SIZE = 17;
		public const int MAP_OFFSET = 8;

		private void Awake()
		{
			Balls = new List<GameObject>();
			PowerBalls = new List<GameObject>();
			MapPersons = new GameObject[Persons.Length];
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				throw new Exception("Two or more Map Manager!");
			}

			if (Data.BOOL.SavedState.Is() && Data.JSON.MapState.Get() != "")
			{
				RestoreMap();
			}
			else
			{
				NewMap();
			}
		}

		/// <summary>
		///     Сохраняет текущее состояние игрового мира
		/// </summary>
		public static void SaveState()
		{
			var jsonMapState = new JSONObject();
			StringBuilder sb = new StringBuilder();
			foreach (var ch in Map)
			{
				sb.Append(ch);
			}
			jsonMapState.AddField("Map", sb.ToString());

			jsonMapState.AddField("Score", ScoreManager.Instance.Score);

			for (int index = 0; index < Instance.MapPersons.Length; index++)
			{
				var go = Instance.MapPersons[index];
				if (go != null)
				{
					jsonMapState.AddField("Person" + index,
						Convert.ToInt32(go.transform.position.x) + ":" +
							Convert.ToInt32(go.transform.position.y));
				}
				else
				{
					jsonMapState.AddField("Person" + index, "null");
				}
			}

			sb = new StringBuilder();
			for (int index = 0; index < Instance.Balls.Count; index++)
			{
				var go = Instance.Balls[index];
				if (go != null)
				{
					sb.Append(go.transform.position.x + ":" + go.transform.position.y + " ");
				}
			}
			jsonMapState.AddField("Balls", sb.ToString().TrimEnd());

			sb = new StringBuilder();
			for (int index = 0; index < Instance.PowerBalls.Count; index++)
			{
				var go = Instance.PowerBalls[index];
				if (go != null)
				{
					sb.Append(go.transform.position.x + ":" + go.transform.position.y + " ");
				}
			}
			jsonMapState.AddField("PowerBalls", sb.ToString().TrimEnd());

			Data.JSON.MapState.Set(jsonMapState.ToString());
			Debug.Log(jsonMapState.ToString());
		}

		/// <summary>
		///     Восстанавливает сохраненную карту
		/// </summary>
		private void RestoreMap()
		{
			CounterBall = 0;
			// Вытаскиваем информацию о карте из JSON
			Map = new char[_MAP_SIZE, _MAP_SIZE];
			JSONObject savedMap = new JSONObject(Data.JSON.MapState.Get());
			for (int i = 0; i < _MAP_SIZE; i++)
			{
				for (int j = 0; j < _MAP_SIZE; j++)
				{
					Map[i, j] = savedMap["Map"].str[i*_MAP_SIZE + j];
				}
			}

			// Помещаем на карту стены
			for (int i = 0; i < Map.GetLength(0); i++)
			{
				for (int j = 0; j < Map.GetLength(1); j++)
				{
					if (Map[i, j] == '#')
					{
						PutObjectToMap(Wall, i, j);
					}
				}
			}

			// Восстанавливаем число очков
			ScoreManager.Instance.AddScore((int) savedMap["Score"].n);

			// Восстанавливаем персонажей
			for (int i = 0; i < Persons.Length; i++)
			{
				if (savedMap["Person" + i].str != "null")
				{
					MapPersons[i] = PutObjectToMap(Persons[i], Convert.ToInt32(savedMap["Person" + i].str.Split(':')[0]) + MAP_OFFSET, Convert.ToInt32(savedMap["Person" + i].str.Split(':')[1]) + MAP_OFFSET, -1);
				}
			}

			// Восстанавливаем шары
			var splitBalls = savedMap["Balls"].str.Split(' ');
			foreach (var ball in splitBalls)
			{
				if (ball != "")
				{
					Balls.Add(PutObjectToMap(Ball, Convert.ToInt32(ball.Split(':')[0]) + MAP_OFFSET, Convert.ToInt32(ball.Split(':')[1]) + MAP_OFFSET));
					CounterBall++;
				}
			}

			// Восстанавливаем огненные шары
			var splitPowerBalls = savedMap["PowerBalls"].str.Split(' ');
			foreach (var ball in splitPowerBalls)
			{
				if (ball != "")
				{
					PowerBalls.Add(PutObjectToMap(BallPower, Convert.ToInt32(ball.Split(':')[0]) + MAP_OFFSET, Convert.ToInt32(ball.Split(':')[1]) + MAP_OFFSET));
					CounterBall++;
				}
			}
		}

		/// <summary>
		///     Генерирует новую карту
		/// </summary>
		private void NewMap()
		{
			CounterBall = 0;
			// генерация карты
			Map = MapGenerator.Generate(_MAP_SIZE, _MAP_SIZE);
			int emptyPlaceCounter = 0;

			for (int i = 0; i < Map.GetLength(0); i++)
			{
				for (int j = 0; j < Map.GetLength(1); j++)
				{
					if (Map[i, j] == ' ')
					{
						// подсчитываем число пустых мест
						// в дальнейшем в них можно будет разместить персонажей
						emptyPlaceCounter++;
					}
				}
			}

			// резеривирование мест для пакмена и привидений
			int[] keyPlaces = new int[Persons.Count()];
			for (int i = 0; i < keyPlaces.Length;)
			{
				int num = _rnd.Next(0, emptyPlaceCounter);
				bool freePlace = true;
				foreach (var n in keyPlaces)
				{
					if (n == num)
					{
						freePlace = false;
					}
				}
				if (freePlace)
				{
					keyPlaces[i] = num;
					i++;
				}
			}

			// расставляем объекты согласно сгенерированным данным
			int personCounter = 0;
			emptyPlaceCounter = 0;
			for (int i = 0; i < Map.GetLength(0); i++)
			{
				for (int j = 0; j < Map.GetLength(1); j++)
				{
					// расставляем стены
					if (Map[i, j] == '#')
					{
						PutObjectToMap(Wall, i, j);
					}
					else
					{
						// расставляем персонажей
						if (keyPlaces.Contains(emptyPlaceCounter))
						{
							MapPersons[personCounter] = PutObjectToMap(Persons[personCounter], i, j, -1);
							personCounter++;
						}
						else
						{
							// расставляем заряженные шары
							if (_rnd.Next(0, 20) < 1)
							{
								PowerBalls.Add(PutObjectToMap(BallPower, i, j));
							}
							// расставляем шары
							else
							{
								Balls.Add(PutObjectToMap(Ball, i, j));
							}
							CounterBall++;
						}
						emptyPlaceCounter++;
					}
				}
			}
		}

		/// <summary>
		///     Помещает объект на карту
		/// </summary>
		/// <param name="gameObj">Объект который нужно разместить на карте</param>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
		/// <param name="z">Координата Z</param>
		/// <returns>Появившийся экземпляр копонента</returns>
		private GameObject PutObjectToMap(GameObject gameObj, int x, int y, int z = 0)
		{
			return Instantiate(gameObj, new Vector3(x - MAP_OFFSET, y - MAP_OFFSET, z), new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
		}
	}
}