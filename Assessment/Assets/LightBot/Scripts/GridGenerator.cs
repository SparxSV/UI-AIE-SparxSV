using System;
using System.Collections.Generic;

using UnityEngine;

using Random = UnityEngine.Random;

namespace LightBot
{
	public class GridGenerator : MonoBehaviour
	{
		[Header("-- Grid Size --")]
		[SerializeField] private int gridWidth = 5;
		[SerializeField] private int gridHeight = 5;
		[SerializeField, Range(5, 20)] private int blockAmount = 5;

		[Header("-- Block Prefabs --")]
		[SerializeField] private GameObject blockPrefab;
		[SerializeField] private GameObject obstaclePrefab;
		[SerializeField] private GameObject lightPrefab;

		[NonSerialized] public GameObject lightBlock;

		[Header("-- Block Lists --")]
		[SerializeField] public List<GameObject> obstacleBlocks;
		[SerializeField] public List<GameObject> blocks;

		private void Start()
		{
			for(int x = 0; x < gridWidth; x++)
			{
				for(int z = 0; z < gridHeight; z++)
				{
					GameObject block = Instantiate(blockPrefab, Vector3.zero, blockPrefab.transform.rotation);
					block.transform.parent = transform;
					block.transform.localPosition = new Vector3(x, 0, z);
					blocks.Add(block);
				}
			}
			
			RandomBlockSpawner();

			lightBlock = Instantiate(lightPrefab, Vector3.zero, lightPrefab.transform.rotation);
			lightBlock.transform.parent = transform;
			lightBlock.transform.localPosition = new Vector3(4, 1, 4);
		}

		public void RandomBlockSpawner()
		{
			for(int i = 0; i < blockAmount; i++)
			{
				GameObject obstacleBlock = Instantiate(obstaclePrefab, new Vector3(Random.Range(1, gridWidth - 1), 1, Random.Range(0, gridHeight)), obstaclePrefab.transform.rotation);
				obstacleBlock.transform.parent = transform;
				obstacleBlocks.Add(obstacleBlock);
			}
		}
	}
}