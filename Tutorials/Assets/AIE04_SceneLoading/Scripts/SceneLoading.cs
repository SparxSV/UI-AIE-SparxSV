using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIE04_SceneLoading
{
	public class SceneLoading : MonoBehaviour
	{
		[SerializeField] private bool isSphereScene;
		[SerializeField] private bool loadAdditively;
		[SerializeField, Range(1, 100)] private int sceneAmount = 1;

		private int count = 0;

		private void OnGUI()
		{
			if(isSphereScene)
			{
				// Load the cube scene if this button is pressed
				if(GUILayout.Button("Load Cube Scene"))
				{
					if(count < sceneAmount)
					{
						SceneManager.LoadSceneAsync("AIE04_SceneLoading_Cube", loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single);
						count++;
					}
					else if(count > sceneAmount)
					{
						SceneManager.UnloadSceneAsync("AIE04_SceneLoading_Cube");
						count--;
					}
				}
			}
			else
			{
				// Load the sphere scene if this button is pressed
				if(GUILayout.Button("Load Sphere Scene"))
				{
					if(count < sceneAmount)
					{
						SceneManager.LoadSceneAsync("AIE04_SceneLoading_Sphere", loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single);
						count++;
					}
					else if(count > sceneAmount)
					{
						SceneManager.UnloadSceneAsync("AIE04_SceneLoading_Sphere");
						count--;
					}
				}
			}
		}
	}
}