using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightBot
{
	public class SceneLoader : MonoBehaviour
	{
		public void LoadGameScene()
		{
			SceneManager.LoadSceneAsync("LightBot/Scenes/LightBot");
		}

		public void LoadMenuScene()
		{
			SceneManager.LoadSceneAsync("LightBot/Scenes/MainMenu");
		}

		public void LoadSettingsScene()
		{
			SceneManager.LoadSceneAsync("LightBot/Scenes/Settings");
		}
	}
}