using UnityEngine;
using UnityEngine.UI;

namespace AIE10_DragDropShop
{
	public class InventoryItemUI : Draggable
	{
		public InventoryItem Held => held;

		[SerializeField] private Image image;

		private InventoryItem held;
		
		public void SetItem(InventoryItem _item)
		{
			held = _item;
			if(image != null)
			{
				if(held != null)
				{
					image.sprite = held.icon;
					image.color = held.color;
				}
				
				gameObject.SetActive(held != null);
			}
		}

		protected override void Swap(Slot _slot)
		{
			InventoryItemUI other = _slot.itemUI;
			if(other != null)
			{
				InventoryItem ourItem = held;
				InventoryItem otherItem = other.held;

				slot.UpdateItem(otherItem);
				other.slot.UpdateItem(ourItem);
			}
		}
	}
}