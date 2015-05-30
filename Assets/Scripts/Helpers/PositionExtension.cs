// Last Change: 2014 11 01 11:33 PM

using UnityEngine;

namespace Assets.Scripts.Helpers
{
	/// <summary>
	///     Extension методы для Transform и GameObject
	/// </summary>
	public static class PositionExtension
	{
		// Установка глобальных значений
		public static void X(this Transform tr, float newValue)
		{
			Vector3 v = tr.position;
			v.x = newValue;
			tr.position = v;
		}

		public static void Y(this Transform tr, float newValue)
		{
			tr.position = new Vector3(tr.position.x, newValue, tr.position.z);
		}

		public static void Z(this Transform tr, float newValue)
		{
			tr.position = new Vector3(tr.position.x, tr.position.y, newValue);
		}

		public static void X(this GameObject go, float newValue)
		{
			go.transform.position = new Vector3(newValue, go.transform.position.y, go.transform.position.z);
		}

		public static void Y(this GameObject go, float newValue)
		{
			go.transform.position = new Vector3(go.transform.position.x, newValue, go.transform.position.z);
		}

		public static void Z(this GameObject go, float newValue)
		{
			go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, newValue);
		}

		// Добавление к глобальным значений
		public static void AddX(this Transform tr, float addedValue)
		{
			tr.position = new Vector3(tr.X() + addedValue, tr.position.y, tr.position.z);
		}

		public static void AddY(this Transform tr, float addedValue)
		{
			tr.position = new Vector3(tr.position.x, tr.Y() + addedValue, tr.position.z);
		}

		public static void AddZ(this Transform tr, float addedValue)
		{
			tr.position = new Vector3(tr.position.x, tr.position.y, tr.Z() + addedValue);
		}

		public static void AddX(this GameObject go, float addedValue)
		{
			go.transform.position = new Vector3(go.X() + addedValue, go.transform.position.y, go.transform.position.z);
		}

		public static void AddY(this GameObject go, float addedValue)
		{
			go.transform.position = new Vector3(go.transform.position.x, go.Y() + addedValue, go.transform.position.z);
		}

		public static void AddZ(this GameObject go, float addedValue)
		{
			go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.Z() + addedValue);
		}

		// Получение глобальных значений
		public static float X(this Transform tr)
		{
			return tr.position.x;
		}

		public static float Y(this Transform tr)
		{
			return tr.position.y;
		}

		public static float Z(this Transform tr)
		{
			return tr.position.z;
		}

		public static float X(this GameObject go)
		{
			return go.transform.position.x;
		}

		public static float Y(this GameObject go)
		{
			return go.transform.position.y;
		}

		public static float Z(this GameObject go)
		{
			return go.transform.position.z;
		}

		// Установка локальных значений
		public static void LocX(this Transform tr, float newValue)
		{
			tr.localPosition = new Vector3(newValue, tr.localPosition.y, tr.localPosition.z);
		}

		public static void LocY(this Transform tr, float newValue)
		{
			tr.localPosition = new Vector3(tr.localPosition.x, newValue, tr.localPosition.z);
		}

		public static void LocZ(this Transform tr, float newValue)
		{
			tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, newValue);
		}

		public static void LocX(this GameObject go, float newValue)
		{
			go.transform.localPosition = new Vector3(newValue, go.transform.localPosition.y, go.transform.localPosition.z);
		}

		public static void LocY(this GameObject go, float newValue)
		{
			go.transform.localPosition = new Vector3(go.transform.localPosition.x, newValue, go.transform.localPosition.z);
		}

		public static void LocZ(this GameObject go, float newValue)
		{
			go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, newValue);
		}

		// Добавление к локальным значениям
		public static void AddLocX(this Transform tr, float addLocedValue)
		{
			tr.localPosition = new Vector3(tr.LocX() + addLocedValue, tr.localPosition.y, tr.localPosition.z);
		}

		public static void AddLocY(this Transform tr, float addLocedValue)
		{
			tr.localPosition = new Vector3(tr.localPosition.x, tr.LocY() + addLocedValue, tr.localPosition.z);
		}

		public static void AddLocZ(this Transform tr, float addLocedValue)
		{
			tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, tr.LocZ() + addLocedValue);
		}

		public static void AddLocX(this GameObject go, float addLocedValue)
		{
			go.transform.localPosition = new Vector3(go.LocX() + addLocedValue, go.transform.localPosition.y, go.transform.localPosition.z);
		}

		public static void AddLocY(this GameObject go, float addLocedValue)
		{
			go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.LocY() + addLocedValue, go.transform.localPosition.z);
		}

		public static void AddLocZ(this GameObject go, float addLocedValue)
		{
			go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, go.LocZ() + addLocedValue);
		}

		// Получение локальных значений
		public static float LocX(this Transform tr)
		{
			return tr.localPosition.x;
		}

		public static float LocY(this Transform tr)
		{
			return tr.localPosition.y;
		}

		public static float LocZ(this Transform tr)
		{
			return tr.localPosition.z;
		}

		public static float LocX(this GameObject go)
		{
			return go.transform.localPosition.x;
		}

		public static float LocY(this GameObject go)
		{
			return go.transform.localPosition.y;
		}

		public static float LocZ(this GameObject go)
		{
			return go.transform.localPosition.z;
		}
	}
}