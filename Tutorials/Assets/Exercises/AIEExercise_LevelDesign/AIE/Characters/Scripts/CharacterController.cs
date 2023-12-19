using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AIE.Characters
{
	public class CharacterController : MonoBehaviour
	{
		[SerializeField] private new CameraMotor camera;
		[SerializeField] private CharacterMotor motor;

	#if UNITY_EDITOR
		private void Awake()
		{
			if(camera == null || motor == null)
			{
				Debug.LogError("Motor or Camera not set!");
				enabled = false;
				EditorApplication.isPlaying = false;
			}
		}
	#endif

		private void Start()
		{
			camera.Initialise();
			motor.Initialise();
		}

		private void Update()
		{
			camera.Tick();
			motor.Tick(camera);
		}

		private void FixedUpdate() => motor.FixedTick(camera);

		private void LateUpdate() => camera.LateTick();
	}
}