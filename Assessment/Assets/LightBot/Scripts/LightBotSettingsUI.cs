using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace LightBot
{
	public class LightBotSettingsUI : MonoBehaviour
	{
		[SerializeField] private LightBotSettings settings;

		[Header("-- Sliders --")]
		[SerializeField] private Slider musicSlider;
		[SerializeField] private Slider fxSlider;

		[Header("-- Input Fields --")]
		[SerializeField] private TMP_InputField musicInputField;
		[SerializeField] private TMP_InputField fxInputField;

		public void ToggleStereo()
		{
			settings.stereoMute = !settings.stereoMute;

			if(settings.stereoMute) // Muted
			{
				musicSlider.value = 0f;
				fxSlider.value = 0f;
			}
			else // Un-muted
			{
				musicSlider.value = 50f;
				fxSlider.value = 50f;
			}
		}

		public void OnMusicVolumeChanged(float _volume)
		{
			settings.musicVolume = _volume;
			musicInputField.text = _volume.ToString();
		}

		public void OnFxVolumeChanged(float _volume)
		{
			settings.soundFxVolume = _volume;
			fxInputField.text = _volume.ToString();
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
		}

		private void Start()
		{
			musicSlider.value = 50f;
			fxSlider.value = 50f;
			
			musicInputField.text = musicSlider.value.ToString();
			fxInputField.text = fxSlider.value.ToString();
		}
	}
}