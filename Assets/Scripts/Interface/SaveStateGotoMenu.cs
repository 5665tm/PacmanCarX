using Assets.Scripts.Managed;
using UnityEngine;

namespace Assets.Scripts.Interface
{
	/// <summary>
	///     Выполняет сохранение состояния игры
	/// </summary>
	public class SaveStateGotoMenu : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				RaycastHit2D hit = Physics2D.Raycast(UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null)
				{

					if (hit.collider.name == gameObject.name)
					{
						MapManager.SaveState();
						Application.LoadLevel("Menu");
					}
				}
			}
		}
	}
}