using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AIE03_Coroutines
{
	[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
	public class CoroutinesExtension : MonoBehaviour
	{
		[SerializeField, Range(0.1f, 5f)] private float interval = 1f;
		[SerializeField] private List<TransformPair> transforms;

		private void Start() => StartCoroutine(TransformUpdateLoop_CR());

		private void OnDrawGizmos()
		{
			foreach(TransformPair t in transforms)
			{
				if(t != null)
				{
					Matrix4x4 trans = Matrix4x4.TRS(t.transform.position, t.transform.rotation, t.transform.localScale);
					
					Gizmos.color = t.gizmoColor;
					Gizmos.matrix = trans;
					Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
				}
			}
		}

		private IEnumerator TransformUpdateLoop_CR()
		{
			int index = 0;

			while(true)
			{
				Transform target = transforms[index].transform;
				transform.position = target.position;
				transform.rotation = target.rotation;
				transform.localScale = target.localScale;
				
				yield return new WaitForSeconds(interval);

				index++;
				if(index == transforms.Count)
					index = 0;
			}
			
			// ReSharper disable once IteratorNeverReturns
		}
	}
}