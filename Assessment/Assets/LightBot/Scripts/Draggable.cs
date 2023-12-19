using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace LightBot
{
	public abstract class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public bool Dragging { get; private set; }

		public Slot slot;
		
		private Transform originalParent;
		private Canvas canvas;
		public void OnBeginDrag(PointerEventData _eventData)
		{
			if(originalParent == null)
				originalParent = transform.parent;

			if(canvas == null)
				canvas = gameObject.GetComponentInParent<Canvas>();
			
			transform.SetParent(canvas.transform, true);
			transform.SetAsLastSibling();

			Dragging = true;
		}

		public void OnDrag(PointerEventData _eventData)
		{
			if(Dragging)
				transform.position = _eventData.position;
		}

		public void OnEndDrag(PointerEventData _eventData)
		{
			Dragging = false;
			
			transform.SetParent(originalParent);
			transform.localPosition = Vector3.zero;

			List<RaycastResult> results = new();
			Slot found = null;
			EventSystem.current.RaycastAll(_eventData, results);

			foreach(RaycastResult result in results)
			{
				Slot s = result.gameObject.GetComponent<Slot>();

				if(s != null)
				{
					found = s;
					break;
				}
			}
			
			if(found != null)
				Swap(found);
			
			((RectTransform) transform).anchoredPosition = Vector2.zero;
		}

		protected abstract void Swap(Slot _slot);
	}
}