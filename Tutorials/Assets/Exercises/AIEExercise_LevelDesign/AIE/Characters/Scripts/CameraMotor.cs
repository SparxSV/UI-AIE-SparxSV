using UnityEngine;

namespace AIE.Characters
{
	public class CameraMotor : MonoBehaviour
	{
		public Transform BoomArm => boomArm;

		[Header("General")] 
		[SerializeField] private Transform target;
		[SerializeField] private Vector2 cameraBounds = new(-90f, 90f);
		[SerializeField, Min(1.0f)] private float sensitivity = 1.0f;
		
		[Header("Transforms")]
		[SerializeField] private new Transform camera;
		[SerializeField] private Transform boomArm;
		
		[Header("Positioning")]
		[SerializeField, Range(0f, 10f)] private float targetArmLength = 2.5f;
		[SerializeField] private Vector3 offset;

		[Header("Collisions")]
		[SerializeField] private float colliderRadius = .5f;
		[SerializeField] private LayerMask collisionMask = 1;

		private float currentArmLength;

		private Vector2 rotation;

		private void OnValidate()
		{
			cameraBounds.x = Mathf.Clamp(cameraBounds.x, -90f, 0f);
			cameraBounds.y = Mathf.Clamp(cameraBounds.y, 0f, 90f);

			if(camera != null && boomArm != null)
			{
				boomArm.localPosition = offset;
				camera.position = boomArm.position - boomArm.forward * targetArmLength;
			}
		}

		public void Initialise()
		{
			currentArmLength = targetArmLength;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void Tick()
		{
			if(target != null)
				transform.position = target.position;
			
			if(boomArm != null)
			{
				float horizontal = Input.GetAxis("Mouse X") * sensitivity;
				float vertical = Input.GetAxis("Mouse Y") * sensitivity;

				rotation.y += horizontal;
				rotation.x += vertical;
				rotation.x = Mathf.Clamp(rotation.x, -cameraBounds.y, -cameraBounds.x);

				boomArm.localEulerAngles = new Vector3(-rotation.x, rotation.y);
			}
		}

		public void LateTick()
		{
			currentArmLength = Physics.SphereCast(boomArm.position, colliderRadius, -boomArm.forward, out RaycastHit hit, targetArmLength + colliderRadius, collisionMask) ? hit.distance : targetArmLength;
			
			if(camera != null && boomArm != null)
			{
				boomArm.localPosition = offset;
				camera.position = boomArm.position - boomArm.forward * currentArmLength;
			}
		}

		private void OnDrawGizmos()
		{
			if(camera != null)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(camera.position, colliderRadius);
			}

			if(boomArm != null)
			{
				Vector3 endPoint = boomArm.position - boomArm.forward * targetArmLength;

				Gizmos.color = Color.red;
				Gizmos.DrawLine(boomArm.position, endPoint);
			}
		}
	}
}