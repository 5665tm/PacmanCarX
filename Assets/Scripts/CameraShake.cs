// Last Change: 2014 11 07 8:51 PM

using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class CameraShake : MonoBehaviour
	{
		private static Vector3 originPosition;
		private static Quaternion originRotation;

		private static float shakeDecay;
		public static float ShakeIntensity;
		private static Vector3? originPosition2;
		private static Quaternion originRotation2;
		private Transform camera;

		public void Start()
		{
			camera = GetComponent<Transform>();
		}

		public IEnumerator Shake(float magnitude, float duration)
		{
			float elapsed = 0.0f;

			Vector3 originalCamPos = Camera.main.transform.position;

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;

				float percentComplete = elapsed/duration;
				float damper = 1.0f - Mathf.Clamp(4.0f*percentComplete - 3.0f, 0.0f, 1.0f);

				// map value to [-1, 1]
				float x = Random.value*2.0f - 1.0f;
				float y = Random.value*2.0f - 1.0f;
				x *= magnitude*damper;
				y *= magnitude*damper;

				Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

				yield return null;
			}

			Camera.main.transform.position = originalCamPos;
		}
	}
}