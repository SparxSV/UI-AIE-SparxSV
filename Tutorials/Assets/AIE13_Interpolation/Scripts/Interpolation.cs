using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AIE13_Interpolation
{
	public class Interpolation : MonoBehaviour
	{
		[SerializeField] private List<Transform> positions = new();
		[SerializeField, Range(0.1f, 5f)] private float tweenTime = 1f;
		[SerializeField] private AnimationCurve tweenCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

		[Header("Debugging")] 
		[SerializeField] private bool showGizmos = true;
		[SerializeField] private Gradient gizmoGradient = new Gradient();

		private int currentIndex;

		private void Start() => StartCoroutine(CurveTween_CR());

		private IEnumerator Tween_CR()
		{
			float timer = 0f;

			while(true)
			{
				Vector3 startPos = transform.position;
				Quaternion startRot = transform.rotation;
				Vector3 startScale = transform.localScale;
				
				while(timer < tweenTime)
				{
					// Lerp picks a point on the line based on the T value which is the time.
					// Calculates each position each frame incrementing the time.
					Vector3 pos = Vector3.Lerp(startPos, positions[currentIndex].position, Mathf.Clamp01(timer / tweenTime));
					Quaternion rot = Quaternion.Slerp(startRot, positions[currentIndex].rotation, Mathf.Clamp01(timer / tweenTime));
					Vector3 scale = Vector3.Lerp(startScale, positions[currentIndex].localScale, Mathf.Clamp01(timer / tweenTime));

					transform.position = pos;
					transform.rotation = rot;
					transform.localScale = scale;

					yield return null;

					timer += Time.deltaTime;
				}

				transform.position = positions[currentIndex].position;
				transform.rotation = positions[currentIndex].rotation;
				transform.localScale = positions[currentIndex].localScale;

				timer = 0;
				currentIndex++;

				if(currentIndex == positions.Count)
					currentIndex = 0;
			}
		}
		
		private IEnumerator CurveTween_CR()
		{
			float timer = 0f;

			while(true)
			{
				Vector3 startPos = transform.position;
				Quaternion startRot = transform.rotation;
				Vector3 startScale = transform.localScale;

				Vector3 endPos = positions[currentIndex].position;
				Quaternion endRot = positions[currentIndex].rotation;
				Vector3 endScale = positions[currentIndex].localScale;
				
				while(timer < tweenTime)
				{
					float factor = Mathf.Clamp01(timer / tweenTime);
					float t = tweenCurve.Evaluate(factor);
					
					// Lerp picks a point on the line based on the T value which is the time.
					// Calculates each position each frame incrementing the time.
					transform.position = Vector3.Lerp(startPos, endPos, t);
					transform.rotation = Quaternion.Slerp(startRot, endRot, t);
					transform.localScale = Vector3.Lerp(startScale, endScale, t);

					yield return null;

					timer += Time.deltaTime;
				}

				transform.position = positions[currentIndex].position;
				transform.rotation = positions[currentIndex].rotation;
				transform.localScale = positions[currentIndex].localScale;

				timer = 0;
				currentIndex++;

				if(currentIndex == positions.Count)
					currentIndex = 0;
			}
		}

		private void OnDrawGizmos()
		{
			if(!showGizmos && positions.Count > 0)
				return;
			
			Matrix4x4 gizmoMatDefault = Gizmos.matrix;
			float gradientStep = 1f / positions.Count;
			
			for(int index = 0; index < positions.Count; index++)
			{
				Transform position = positions[index];
				
				Gizmos.matrix = position.localToWorldMatrix;
				Gizmos.color = gizmoGradient.Evaluate(gradientStep * index);

				Gizmos.DrawCube(Vector3.zero, Vector3.one);
			}

			Gizmos.matrix = gizmoMatDefault;
		}
	}
}