using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
	public EnvironmentSet set;
	public GameObject cavePrefab;
	public int[] caveMeters;
	public bool caveBool = true;
	GameObject prefabTemp;

	byte floatingPrefabsLength;
	byte floorPrefabsLength;

	public bool inGame;
	public bool canDestroy;
	public float characterSpeed;


	public UIController UIC;

	private void Start()
	{
		SetEnvironment();		
	}
	private void Update()
	{
		if (caveMeters.Contains<int>(Mathf.RoundToInt(UIC.target.transform.position.x)))
		{
			caveBool = true;
		}
	}
	void SetEnvironment()
	{
		EnvironmentSet[] temp = Resources.LoadAll<EnvironmentSet>("Sets");
		set = temp[Random.Range(0, temp.Length)];
		floatingPrefabsLength = (byte)set.floatingPrefabs.Length;
		floorPrefabsLength = (byte)set.floorPrefabs.Length;
		GameObject spawn = Instantiate(set.specialPrefabs[0]);
		prefabsInstantiated.Add(spawn.GetComponent<EnvironmentPrefabController>());
	}
	public void SetEnvironment(EnvironmentSet newSet)
	{
		set = newSet;
		floatingPrefabsLength = (byte)set.floatingPrefabs.Length;
		floorPrefabsLength = (byte)set.floorPrefabs.Length;
	}
	public void StartGame()
	{
		inGame = true;
		canDestroy = true;
		for (int i = 0; i < 3; i++)
		{
			InstantiatePrefab();
		}
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
		if (caveBool)
		{
			caveBool = false;
			prefabTemp = Instantiate(cavePrefab);
		}
		else
		{
			int rand = Random.Range(0, 3);
			if (rand == 1)
			{
				Debug.Log("InstantiatedFloating");
				prefabTemp = Instantiate(set.floatingPrefabs[Random.Range(0, set.floatingPrefabs.Length)]);
			}
			else
			{
				Debug.Log("InstantiatedFloor");
				prefabTemp = Instantiate(set.floorPrefabs[Random.Range(0, set.floorPrefabs.Length)]);
			}
		}
		EnvironmentPrefabController epcTemp = prefabTemp.GetComponent<EnvironmentPrefabController>();
		epcTemp.transform.position = prefabsInstantiated[prefabsInstantiated.Count - 1].transform.position;
		float xSize = prefabsInstantiated[prefabsInstantiated.Count - 1].floorRenderer.bounds.size.x;
		float newXSize = epcTemp.floorRenderer.bounds.size.x;
		Vector3 newPos = new Vector3(prefabTemp.transform.position.x+(xSize/2)+(newXSize/2), prefabTemp.transform.position.y, 0);
		prefabTemp.transform.position = newPos;
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
	IEnumerator RunSpeed()
	{
		while (inGame)
		{
			yield return new WaitForSeconds(1);
			characterSpeed += 0.1f;
		}
	}
}
