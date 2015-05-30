using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
	/// <summary>
	///     Скрипт для кнопки управления звуком
	/// </summary>
	public class ButtonSoundOnOff : MonoBehaviour
	{
		/// <summary>
		///     Спрайт для изображения кнопки когда звук включен
		/// </summary>
		public Sprite SoundOnSprite;

		/// <summary>
		///     Спрайт для изображения кнопки когда звук выключен
		/// </summary>
		public Sprite SoundOffSprite;

		/// <summary>
		///     Включен ли звук?
		/// </summary>
		private bool _isSoundOn = true;

		/// <summary>
		///     Управляние картинкой для кнопки звука
		/// </summary>
		private Image _imageRenderer;

		/// <summary>
		///     Звук который проигрывается когда включили звук
		/// </summary>
		public AudioClip SoundOnAudioClip;

		/// <summary>
		///     Источник звука откуда проигрывается звук включения
		/// </summary>
		private AudioSource _audioSource;

		private void Awake()
		{
			_isSoundOn = Data.BOOL.Sound.Is();
			_imageRenderer = GetComponent<Image>();
			_imageRenderer.sprite = _isSoundOn ? SoundOnSprite : SoundOffSprite;
			_audioSource = GetComponent<AudioSource>();
		}

		public void Click()
		{
			if (_isSoundOn)
			{
				_imageRenderer.sprite = SoundOffSprite;
				Data.BOOL.Sound.Set(false);
			}
			else
			{
				_imageRenderer.sprite = SoundOnSprite;
				Data.BOOL.Sound.Set(true);
				_audioSource.PlayOneShot(SoundOnAudioClip);
			}
			_isSoundOn = !_isSoundOn;
		}
	}
}