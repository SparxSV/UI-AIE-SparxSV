using UnityEngine;

namespace UI_EventSystems
{
	public class BlockList : MonoBehaviour
	{
		public Block[] blocks;

		private void Start()
		{
			blocks = GetComponents<Block>();
		}
	}
}