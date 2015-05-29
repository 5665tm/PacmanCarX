// Last Change: 2015 01 24 21:02

using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

// ReSharper disable once UnusedMember.Global
// ReSharper disable once CheckNamespace
public class MapGenerator : MonoBehaviour
{
	public GameObject[] Persons;
	public static MapGenerator Instance;
	public GameObject Wall;
	public GameObject Ball;
	public GameObject BallPower;
	public char[,] Map { get; private set; }
	public int PacmanX { get; private set; }
	public int PacmanY { get; private set; }
	private readonly Random _rnd = new Random();
	private int _indexDungeonCube;

	// ReSharper disable once UnusedMember.Local
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
		Map = GenerateMap(17, 17);
		int emptyPlaceCounter = 0;

		for (int i = 0; i < Map.GetLength(0); i++)
		{
			for (int j = 0; j < Map.GetLength(1); j++)
			{
				if (Map[i, j] == ' ')
				{
					emptyPlaceCounter++;
				}
			}
		}

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

		int personCounter = 0;
		emptyPlaceCounter = 0;
		for (int i = 0; i < Map.GetLength(0); i++)
		{
			for (int j = 0; j < Map.GetLength(1); j++)
			{
				if (Map[i, j] == '#')
				{
					PutObjectToMap(Wall, i, j);
				}
				else
				{
					if (keyPlaces.Contains(emptyPlaceCounter))
					{
						if (personCounter == 0)
						{
							PacmanX = i;
							PacmanY = j;
						}
						PutObjectToMap(Persons[personCounter], i, j);
						personCounter++;
					}
					else
					{
						PutObjectToMap(_rnd.Next(0, 5) < 1 ? BallPower : Ball, i, j);
					}
					emptyPlaceCounter++;
				}
			}
		}
	}

	private void PutObjectToMap(GameObject gameObj, int x, int y)
	{
		Instantiate(gameObj, new Vector3(x - 8, y - 8), new Quaternion(0f, 0f, 0f, 0f));
	}

	/// <summary>
	///     Возвращает сгенерированную карту на основе одного из двух алгоритмов.
	/// </summary>
	/// <param name="height">Высота карты</param>
	/// <param name="width">Ширина карты</param>
	/// <returns>Сгенерированная карта</returns>
	private char[,] GenerateMap(int height, int width)
	{
		return _rnd.Next(0, 1) == 0 ? GenerateMapLogicOne(height, width) : GenerateMapLogicTwo(height, width);
	}

	/// <summary>
	///     Генератор карт для пространств выраженных блоками. Подходит для "коридорных карт".
	/// </summary>
	/// <param name="maxHeight">Максимально допустимая высота карты</param>
	/// <param name="maxWidth">Максимально допустимая ширина карты</param>
	/// <returns>Сгенерированная карта.</returns>
	private char[,] GenerateMapLogicOne(int maxHeight, int maxWidth)
	{
		// Этап 1 - генерерируем массив с заданными размерами.
		// Заполняем карту стенами.
		int width = maxWidth;
		int height = maxHeight;
		var arrMap = new char[height, width];
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				arrMap[i, j] = '#';
			}
		}
		// Этап 2 - выбираем рандомно место откуда идти ' '
		int yPoint = _rnd.Next(1, height - 1);
		int xPoint = _rnd.Next(1, width - 1);
		arrMap[yPoint, xPoint] = ' ';
		// Этап 3 - идем куда нибудь, пробивая себе путь среди стен
		// сколько прямых линий мы проедем
		int pathNumber = _rnd.Next(50, 200);
		int pathMin = _rnd.Next(2, 5);
		int pathMax = _rnd.Next(pathMin, pathMin + 10);
		for (int m = 0; m < pathNumber; m++)
		{
			// куда мы будем двигаться, вверх, влево, направо или вниз
			int vector = _rnd.Next(0, 4);
			// сколько клеток содержит наш путь
			int lengthPath = _rnd.Next(pathMin, pathMax);
			switch (vector)
			{
				// вверх
				case 0:
					for (int i = 0; i <= lengthPath; i++)
					{
						int y = --yPoint;
						if (xPoint >= 1 && xPoint <= width - 2 && yPoint >= 1 && yPoint <= height - 2)
						{
							arrMap[y, xPoint] = ' ';
						}
						else
						{
							yPoint = 1;
							break;
						}
					}
					break;
				// вниз
				case 1:
					for (int i = 0; i <= lengthPath; i++)
					{
						int y = ++yPoint;
						if (xPoint >= 1 && xPoint <= width - 2 && yPoint >= 1 && yPoint <= height - 2)
						{
							arrMap[y, xPoint] = ' ';
						}
						else
						{
							yPoint = height - 2;
							break;
						}
					}
					break;
				// влево
				case 2:
					for (int i = 0; i <= lengthPath; i++)
					{
						int x = --xPoint;
						if (xPoint >= 1 && xPoint <= width - 2 && yPoint >= 1 && yPoint <= height - 2)
						{
							arrMap[yPoint, x] = ' ';
						}
						else
						{
							xPoint = 1;
							break;
						}
					}
					break;
				// направо
				default:
					for (int i = 0; i <= lengthPath; i++)
					{
						int x = ++xPoint;
						if (xPoint >= 1 && xPoint <= width - 2 && yPoint >= 1 && yPoint <= height - 2)
						{
							arrMap[yPoint, x] = ' ';
						}
						else
						{
							xPoint = width - 2;
							break;
						}
					}
					break;
			}
		}
		return arrMap;
	}

	/// <summary>
	///     Генератор карт для пространств выраженных блоками. Подходит для относительно свободных пространств.
	/// </summary>
	/// <param name="maxHeight">Максимально допустимая высота карты</param>
	/// <param name="maxWidth">Максимально допустимая ширина карты</param>
	/// <returns>Сгенерированная карта.</returns>
	private char[,] GenerateMapLogicTwo(int maxHeight, int maxWidth)
	{
		// Этап 1 - генерерируем массив с заданными размерами.
		// Заполняем карту стенами.
		int width = maxWidth;
		int height = maxHeight;
		var arrMap = new char[height, width];
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				arrMap[i, j] = '#';
			}
		}
		// Этап 2 - выбираем рандомно место откуда идти '.'
		int xpoint = _rnd.Next(1, width - 1);
		int ypoint = _rnd.Next(1, height - 1);
		arrMap[ypoint, xpoint] = ' ';
		// Этап 3 - идем куда нибудь, пробивая себе путь среди стен
		for (int m = 0; m < width*20; m++)
		{
			if (xpoint == 1)
			{
				xpoint = _rnd.Next(xpoint, 3);
				arrMap[ypoint, xpoint] = ' ';
			}
			if (xpoint == width - 2)
			{
				xpoint = _rnd.Next(xpoint - 1, xpoint + 1);
				arrMap[ypoint, xpoint] = ' ';
			}
			if (xpoint > 1 && xpoint < width - 2)
			{
				xpoint = _rnd.Next(xpoint - 1, xpoint + 2);
				arrMap[ypoint, xpoint] = ' ';
			}
			if (ypoint == 1)
			{
				ypoint = _rnd.Next(ypoint, 3);
				arrMap[ypoint, xpoint] = ' ';
			}
			if (ypoint == height - 2)
			{
				ypoint = _rnd.Next(ypoint - 1, ypoint + 1);
				arrMap[ypoint, xpoint] = ' ';
			}
			if (ypoint > 1 && ypoint < height - 2)
			{
				ypoint = _rnd.Next(ypoint - 1, ypoint + 2);
				arrMap[ypoint, xpoint] = ' ';
			}
		}
		return arrMap;
	}
}