using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

	#region Singleton
	public static EnvironmentController instance;
	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}
	#endregion
	public List<EnvironmentPrefabController> prefabsInstantiated;
	public byte environmentSetLength;
	public EnvironmentSet set;

	public float timeToDestroy;
	public bool inGame;
	public bool canDestroy;
	public float characterSpeed;

	private void Start()
	{
		StartGame();		
	}

	void StartGame()
	{
		inGame = true;
		canDestroy = true;
		EnvironmentSet[] temp = Resources.LoadAll<EnvironmentSet>("Sets");
		set = temp[Random.Range(0, temp.Length)];
		environmentSetLength = (byte)set.floorPrefabs.Length;
		for (int i = 0; i < 3; i++)
		{
			InstantiatePrefab();
		}
		//StartCoroutine("StartSpawning", 1);
		StartCoroutine("RunSpeed");
	}
	void EndGame()
	{
		inGame = false;
		canDestroy = false;
		StopCoroutine("StartSpawning");
		StopCoroutine("RunSpeed");
		characterSpeed = 0;
	}

	void InstantiatePrefab()
	{
		GameObject temp = Instantiate(set.floorPrefabs[Random.Range(0, environmentSetLength)]);
		EnvironmentPrefabController epcTemp = temp.GetComponent<EnvironmentPrefabController>();
		epcTemp.transform.position = prefabsInstantiated[prefabsInstantiated.Count - 1].transform.position;
		float xSize = prefabsInstantiated[prefabsInstantiated.Count - 1].floorRenderer.bounds.size.x;
		float newXSize = epcTemp.floorRenderer.bounds.size.x;
		Vector3 newPos = new Vector3(temp.transform.position.x+(xSize/2)+(newXSize/2), temp.transform.position.y, 0);
		temp.transform.position = newPos;
		prefabsInstantiated.Add(epcTemp);
	}

	public void ChangeEnviron()
	{
		if(prefabsInstantiated.Count > 5)
		{
			EnvironmentPrefabController temp = prefabsInstantiated[0];
			prefabsInstantiated.Remove(prefabsInstantiated[0]);
			Destroy(temp.gameObject);
		}
		InstantiatePrefab();
	}

	IEnumerator StartSpawning(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			InstantiatePrefab();
		}
	}
	IEnumerator RunSpeed()
	{
		while (inGame)
		{
			yield return new WaitForSeconds(1);
			characterSpeed += 0.1f;
		}
	}
}
