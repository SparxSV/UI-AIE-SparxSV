using UnityEngine;

namespace UI_EventSystems
{
	public class BlockListUI : MonoBehaviour
	{
		public BlockList blockList;
		public BlockUI prefab;

		private Player player;

		private void Start()
		{
			player = blockList.GetComponent<Player>();
			foreach(Block b in blockList.blocks)
			{
				BlockUI ui = Instantiate(prefab, transform);
				ui.SetBlock(b);
				ui.Init(player);
			}
		}
	}
}