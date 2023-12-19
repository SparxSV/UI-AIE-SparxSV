using System;

using UnityEngine;

namespace AIE12_Animation
{
	[RequireComponent(typeof(Animator))]
	public class CharacterAnimation : MonoBehaviour
	{
		private static int walkHash = Animator.StringToHash("IsWalking");
		
		private Animator animator;

		private void Start()
		{
			animator = gameObject.GetComponent<Animator>();
		}

		private void Update()
		{
			animator.SetBool(walkHash, Input.GetKey(KeyCode.Space));
		}
	}
}