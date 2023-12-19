using System;

using UnityEngine;

using Random = UnityEngine.Random;

namespace AIE02_Input
{
	public class UnityInput : MonoBehaviour
	{
		[SerializeField] private float moveSpeed = 3f;

		private new MeshRenderer renderer;

		private void Start() => renderer = gameObject.GetComponent<MeshRenderer>();

		private void Update()
		{
			Vector3 input = new Vector3
			{
				x = Input.GetAxis("Horizontal"),
				y = Input.GetAxis("Depthical"),
				z = Input.GetAxis("Vertical")
			};

			transform.position += transform.right * (input.x * Time.deltaTime * moveSpeed);
			transform.position += transform.up * (input.y * Time.deltaTime * moveSpeed);
			transform.position += transform.forward * (input.z * Time.deltaTime * moveSpeed);
			
			if(Input.GetKeyDown(KeyCode.Space))
			{
				renderer.material.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
			}

			if(Input.GetButtonDown("Fire1"))
			{
				Vector2 randomPos = Random.insideUnitSphere * 5f;
				transform.position = randomPos;
			}
		}
	}
}