using System;

using TMPro;

using UI_EventSystems;

using UnityEngine;
using UnityEngine.UI;

namespace UI_Layouts
{
	public class ActionUI : MonoBehaviour
	{
		public Action action;

		[Header("Child Components")] 
		public Image icon;
		public TextMeshProUGUI nameTag;
		public TextMeshProUGUI descriptionTag;

		public Player player;
		
		private void Start()
		{
			SetAction(action);
		}

		public void SetAction(Action _a)
		{
			action = _a;

			if(nameTag)
				nameTag.text = action.actionName;

			if(descriptionTag)
				descriptionTag.text = action.description;

			if(icon)
			{
				icon.sprite = action.icon;
				icon.color = action.color;
			}
		}
	}
}