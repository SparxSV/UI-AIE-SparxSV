using UnityEngine;

namespace AIE10_DragDropShop
{
	[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "AIE/Inventory Item")]
	public class InventoryItem : ScriptableObject
	{
		public string title;
		[Multiline(3)] public string description;
		public Sprite icon;
		public Color color = Color.white;
	}
}