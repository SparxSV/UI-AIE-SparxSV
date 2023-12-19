using UnityEngine;

namespace AIE.Characters.Animation
{
	public class AnimationController : MonoBehaviour
	{
		private static readonly int speedHash = Animator.StringToHash("speed"); 
		
		[SerializeField] private Animator animator;

		public void OnMovement(Vector2 _movement)
		{
			animator.SetFloat(speedHash, _movement.magnitude);
		}
	}
}