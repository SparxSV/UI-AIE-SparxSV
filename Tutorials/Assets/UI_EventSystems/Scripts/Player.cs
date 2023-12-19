using UnityEngine;

namespace UI_EventSystems
{
	public class Player : MonoBehaviour
	{
		public void DoBlock(Block _block)
		{
			Debug.Log("You chose " + _block.actionName);
		}
	}
}