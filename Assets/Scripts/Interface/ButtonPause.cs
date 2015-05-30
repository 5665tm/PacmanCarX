using UnityEngine;

namespace Assets.Scripts.Interface
{
	/// <summary>
	///     Скрипт для кнопки Пауза-Продолжить
	/// </summary>
	public class ButtonPause : MonoBehaviour
	{
		/// <summary>
		///     Спрайт Play
		/// </summary>
		public Sprite Play;

		/// <summary>
		///     Спрайт Pause
		/// </summary>
		public Sprite Pause;

		/// <summary>
		///     Игра активна?
		/// </summary>
		private bool _isPlay = true;

		/// <summary>
		///     Объект спрай-рендера
		/// </summary>
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			Time.timeScale = 1f;
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				RaycastHit2D hit = Physics2D.Raycast(UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null)
				{
					if (hit.collider.name == gameObject.name)
					{
						if (_isPlay)
						{
							_spriteRenderer.sprite = Pause;
							Time.timeScale = 0;
						}
						else
						{
							_spriteRenderer.sprite = Play;
							Time.timeScale = 1;
						}
						_isPlay = !_isPlay;
					}
				}
			}
		}
	}
}