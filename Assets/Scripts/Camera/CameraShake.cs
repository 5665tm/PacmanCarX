// Last Change: 2014 11 07 8:51 PM

using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Camera
{
	/// <summary>
	///     Осуществляет "встряску" главной камеры, например при взрыве
	/// </summary>
	public class CameraShake : MonoBehaviour
	{
		/// <summary>
		///     Запуск эффекта встряски
		/// </summary>
		/// <param name="magnitude">Сила встряски</param>
		/// <param name="duration">Продолжительность встряски</param>
		/// <returns></returns>
		public IEnumerator Shake(float magnitude, float duration)
		{
			Vector3 originalCamPos = UnityEngine.Camera.main.transform.position;
			float elapsed = 0.0f;

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;

				float percentComplete = elapsed/duration;
				float damper = 1.0f - Mathf.Clamp(4.0f*percentComplete - 3.0f, 0.0f, 1.0f);

				float x = Random.value*2.0f - 1.0f;
				float y = Random.value*2.0f - 1.0f;
				x *= magnitude*damper;
				y *= magnitude*damper;

				UnityEngine.Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

				yield return null;
			}

			UnityEngine.Camera.main.transform.position = originalCamPos;
		}
	}
}