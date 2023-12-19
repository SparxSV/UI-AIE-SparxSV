using UnityEngine;

namespace AIE.Characters
{
	public class WallObject : MonoBehaviour
	{
		private void Start()
		{
			gameObject.layer = LayerMask.NameToLayer("Walls");
			gameObject.tag = "Walls";
		}
	}
}