using UnityEngine;

namespace AIE11_OptionsMenu.Scripts
{
	public class ToggleSettings : MonoBehaviour
	{
		public void Toggle()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}