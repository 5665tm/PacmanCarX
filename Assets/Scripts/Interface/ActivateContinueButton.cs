using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
	/// <summary>
	///     Включает или отключает кнопку "продолжить" в зависимости от наличия сохраненной игры.
	/// </summary>
	public class ActivateContinueButton : MonoBehaviour
	{
		private void Awake()
		{
			GetComponent<Button>().interactable = Data.JSON.MapState.Get() != "";
		}
	}
}