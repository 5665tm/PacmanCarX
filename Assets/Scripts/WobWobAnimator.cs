using UnityEngine;
using Random = System.Random;

public class WobWobAnimator : MonoBehaviour
{
	private int _counter;
	private int _maxCounter;
	private bool _maximize;

	private void Start()
	{
		_maxCounter = new Random().Next(10, 18);
	}

	private void FixedUpdate()
	{
		if (_counter++ < _maxCounter)
		{
			if (_maximize)
			{
				transform.localScale *= 1.006f;
			}
			else
			{
				transform.localScale /= 1.006f;
			}
		}
		else
		{
			_counter = 0;
			_maximize = !_maximize;
		}
	}
}