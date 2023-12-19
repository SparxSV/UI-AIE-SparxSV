using System;

using UnityEngine;

namespace LightBot
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] public float smoothness;
		[SerializeField] public Transform targetObject;

		private Vector3 initialOffset;
		private Vector3 cameraPosition;

		private void Start()
		{
			initialOffset = transform.position - targetObject.position;
		}

		private void LateUpdate()
		{
			cameraPosition = targetObject.position + initialOffset;
			transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.smoothDeltaTime);
		}
	}
}