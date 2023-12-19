using System;

using UI_EventSystems;

using UnityEngine;

namespace UI_Layouts
{
	public class ActionListUI : MonoBehaviour
	{
		public ActionList actionList;
		public ActionUI prefab;

		public Player player;
		
		private void Start()
		{
			player = actionList.GetComponent<Player>();
			foreach(Action a in actionList.actions)
			{
				ActionUI ui = Instantiate(prefab, transform);
				ui.SetAction(a);
			}
		}
	}
}