using UnityEngine;

namespace UI_EventSystems
{
	public class ToggleActive : MonoBehaviour
	{
		public void Toggle()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}