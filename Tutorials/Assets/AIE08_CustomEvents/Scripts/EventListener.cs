using UnityEngine;

namespace AIE08_CustomEvents
{
	public class EventListener : MonoBehaviour
	{
		public void OnPlayerDeath(Player _player)
		{
			Debug.LogError("Oh dear, player done unalived");
		}

		public void OnPlayerDamaged(Player _player, float _damage)
		{
			Debug.Log($"The player took {_damage} damage");
		}
	}
}