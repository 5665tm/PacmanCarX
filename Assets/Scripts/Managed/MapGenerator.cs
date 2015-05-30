using System;

namespace Assets.Scripts.Managed
{
	/// <summary>
	///     Менеджер карты
	/// </summary>
	public partial class MapManager
	{
		/// <summary>
		///     Предоставляет базовые возможности для генерации карт
		/// </summary>
		internal static class MapGenerator
		{
			/// <summary>
			///     Нахрен логику, включи рандом
			/// </summary>
			private static readonly Random _rndGen = new Random();

			/// <summary>
			///     Возвращает сгенерированную карту на основе одного из двух алгоритмов.
			/// </summary>
			/// <param name="height">Высота карты</param>
			/// <param name="width">Ширина карты</param>
			/// <returns>Сгенерированная карта</returns>
			public static char[,] Generate(int height, int width)
			{
				return _rndGen.Next(0, 1) == 0 ? GenerateLogicOne(height, width) : GenerateLogicTwo(height, width);
			}

			/// <summary>
			///     Генератор карт для пространств выраженных блоками. Подходит для "коридорных карт".
			/// </summary>
			/// <param name="maxHeight">Максимально допустимая высота карты</param>
			/// <param name="maxWidth">Максимально допустимая ширина карты</param>
			/// <returns>Сгенерированная карта.</returns>
			private static char[,] GenerateLogicOne(int maxHeight, int maxWidth)
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
				int yPoint = _rndGen.Next(1, height - 1);
				int xPoint = _rndGen.Next(1, width - 1);
				arrMap[yPoint, xPoint] = ' ';
				// Этап 3 - идем куда нибудь, пробивая себе путь среди стен
				// сколько прямых линий мы проедем
				int pathNumber = _rndGen.Next(50, 200);
				int pathMin = _rndGen.Next(2, 5);
				int pathMax = _rndGen.Next(pathMin, pathMin + 10);
				for (int m = 0; m < pathNumber; m++)
				{
					// куда мы будем двигаться, вверх, влево, направо или вниз
					int vector = _rndGen.Next(0, 4);
					// сколько клеток содержит наш путь
					int lengthPath = _rndGen.Next(pathMin, pathMax);
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
			private static char[,] GenerateLogicTwo(int maxHeight, int maxWidth)
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
				int xpoint = _rndGen.Next(1, width - 1);
				int ypoint = _rndGen.Next(1, height - 1);
				arrMap[ypoint, xpoint] = ' ';
				// Этап 3 - идем куда нибудь, пробивая себе путь среди стен
				for (int m = 0; m < width*20; m++)
				{
					if (xpoint == 1)
					{
						xpoint = _rndGen.Next(xpoint, 3);
						arrMap[ypoint, xpoint] = ' ';
					}
					if (xpoint == width - 2)
					{
						xpoint = _rndGen.Next(xpoint - 1, xpoint + 1);
						arrMap[ypoint, xpoint] = ' ';
					}
					if (xpoint > 1 && xpoint < width - 2)
					{
						xpoint = _rndGen.Next(xpoint - 1, xpoint + 2);
						arrMap[ypoint, xpoint] = ' ';
					}
					if (ypoint == 1)
					{
						ypoint = _rndGen.Next(ypoint, 3);
						arrMap[ypoint, xpoint] = ' ';
					}
					if (ypoint == height - 2)
					{
						ypoint = _rndGen.Next(ypoint - 1, ypoint + 1);
						arrMap[ypoint, xpoint] = ' ';
					}
					if (ypoint > 1 && ypoint < height - 2)
					{
						ypoint = _rndGen.Next(ypoint - 1, ypoint + 2);
						arrMap[ypoint, xpoint] = ' ';
					}
				}
				return arrMap;
			}
		}
	}
}