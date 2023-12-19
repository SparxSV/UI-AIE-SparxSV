using UnityEngine;

namespace AIE.Characters
{
	public class GroundObject : MonoBehaviour
	{
		private void Start()
		{
			gameObject.layer = LayerMask.NameToLayer("Ground");
			gameObject.tag = "Ground";
		}
	}
}