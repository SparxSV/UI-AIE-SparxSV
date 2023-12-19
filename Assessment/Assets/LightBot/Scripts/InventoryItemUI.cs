using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace LightBot
{
	public class InventoryItemUI : Draggable
	{
		[SerializeField] private Image image;
		[SerializeField] private TextMeshProUGUI text;

		private InventoryItem held;
		
		public void SetItem(InventoryItem _item)
		{
			held = _item;
			
			if(image != null && text != null)
			{
				if(held != null)
				{
					image.sprite = held.icon;
					image.color = held.color;
					text.text = held.heading;
				}
				
				gameObject.SetActive(held != null);
			}
		}
		
		public void DeleteItem()
		{
			gameObject.SetActive(held == false);
		}
		
		protected override void Swap(Slot _slot)
		{
			InventoryItemUI other = _slot.itemUI;

			if(other != null && slot.canClone)
			{
				InventoryItem ourItem = held;
				
				slot.UpdateItem(ourItem);
				other.slot.UpdateItem(ourItem);
			}
		}
	}
}