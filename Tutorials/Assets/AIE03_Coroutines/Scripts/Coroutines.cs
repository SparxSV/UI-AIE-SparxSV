using System.Collections;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AIE03_Coroutines
{
	[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
	[AddComponentMenu("AIE/Tutorials/Coroutine Tutorial")]
	public class Coroutines : MonoBehaviour
	{
		[SerializeField, Range(0.1f, 5f), Tooltip("The time between swapping transforms.")] 
		private float interval = 1f;
		
		[Header("Transforms")]
		[SerializeField] private Transform start;
		[SerializeField] private Transform end;
		
		private void Start()
		{
		#if UNITY_EDITOR
			if(start == null || end == null)
			{
				Debug.Log("Start or End not set");
				EditorApplication.isPlaying = false;

				return;
			}
		#endif
			
			StartCoroutine(PingPongBetween_CR());
		}

		private void OnDrawGizmos()
		{
			if(start != null && end != null)
			{
				Matrix4x4 gizmoDefault = Gizmos.matrix;
				Matrix4x4 startMat = Matrix4x4.TRS(start.position, start.rotation, start.localScale);
				Matrix4x4 endMat = Matrix4x4.TRS(end.position, end.rotation, end.localScale);
			
				Gizmos.color = Color.red;
				Gizmos.matrix = startMat;
				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

				Gizmos.color = Color.green;
				Gizmos.matrix = endMat;
				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

				Gizmos.matrix = gizmoDefault;
				Gizmos.color = Color.white;
				Gizmos.DrawLine(start.position, end.position);
			}	
		}

		private IEnumerator PingPongBetween_CR()
		{
			Transform target = start;
			bool targetingStart = true;

			while(true)
			{
				transform.position = target.position;
				transform.rotation = target.rotation;
				transform.localScale = target.localScale;

				yield return new WaitForSeconds(interval);

				targetingStart = !targetingStart;
				target = targetingStart ? start : end;
			}
			
			// ReSharper disable once IteratorNeverReturns
		}
	}
}