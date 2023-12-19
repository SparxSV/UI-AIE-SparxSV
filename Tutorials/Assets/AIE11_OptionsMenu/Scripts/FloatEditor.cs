using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace AIE11_OptionsMenu.Scripts
{
	public class FloatEditor : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private Slider slider;
		[SerializeField] private TMP_InputField input;
		
		// string used to format the text field when you move the slider
		[SerializeField] private string formatString = "0.0";
	}
}