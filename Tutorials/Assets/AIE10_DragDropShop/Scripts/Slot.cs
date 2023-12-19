﻿using System;
using System.Reflection;

using UnityEngine;
using UnityEngine.EventSystems;

namespace AIE10_DragDropShop
{
	public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public InventoryItemUI itemUI;
		
		private InventoryUI parentInventory;
		private int index;
		private Draggable draggableItem;
		
		public void Init(InventoryUI _parent, int _index, Draggable _item)
		{
			parentInventory = _parent;
			index = _index;
			draggableItem = _item;
			draggableItem.slot = this;
		}

		public void UpdateItem(InventoryItem _item)
		{
			parentInventory.inventory.contents[index] = _item;
			itemUI.SetItem(_item);
		}

		public void OnPointerEnter(PointerEventData _eventData)
		{
			
		}

		public void OnPointerExit(PointerEventData _eventData)
		{
			
		}
	}
}