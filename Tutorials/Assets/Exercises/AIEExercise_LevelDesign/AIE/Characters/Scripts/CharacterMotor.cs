using System;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AIE.Characters
{
	[Serializable] public class MovementEvent : UnityEvent<Vector2> { }

	[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
	public class CharacterMotor : MonoBehaviour
	{
	#region Auto Tagging

	#if UNITY_EDITOR
		private const string WALL_TAG_LAYER = "Walls";
		private const string GROUND_TAG_LAYER = "Ground";

		[InitializeOnLoadMethod]
		public static void ManageLayer()
		{
			SerializedObject tagManager = new(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset"));
			SerializedProperty layers = tagManager.FindProperty("layers");
			SerializedProperty tags = tagManager.FindProperty("tags");

			if(DoesLayerExist(GROUND_TAG_LAYER) && DoesLayerExist(WALL_TAG_LAYER) && DoesTagExist(WALL_TAG_LAYER, tags) && DoesTagExist(GROUND_TAG_LAYER, tags))
				return;

			if(layers is not { isArray: true })
			{
				Debug.LogWarning($"Unable to set up layers. You can resolve this issue by manually adding a tag named 'Ground'.");
				return;
			}

			if(tags is not { isArray: true })
			{
				Debug.LogWarning($"Unable to set up tags. You can resolve this issue by manually adding a tag named 'Wall'.");
				return;
			}

			bool set = false;
			tags.arraySize = 0;

			if(!AddLayer(GROUND_TAG_LAYER, layers))
				Debug.LogWarning($"Layer '{GROUND_TAG_LAYER}' already exists!");
			else
				set = true;

			if(!AddLayer(WALL_TAG_LAYER, layers))
				Debug.LogWarning($"Layer '{WALL_TAG_LAYER}' already exists!");
			else
				set = true;

			if(!AddTag(GROUND_TAG_LAYER, tags))
				Debug.LogWarning($"Tag '{GROUND_TAG_LAYER}' already exists!");
			else
				set = true;

			if(!AddTag(WALL_TAG_LAYER, tags))
				Debug.LogWarning($"Tag '{WALL_TAG_LAYER}' already exists!");
			else
				set = true;

			if(set)
			{
				Debug.Log($"Layers 'Ground' and 'Wall' created.");
				tagManager.ApplyModifiedProperties();
			}
			else
			{
				Debug.LogWarning($"Unable to create Layers 'Wall' and 'Ground' - no blank layers found to replace! Please create the layer 'UIObject3d' manually in order to continue.");
			}
		}

		private static bool AddLayer(string _layer, SerializedProperty _property)
		{
			if(DoesLayerExist(_layer))
				return false;

			for(int i = 8; i < _property.arraySize; i++)
			{
				SerializedProperty element = _property.GetArrayElementAtIndex(i);

				if(element.stringValue != "")
					continue;

				if(element.stringValue == _layer)
					break;

				element.stringValue = _layer;
				return true;
			}

			return false;
		}

		private static bool AddTag(string _tag, SerializedProperty _property)
		{
			if(DoesTagExist(_tag, _property))
				return false;

			_property.InsertArrayElementAtIndex(0);
			_property.GetArrayElementAtIndex(0).stringValue = _tag;
			return true;
		}

		private static bool DoesLayerExist(string _layer) => LayerMask.NameToLayer(_layer) != -1;

		private static bool DoesTagExist(string _tag, SerializedProperty _property)
		{
			for(int i = 0; i < _property.arraySize; i++)
			{
				if(_property.GetArrayElementAtIndex(i).stringValue == _tag)
					return true;
			}

			return false;
		}

#endif

	#endregion
		
		private const float SPEED_ON_GROUND_MODIFIER = 1;
		private const float SPEED_IN_AIR_MODIFIER = 1;
		private const float IN_AIR_GROUND_CHECK_DELAY = 0.2f;
		
		public bool IsGrounded { get; private set; }

		[Header("Components")]
		[SerializeField] private Transform model;

		[Header("Movement Settings")]
		[SerializeField, Range(1, 20)] private float jumpForce = 8;
		[SerializeField, Range(1, 20)] private float maxSpeed = 8;

		[Header("Ground Check Settings")]
		[SerializeField, Min(0.1f)] private float groundDistanceCheck = 1;
		[SerializeField, Min(0.2f)] private float groundDistanceInAirCheck = 0.2f;

		[SerializeField, Min(0.1f)] private float fallMultiplier = 2.5f;
		[SerializeField, Min(0.2f)] private float lowJumpMultiplier = 2f;

		[Header("Collision Settings")]
		[SerializeField] private LayerMask layerChecks;
		[SerializeField] private string groundTag = "Ground";
		[SerializeField] private string wallTag = "Walls";

		[Header("Events")] 
		public MovementEvent motionEvent = new();

		private float lastTimeInAir;

		private bool isJumpPressed;
		private Vector2 movement;
		
		private new Rigidbody rigidbody;
		private new CapsuleCollider collider;

		public void Initialise()
		{
			rigidbody = gameObject.GetComponent<Rigidbody>();
			collider = gameObject.GetComponent<CapsuleCollider>();
		}

		public void Tick(CameraMotor _cameraMotor)
		{
			isJumpPressed = Input.GetButton("Jump");

			// If we are grounded jump and store the jump time
			if(isJumpPressed && IsGrounded)
			{
				rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				IsGrounded = false;

				lastTimeInAir = Time.time;
			}

			movement = new Vector2
			{
				x = Input.GetAxis("Horizontal"),
				y = Input.GetAxis("Vertical")
			};
			
			CheckGrounded();

			if(model != null && movement != Vector2.zero)
			{
				Quaternion targetRot = Quaternion.Euler(0, _cameraMotor.BoomArm.localEulerAngles.y, 0);
				model.localRotation = Quaternion.Slerp(model.rotation, targetRot, Time.deltaTime * 20);
			}
			
			motionEvent.Invoke(new Vector2(rigidbody.velocity.x, rigidbody.velocity.z));
		}

		public void FixedTick(CameraMotor _cameraMotor)
		{
			HandleMovement(_cameraMotor);
			ApplyExtraGravity();
		}

		private void CheckGrounded()
		{
			// Use a smaller ground distance check if in air, to prevent suddenly snapping to ground
			float chosenGroundCheckDistance = GetGroundDistanceCheck(IsGrounded);

			// Check if the current time is greater than the required jump check time
			if(Time.time >= lastTimeInAir + IN_AIR_GROUND_CHECK_DELAY)
			{
				// Get all layers below us in the correct distance
				RaycastHit[] hits = CapsuleCastAllInDirection(-transform.up, chosenGroundCheckDistance);

				// If there is actually anything in the array, we can loop through it, otherwise, we aren't grounded
				if(hits.Length > 0)
				{
					foreach(RaycastHit hit in hits)
					{
						// Check if we are touching the ground, if so, set grounded to true and return
						if(hit.transform.CompareTag(groundTag) || hit.transform.gameObject.layer == 0)
						{
							IsGrounded = true;

							return;
						}

						IsGrounded = false;
					}
				}
				else
				{
					// We are standing above nothing so we aren't grounded
					IsGrounded = false;
				}
			}
		}

		private float GetGroundDistanceCheck(bool _isGrounded = false) => _isGrounded ? groundDistanceCheck : groundDistanceInAirCheck;

		private void HandleMovement(CameraMotor _cameraMotor)
		{
			// If the camera motor isn't running right now we shouldn't be able to control the player
			if(!_cameraMotor.enabled)
				return;

			// Calculate the max speed and the speed modifier by the grounded state
			float modifier = IsGrounded ? SPEED_ON_GROUND_MODIFIER : SPEED_IN_AIR_MODIFIER;

			// Calculate the correct velocity by the axis of the input
			Vector3 forward = _cameraMotor.BoomArm.forward * movement.y;
			Vector3 right = _cameraMotor.BoomArm.right * movement.x;
			Vector3 desiredVelocity = ((forward + right) * (maxSpeed * modifier)) - rigidbody.velocity;

			// Check we can move this way, if we can apply the velocity
			if(CanMoveInDirection(desiredVelocity))
				rigidbody.AddForce(new Vector3(desiredVelocity.x, 0, desiredVelocity.z), ForceMode.Impulse);

			movement = Vector2.zero;
		}

		private RaycastHit[] CapsuleCastAllInDirection(Vector3 _direction, float _distance)
		{
			// Calculate the top and bottom spheres of the capsule
			Vector3 top = transform.position + collider.center + Vector3.up * (collider.height * 0.5f - collider.radius);
			Vector3 bot = transform.position + collider.center - Vector3.up * (collider.height * 0.5f - collider.radius);

			// Cast the capsule in the passed direction and distance
			// ReSharper disable once Unity.PreferNonAllocApi
			return Physics.CapsuleCastAll(top, bot, collider.radius * 0.95f, _direction, _distance, layerChecks);
		}

		private void ApplyExtraGravity()
		{
			// Check if we are falling, if we are apply normal snappy fall
			if(rigidbody.velocity.y < 0)
			{
				rigidbody.velocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.deltaTime);
			}
			// We are rising, but we aren't pressing the jump button, so fall faster
			else if(rigidbody.velocity.y > 0 && !isJumpPressed)
			{
				rigidbody.velocity += Vector3.up * (Physics.gravity.y * lowJumpMultiplier * Time.deltaTime);
			}
		}

		private bool CanMoveInDirection(Vector3 _targetDir)
		{
			// Find everything in the direction we are attempting to move at least half a meter away
			RaycastHit[] hits = CapsuleCastAllInDirection(_targetDir, 0.5f);

			foreach(RaycastHit hit in hits)
			{
				if(hit.collider.CompareTag(wallTag) || hit.transform.gameObject.layer == 0)
				{
					// We will walk into a wall so don't move that way
					return false;
				}
			}

			// We can move this way since we won't walk into a wall
			return true;
		}
	}
}