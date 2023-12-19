using UnityEngine;

namespace AIE10_DragDropShop
{
	public class InventoryUI : MonoBehaviour
	{
		public TooltipUI TooltipUI => tooltipUI;
		
		public Inventory inventory;
		[SerializeField] private InventoryItemUI itemPrefab;
		[SerializeField] private Slot slotPrefab;
		[SerializeField] private TooltipUI tooltipUI;

		private Slot[] slots;

		private void Start()
		{
			slots = new Slot[inventory.contents.Length];

			for(int i = 0; i < slots.Length; i++)
			{
				slots[i] = Instantiate(slotPrefab, transform);
				slots[i].itemUI = Instantiate(itemPrefab, slots[i].transform);
				slots[i].itemUI.SetItem(inventory.contents[i]);
				slots[i].Init(this, i, slots[i].itemUI);
			}
		}
	}
}