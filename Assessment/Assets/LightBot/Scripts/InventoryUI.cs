using TMPro;

using UnityEngine;

namespace LightBot
{
	public class InventoryUI : MonoBehaviour
	{
		public Inventory inventory;
		
		[SerializeField] private InventoryItemUI itemPrefab;
		[SerializeField] private Slot slotPrefab;

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

		public void ClearSlots(Inventory _inventory)
		{
			for(int i = 0; i < _inventory.contents.Length; i++)
			{
				if(_inventory.contents[i] == null)
					continue;
				
				slots[i].itemUI.gameObject.SetActive(false);
				_inventory.contents[i] = null;

				Debug.Log($"Element {i + 1} has been cleared.");
			}
		}
	}
}