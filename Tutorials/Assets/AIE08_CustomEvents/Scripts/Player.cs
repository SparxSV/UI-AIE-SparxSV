using System;

using UnityEngine;
using UnityEngine.Events;

namespace AIE08_CustomEvents
{
	[Serializable]
	public class PlayerDeathEvent : UnityEvent<Player> { }

	[Serializable] public class PlayerDamagedEvent : UnityEvent<Player, float> { }

	public class Player : MonoBehaviour
	{
		public PlayerDeathEvent deathEvent = new();
		public PlayerDamagedEvent damagedEvent = new();

		private float health = 10f;

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.D))
			{
				damagedEvent.Invoke(this, 1);
				health--;
				
				if(health <= 0)
					deathEvent.Invoke(this);
			}

			if(Input.GetKeyDown(KeyCode.K))
				deathEvent.Invoke(this);
		}
	}
}