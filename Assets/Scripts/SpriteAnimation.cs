using System;
using System.Linq;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
	public Sprite[] Sprites;
	public Color StartColor;
	public Color EndColor;
	private bool _colorToStart;
	private int _spritesCount;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spritesCount = Sprites.Count();
	}

	private void Update()
	{
		int specCounter = Convert.ToInt32(Time.timeSinceLevelLoad*5);
		int stageAnimation = specCounter%_spritesCount;
		_spriteRenderer.sprite = Sprites[stageAnimation];

		if (stageAnimation == 0)
		{
			_colorToStart = !_colorToStart;
		}

		int specColorCounter = Convert.ToInt32(Time.timeSinceLevelLoad*10);
		int stageColor = specColorCounter%20;
		int stageSpecColor = stageColor < 10 ? stageColor : 20 - stageColor - 1;

		_spriteRenderer.color = new Color(Mathf.Lerp(StartColor.r, EndColor.r, stageSpecColor/9f),
			Mathf.Lerp(StartColor.g, EndColor.g, stageSpecColor/9f),
			Mathf.Lerp(StartColor.b, EndColor.b, stageSpecColor/9f));
	}
}