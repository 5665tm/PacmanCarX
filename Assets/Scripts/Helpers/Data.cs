using System;
using UnityEngine;

/// <summary>
///     Прослойка для хранения данных игры
/// </summary>
// ReSharper disable once CheckNamespace
public static class Data
{
	// ReSharper disable once InconsistentNaming
	/// <summary>
	///     Перечисление данных использующих числовые значения
	/// </summary>
	public enum NUM : byte
	{
		Score1St,
		Score2Nd,
		Score3Rd,
		ScoreLast
	}

	// ReSharper disable once InconsistentNaming
	/// <summary>
	///     Перечисления имеющие булевые значения
	/// </summary>
	public enum BOOL : byte
	{
		/// <summary>
		///     Разрешены ли звуки?
		/// </summary>
		Sound,

		/// <summary>
		///     Загрузка карты из сохраненного состояния
		/// </summary>
		SavedState
	}

	// ReSharper disable once InconsistentNaming
	/// <summary>
	///     Перечисление данных в формате JSON
	/// </summary>
	public enum JSON : byte
	{
		/// <summary>
		///     Состояние последней игры
		/// </summary>
		MapState
	}

	// ReSharper disable once InconsistentNaming
	/// <summary>
	///     Перечисление данных допускающих выбор
	/// </summary>
	public enum SELECT : byte
	{
	}

	/// <summary>
	///     Установка данных для значений допускающих выбор
	/// </summary>
	/// <param name="key">Выбранное значение</param>
	public static void Set(this Enum key)
	{
		// проверка корректности
		Type type = key.GetType();
		if (!type.FullName.StartsWith("Data") || (type == typeof (NUM) || type == typeof (SELECT) || type == typeof (JSON) || type == typeof (BOOL)))
		{
			Debug.Break();
			throw new Exception("Not correct key");
		}
		// имя ключа
		string keyName = type.Name;
		// значение ключа
		var value = (byte) (Enum.Parse(type, key.ToString()));
		// устанавливаем значение
		PlayerPrefs.SetInt(keyName, value);
	}

	/// <summary>
	///     Установка данных для числовых значений
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="value">Значение</param>
	public static void Set(this NUM key, int value)
	{
		SpecSet(key, value);
	}

	/// <summary>
	///     Установка данных для булевых значений
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="value">Значение</param>
	public static void Set(this BOOL key, int value)
	{
		SpecSet(key, value);
	}

	/// <summary>
	///     Предоставляет реализацию для установки булевых и числовых значений
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	private static void SpecSet(Enum key, int value)
	{
		// имя ключа
		string keyName = key.ToString();
		// устанавливаем значение
		PlayerPrefs.SetInt(keyName, value);
	}

	/// <summary>
	///     Установка данных для булевых значений
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="value">Значение</param>
	public static void Set(this BOOL key, bool value)
	{
		// имя ключа
		string keyName = key.ToString();
		// устанавливаем значение
		PlayerPrefs.SetInt(keyName, Convert.ToInt32(value));
	}

	/// <summary>
	///     Установка данных для JSON значений
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="value">Строка в формате JSON</param>
	public static void Set(this JSON key, string value)
	{
		// имя ключа
		string keyName = key.ToString();
		// устанавливаем значение
		PlayerPrefs.SetString(keyName, value);
	}

	/// <summary>
	///     Добавляет некоторое число к имеющейся величине
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="value">Добавляемое число</param>
	public static void Add(this NUM key, int value)
	{
		int oldValue = key.Get();
		key.Set(oldValue + value);
	}

	/// <summary>
	///     Получение значений ключей от значений допускающих выбор
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <returns>Значение</returns>
	public static int Get(this SELECT key)
	{
		return SpecGet(key);
	}

	/// <summary>
	///     Получение значений ключей от числовых значений
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <returns>Значение</returns>
	public static int Get(this NUM key)
	{
		return SpecGet(key);
	}

	/// <summary>
	///     Предоставляет реализацию для получения числовых значений, и значений допускающих выбор
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	private static int SpecGet(Enum key)
	{
		// имя ключа
		string keyName = key.ToString();
		// устанавливаем значение по умолчанию
		if (!PlayerPrefs.HasKey(keyName))
		{
			PlayerPrefs.SetInt(keyName, 0);
		}
		// получаем значение
		return PlayerPrefs.GetInt(keyName);
	}

	/// <summary>
	///     Получение значений ключей от JSON значений
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <returns>Значение</returns>
	public static string Get(this JSON key)
	{
		// имя ключа
		string keyName = key.ToString();
		// устанавливаем значение по умолчанию
		if (!PlayerPrefs.HasKey(keyName))
		{
			PlayerPrefs.SetString(keyName, "");
		}
		// получаем значение
		return PlayerPrefs.GetString(keyName);
	}

	/// <summary>
	///     Проверяет совпадает ли опция с той что сейчас активна
	/// </summary>
	/// <param name="key"></param>
	/// <returns>Результат сравнения</returns>
	public static bool Is(this Enum key)
	{
		// проверка корректности
		Type type = key.GetType();
		if (!type.FullName.StartsWith("Data") || (type == typeof (NUM) || type == typeof (JSON) || type == typeof (SELECT)))
		{
			Debug.Break();
			throw new Exception("Not correct key: " + key);
		}
		// реализация для булевых значений
		if (type == typeof (BOOL))
		{
			string keyName = key.ToString();
			// устанавливаем значение по умолчанию
			if (!PlayerPrefs.HasKey(keyName))
			{
				PlayerPrefs.SetInt(keyName, 0);
			}
			// получаем значение
			return 0 != PlayerPrefs.GetInt(keyName);
		}
		// реализация для данных допускающих выбор
		else
		{
			// имя ключа
			string keyName = type.Name;
			// устанавливаем значение по умолчанию
			if (!PlayerPrefs.HasKey(keyName))
			{
				PlayerPrefs.SetInt(keyName, 0);
			}
			// значение ключа
			var value = (byte) (Enum.Parse(type, key.ToString()));
			int value2 = PlayerPrefs.GetInt(keyName);
			return value == value2;
		}
	}
}