using System;
using System.Security;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace AIE06_UIIntro
{
	public class UI : MonoBehaviour
	{
		[SerializeField] private Button button;
		[SerializeField] private Slider slider;
		[SerializeField] private Toggle toggle;
		[SerializeField] private TextMeshProUGUI sliderText;

		private void Start()
		{
			sliderText.text = slider.value.ToString();

			button.onClick.AddListener(OnClickUsingScript);
			toggle.onValueChanged.AddListener(_isOn =>
			{
				button.interactable = _isOn;
				
				if(!_isOn)
					Debug.LogWarning("Button Disabled");

				else
					Debug.LogWarning("Button Enabled");
				});
		}

		public void OnSliderValueChanged(float _value)
		{
			sliderText.text = _value.ToString();
		}
		
		public void OnClick()
		{
			Debug.Log("Button Clicked!");
		}

		private void OnClickUsingScript()
		{
			Debug.LogWarning("This event was subscribed to through code and is doing stuff.");
		}
	}
}