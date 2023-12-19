using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace LightBot
{
	public class Options : MonoBehaviour
	{
		[Header("GameObjects")]
		[SerializeField] private BotInterpolation bot;
		[SerializeField] private InventoryUI inventory;
		[SerializeField] private GridGenerator generator;

		private readonly Vector3 botPosition = new (0, 1, 0);
		private readonly Quaternion botRotation = Quaternion.Euler(0, 0, 0);

		public void QuitGame()
		{
			Application.Quit();
		}
		
		public void ReloadMap(GridGenerator _gridGenerator)
		{
			StopInterpolation();
			
			foreach(GameObject block in _gridGenerator.obstacleBlocks)
			{
				Destroy(block);
			}
			
			_gridGenerator.obstacleBlocks.Clear();
			_gridGenerator.RandomBlockSpawner();
			
			generator.lightBlock.gameObject.GetComponent<Renderer>().material.color = (new Color(0, 248, 255, 50) / 255);
		}

		public void StartInterpolation()
		{
			if(inventory.inventory.contents == null)
				throw new ArgumentOutOfRangeException();
			
			StartCoroutine(RunCommands_CR());
		}

		public void StopInterpolation()
		{
			StopAllCoroutines();
			ResetPosition();
		}
		
		public void ClearSlots()
		{
			inventory.ClearSlots(inventory.inventory);
		}

		public void ResetPosition()
		{
			bot.transform.position = botPosition;
			bot.transform.rotation = botRotation;
			
			Debug.Log("Position has been reset.");
		}

		// ReSharper disable Unity.PerformanceAnalysis
		private IEnumerator RunCommands_CR()
		{
			foreach(InventoryItem t in inventory.inventory.contents)
			{
				if(t != null)
				{
					switch(t.action)
					{
						case InventoryItem.Action.MoveForward:
							yield return StartCoroutine(bot.Bot_ForwardCR());

							break;

						case InventoryItem.Action.TurnLeft:
							yield return StartCoroutine(bot.Bot_TurnLeftCR());

							break;

						case InventoryItem.Action.TurnRight:
							yield return StartCoroutine(bot.Bot_TurnRightCR());

							break;

						case InventoryItem.Action.LightUp:
							if(bot.inLightBlock)
							{
								generator.lightBlock.gameObject.GetComponent<Renderer>().material.color = (new Color(255, 255, 0, 150) / 255);
								yield return new WaitForSeconds(2);	
							}

							break;

						default:
							throw new ArgumentOutOfRangeException();
					}
				}
			}

			ResetPosition();
			ClearSlots();
			ReloadMap(generator);
		}
	}
}