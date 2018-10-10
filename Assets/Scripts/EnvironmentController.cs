using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

	public List<GameObject> prefabsInstantiated;
	public byte environmentSetLength;
	public EnvironmentSet set;
	public EnvironmentSet[] temp;

	private void Start()
	{
		temp = Resources.LoadAll<EnvironmentSet>("Sets");
		set = temp[Random.Range(0,temp.Length)];
		environmentSetLength = (byte)set.Prefabs.Length;
		StartCoroutine("StartSpawning",1);
	}

	void InstantiatePrefab()
	{
		GameObject temp = Instantiate(set.Prefabs[Random.Range(0, environmentSetLength)]);
		temp.transform.position = prefabsInstantiated[prefabsInstantiated.Count - 1].transform.position;
		float xSize = prefabsInstantiated[prefabsInstantiated.Count - 1].transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
		float newXSize = temp.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
		Vector3 newPos = new Vector3(temp.transform.position.x+(xSize/2)+(newXSize/2), temp.transform.position.y, 0);
		temp.transform.position = newPos;
		prefabsInstantiated.Add(temp);
	}

	IEnumerator StartSpawning(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			InstantiatePrefab();
		}
	}
}
