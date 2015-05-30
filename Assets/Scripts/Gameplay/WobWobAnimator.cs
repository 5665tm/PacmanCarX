using UnityEngine;

namespace Assets.Scripts.Gameplay
{
	/// <summary>
	///     Осуществляет эффект зацикленного увеличения-уменьшения объекта
	/// </summary>
	public class WobWobAnimator : MonoBehaviour
	{
		private int _counter;
		private const int _MAX_COUNTER = 10;
		private const float _SCALE_FACTOR = 1.008f;
		private bool _maximize;
		private bool _halfFlag;

		private void FixedUpdate()
		{
			if (_halfFlag)
			{
				if (_counter++ < _MAX_COUNTER)
				{
					if (_maximize)
					{
						transform.localScale *= _SCALE_FACTOR;
					}
					else
					{
						transform.localScale /= _SCALE_FACTOR;
					}
				}
				else
				{
					_counter = 0;
					_maximize = !_maximize;
				}
			}
			_halfFlag = !_halfFlag;
		}
	}
}