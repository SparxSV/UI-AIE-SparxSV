using UnityEngine;

namespace AIE05_ObjectManagement
{
	public class BeanSpawner : MonoBehaviour
	{
		[SerializeField, Range(1, 50)] private int beanAmount = 15;
		[SerializeField] private GameObject[] beanPrefabs;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
			
			for(int i = 0; i < beanAmount; i++)
			{
				GameObject newBean = Instantiate(beanPrefabs[Random.Range(0, beanPrefabs.Length)]);

				Vector2 pos = Random.insideUnitCircle.normalized * 5;
				newBean.transform.position = new Vector3(pos.x, newBean.transform.position.y + 1, pos.y);

				Vector3 heading = newBean.transform.position - beanPrefabs[0].transform.position;
				
				newBean.transform.forward = heading.normalized;
				//newBean.transform.Rotate(Vector3.up, 90);
			}
		}
	}
}