using System;
using System.ComponentModel;
using System.Numerics;

using UnityEngine;

using Vector3 = UnityEngine.Vector3;

namespace AIE01_FirstSteps
{
	public class TransformsAndVariables : MonoBehaviour
	{
		public enum MovementType
		{
			Linear,
			PingPong,
			Sin
		}

		[SerializeField] private MovementType movementType;
		[SerializeField] private float moveSpeed = 1f;

		private float time;

		private void Start() => time = 0;

		private void Update()
		{
			Vector3 newPos = transform.position;
			time += Time.deltaTime * moveSpeed;

			switch(movementType)
			{
				case MovementType.Linear:
					newPos += transform.up * (Time.deltaTime * moveSpeed);
					break;
				
				case MovementType.PingPong:
					newPos += transform.up * (Mathf.PingPong(time, 1f) * Time.deltaTime);
					break;
				
				case MovementType.Sin:
					newPos += transform.up * (Mathf.Sin(time) * Time.deltaTime);
					break;
				
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			transform.position = newPos;
		}
	}
}