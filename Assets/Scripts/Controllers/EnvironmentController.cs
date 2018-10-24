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

	public bool inGame;
	public bool canDestroy;
	public float characterSpeed;
	public float floorPercentage = 0.7f;
	public float floatingPercentage = 0.3f;

    public delegate void GameOverDelegate();
    public GameOverDelegate gameOverDelegate;


	public UIController UIC;
	EnvironmentSet[] sets;

	private void Start()
	{
		GetEnvironments();//Take Scriptables EnvironmentSet from Resources on runtime
		SetEnvironment();//Set the first environment
        gameOverDelegate += EndGame;
	}
	private void Update()
	{
		if (caveMeters.Contains<int>(Mathf.RoundToInt(UIC.target.transform.position.x)))//If the character reached meters for a cave
		{
			caveBool = true;//caveBool = true to let the generator know that the next prefab will be the cave
		}
	}
	void GetEnvironments()
	{
		sets = Resources.LoadAll<EnvironmentSet>("Sets");//Take scriptables EnvironmentSet from Resources on runtime
	}
	void SetEnvironment()
	{
		set = sets[Random.Range(0, sets.Length)];//Set the first environment
		GameObject spawn = Instantiate(set.specialPrefabs[0]);//Instantiate the Spawn
		prefabsInstantiated.Add(spawn.GetComponent<EnvironmentPrefabController>());//Add the spawn to the list of instantiated prefabs
	}
    void SetNewEnvironment()
    {
		EnvironmentSet temp = sets[Random.Range(0, sets.Length)];//Set the first environment
		//Debug.Log(set.name + "  " + temp.name);
		while (set == temp)//Redo the random until we get an environment different to the actual
		{
			temp = sets[Random.Range(0, sets.Length)];
			//Debug.Log("IN "+set.name + "  " + temp.name);
		}
		set = temp;//Set the environment
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
    public void TriggerEndGame()
    {
        gameOverDelegate();
    }

	void InstantiatePrefab()
	{
		if (caveBool)//if the next prefab must be a cave
		{
			caveBool = false;
			prefabTemp = Instantiate(cavePrefab);//instantiate the prefab
            SetNewEnvironment();//Change environment
		}
		else//if the next prefab wont be a cave
		{
			if (Random.value <= floorPercentage)
			{
				Debug.Log("InstantiatedFloor");
				prefabTemp = Instantiate(set.floorPrefabs[Random.Range(0, set.floorPrefabs.Length)]);
			}
			else
			{
				Debug.Log("InstantiatedFloating");
				prefabTemp = Instantiate(set.floatingPrefabs[Random.Range(0, set.floatingPrefabs.Length)]);
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
