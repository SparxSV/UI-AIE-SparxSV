using System;
using System.ComponentModel;
using System.Globalization;

using TMPro;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

using Slider = UnityEngine.UI.Slider;

namespace Options_UI
{
	public class SettingsUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private Settings settings;

		[Header("-- Sliders --")] 
		[SerializeField] private Slider musicSlider;
		[SerializeField] private Slider fxSlider;

		[Header("-- Input Fields --")]
		[SerializeField] private TMP_InputField musicInputField;
		[SerializeField] private TMP_InputField fxInputField;

		public void ToggleSettings()
		{
			gameObject.SetActive(!gameObject.activeSelf);
			text.text = !gameObject.activeSelf ? "OFF" : "ON";
			
			settings.musicVolume = musicSlider.value;
			settings.soundFxVolume = fxSlider.value;
			
			// DEBUG TOOLS
			Debug.Log($"Settings Window set to {text.text}");
		}

		public void ToggleStereo()
		{
			settings.stereoMute = !settings.stereoMute;
			
			if(settings.stereoMute)
			{
				musicSlider.value = 0f;
				fxSlider.value = 0f;
				
				// DEBUG TOOLS
				Debug.Log("Volume has been muted");
			}
			else
			{
				musicSlider.value = 50f;
				fxSlider.value = 50f;
				
				// DEBUG TOOLS
				Debug.Log("Volume has been un-muted. Volume set to 50%");
			}
		}

		public void OnMusicVolumeChanged(float _volume)
		{
			settings.musicVolume = _volume;
			musicInputField.text = _volume.ToString();
			
			// DEBUG TOOLS
			Debug.Log($"Music level set to {_volume}");
		}

		public void OnFxVolumeChanged(float _volume)
		{
			settings.soundFxVolume = _volume;
			fxInputField.text = _volume.ToString();
			
			// DEBUG TOOLS
			Debug.Log($"Fx level set to {_volume}");
		}

		public void OnMusicEndChange(string _volume)
		{
			double musicVolume = Convert.ToDouble(_volume);

			if(musicVolume > 100f)
				musicVolume = 100f;
			
			else if(musicVolume < 0f)
				musicVolume = 0f;
			
			settings.musicVolume = (float)musicVolume;
			musicSlider.value = (float)musicVolume;
			musicInputField.text = musicVolume.ToString();
			
			// DEBUG TOOLS
			Debug.Log($"Music level has been set to {musicVolume}");
		}

		public void OnFxEndChange(string _volume)
		{
			double fxVolume = Convert.ToDouble(_volume);

			if(fxVolume > 100f)
				fxVolume = 100f;
			
			else if(fxVolume < 0f)
				fxVolume = 0f;

			settings.soundFxVolume = (float) fxVolume;
			fxSlider.value = (float) fxVolume;
			fxInputField.text = fxVolume.ToString();
			
			// DEBUG TOOLS
			Debug.Log($"Fx level has been set to {fxVolume}");
		}

		private void Start()
		{
			text.text = "ON";

			musicSlider.value = 50f;
			fxSlider.value = 50f;
			
			musicInputField.text = musicSlider.value.ToString();
			fxInputField.text = fxSlider.value.ToString();
			
			// DEBUG TOOLS
			Debug.Log($"Volume has been set to 50%");
		}
	}
}
