using System;

using UnityEngine;

namespace UI_Layouts
{
	public class ActionList : MonoBehaviour
	{
		public Action[] actions;

		private void Start()
		{
			actions = GetComponents<Action>();
		}
	}
}