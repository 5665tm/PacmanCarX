using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
	public class SpriteAnimation : MonoBehaviour
	{
		/// <summary>
		///     Список спрайтов анимации
		/// </summary>
		public Sprite[] Sprites;

		/// <summary>
		///     Стартовый цвет
		/// </summary>
		public Color StartColor;

		/// <summary>
		///     Конечный цвет
		/// </summary>
		public Color EndColor;

		/// <summary>
		///     Переход от начального цвета к конечному или назад?
		/// </summary>
		private bool _colorToStart;

		/// <summary>
		///     Число спрайтов анимации
		/// </summary>
		private int _spritesCount;

		/// <summary>
		///     Рендер-компонент для спрайтов
		/// </summary>
		private SpriteRenderer _spriteRenderer;

		/// <summary>
		///     Анимация активна?
		/// </summary>
		private bool _isActive = true;

		private void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_spritesCount = Sprites.Count();
		}

		private void Update()
		{
			if (_isActive)
			{
				// Анимация спрайтов
				int specCounter = Convert.ToInt32(Time.timeSinceLevelLoad*5);
				int stageAnimation = specCounter%_spritesCount;
				_spriteRenderer.sprite = Sprites[stageAnimation];

				// Анимация цвета
				if (stageAnimation == 0)
				{
					_colorToStart = !_colorToStart;
				}
				const int NUMBER_OF_STAGE_COLOR = 10;
				int specColorCounter = Convert.ToInt32(Time.timeSinceLevelLoad*NUMBER_OF_STAGE_COLOR);
				int stageColor = specColorCounter%(NUMBER_OF_STAGE_COLOR*2);
				int stageSpecColor = stageColor < NUMBER_OF_STAGE_COLOR ? stageColor : NUMBER_OF_STAGE_COLOR*2 - stageColor - 1;
				_spriteRenderer.color = new Color(Mathf.Lerp(StartColor.r, EndColor.r, stageSpecColor/(NUMBER_OF_STAGE_COLOR-1f)),
					Mathf.Lerp(StartColor.g, EndColor.g, stageSpecColor/(NUMBER_OF_STAGE_COLOR-1f)),
					Mathf.Lerp(StartColor.b, EndColor.b, stageSpecColor/(NUMBER_OF_STAGE_COLOR-1f)));
			}
		}

		/// <summary>
		///     Включает процесс анимации
		/// </summary>
		public void Activate()
		{
			_isActive = true;
		}

		/// <summary>
		///     Выключает процесс анимации
		/// </summary>
		public void Deactivate()
		{
			_isActive = false;
		}
	}
}