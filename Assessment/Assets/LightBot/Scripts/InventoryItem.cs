using UnityEngine;

namespace LightBot
{
	[CreateAssetMenu(fileName = "New Instruction", menuName = "LightBot/Inventory Item")]
	public class InventoryItem : ScriptableObject
	{
		public enum Action
		{
			MoveForward,
			TurnLeft,
			TurnRight,
			LightUp
		};
		
		public string heading;
		public Sprite icon;
		public Color color = Color.white;

		public Action action;
	}
}