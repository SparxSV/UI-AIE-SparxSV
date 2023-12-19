using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace AIE11_OptionsMenu.Scripts
{
	public class SettingsUI : MonoBehaviour
	{
		[SerializeField] private Slider slider;
		[SerializeField] private TextMeshProUGUI sliderText;

		private void Start()
		{
			sliderText.text = slider.value.ToString();
		}

		public void OnSliderValueChanged(float _value)
		{
			sliderText.text = _value.ToString();
		}
	}
}