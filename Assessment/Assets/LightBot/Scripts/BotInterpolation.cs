using System.Collections;

using UnityEngine;

namespace LightBot
{
	public class BotInterpolation : MonoBehaviour
	{
		[Header("Interpolation Settings")]
		[SerializeField, Range(.1f, 5f)] private float tweenTime = 1f;
		[SerializeField] private AnimationCurve tweenCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		
		public bool inLightBlock = false;
		
		private int currentIndex;

		public IEnumerator Bot_ForwardCR()
		{
			float timer = 0f;
			
			Vector3 startPos = transform.position;

			Vector3 endPos = transform.position + transform.forward;

			while(timer < tweenTime)
			{
				float factor = Mathf.Clamp01(timer / tweenTime);
				float t = tweenCurve.Evaluate(factor);

				transform.position = Vector3.Lerp(startPos, endPos, t);

				yield return null;

				timer += Time.deltaTime;
			}

			transform.position = endPos;
		}

		public IEnumerator Bot_TurnRightCR()
		{
			float timer = 0f;

			Quaternion startRot = transform.rotation;

			Quaternion endRot = transform.rotation * Quaternion.Euler(0, 90, 0);

			while(timer < tweenTime)
			{
				float factor = Mathf.Clamp01(timer / tweenTime);
				float t = tweenCurve.Evaluate(factor);
				
				transform.rotation = Quaternion.Slerp(startRot, endRot, t);

				yield return null;

				timer += Time.deltaTime;
			}

			transform.rotation = endRot;
		}
		
		public IEnumerator Bot_TurnLeftCR()
		{
			float timer = 0f;

			Quaternion startRot = transform.rotation;

			Quaternion endRot = transform.rotation * Quaternion.Euler(0, -90, 0);

			while(timer < tweenTime)
			{
				float factor = Mathf.Clamp01(timer / tweenTime);
				float t = tweenCurve.Evaluate(factor);
					
				transform.rotation = Quaternion.Slerp(startRot, endRot, t);

				yield return null;

				timer += Time.deltaTime;
			}

			transform.rotation = endRot;
		}

		public void OnTriggerEnter(Collider _other)
		{
			if(_other.CompareTag("Light"))
			{
				Debug.LogWarning("I DON'T LIKE YOU");
				inLightBlock = !inLightBlock;
			}
		}

		public void OnTriggerExit(Collider _other)
		{
			inLightBlock = !inLightBlock;
		}
	}
}