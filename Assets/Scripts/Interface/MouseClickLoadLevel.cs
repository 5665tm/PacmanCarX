using UnityEngine;

namespace Assets.Scripts.Interface
{
	/// <summary>
	///     Загружает указанный уровень по нажатию кнопкой мыши на объекте
	/// </summary>
	public class MouseClickLoadLevel : MonoBehaviour
	{
		/// <summary>
		///     Название уровня который будет загружаться
		/// </summary>
		public string LoadedLevel;

		private void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				RaycastHit2D hit = Physics2D.Raycast(UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null)
				{
					if (hit.collider.name == gameObject.name)
					{
						Application.LoadLevel(LoadedLevel);
					}
				}
			}
		}
	}
}