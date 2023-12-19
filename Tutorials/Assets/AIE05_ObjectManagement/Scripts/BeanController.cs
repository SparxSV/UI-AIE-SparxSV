using System;

using UnityEngine;

namespace AIE05_ObjectManagement
{
	public class BeanController : MonoBehaviour
	{
		private void OnMouseUpAsButton()
		{
			Destroy(gameObject);
		}
	}
}