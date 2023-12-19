using UnityEngine;

using Random = UnityEngine.Random;

namespace AIE09_Physics
{
	public class ShootBall : MonoBehaviour
	{
		[SerializeField] private new Transform camera;
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private LayerMask hitLayers = 1;

		private void Update()
		{
			if(Input.GetButtonDown("Fire1"))
			{
				GameObject newBullet = Instantiate(bulletPrefab, camera.position + camera.forward, Quaternion.identity, transform);
				Rigidbody rb = newBullet.GetComponent<Rigidbody>();
				rb.AddForce(camera.forward * 10, ForceMode.Impulse);
			}

			if(Input.GetMouseButtonDown(1))
			{
				if(Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 1, hitLayers))
				{
					MeshRenderer rend = hit.collider.GetComponent<MeshRenderer>();
					rend.material.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
				}
			}
		}
	}
}