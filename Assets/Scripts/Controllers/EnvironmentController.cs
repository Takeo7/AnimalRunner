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
    public bool isCaveBool = false;
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
	public List<EnvironmentSet> setsList;

	private void Start()
	{
		ResumeTime();
		GetEnvironments();//Take Scriptables EnvironmentSet from Resources on runtime
		SetEnvironment();//Set the first environment
        gameOverDelegate += EndGame;
        //gameOverDelegate += UploadUserDataEC;
        gameOverDelegate += CharacterReferences.instance.PC.CalculatePoints;
    }
    public void UploadUserDataEC()
    {
        PlayFabLogin.instance.UploadUserData(false);
    }
	private void Update()
	{
		if (caveMeters.Contains<int>(Mathf.RoundToInt(UIC.target.transform.position.x)) && isCaveBool == false)//If the character reached meters for a cave
		{
            isCaveBool = true;
			caveBool = true;//caveBool = true to let the generator know that the next prefab will be the cave
		}
	}
	//TimeControl
	public void StopTime()
	{
		Time.timeScale = 0;
	}
	public void ResumeTime()
	{
		Time.timeScale = 1;
	}
    public void GameOverDelegateFunc()
    {
        gameOverDelegate();
    }
	//FinishTimeControl
	public void UpdateEnvironments()
	{
		CharacterReferences CR = CharacterReferences.instance;
		int length = sets.Length;
		for (int i = 0; i < length; i++)
		{
			switch (sets[i].setType)
			{
				case SetType.Forest:
					if (CR.charactersInfo.characters[0].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Desert:
					if (CR.charactersInfo.characters[1].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Ice:
					if (CR.charactersInfo.characters[3].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Candy:
					if (CR.charactersInfo.characters[4].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
			}
		}
	}
	public void GetEnvironments()
	{
		CharacterReferences CR = CharacterReferences.instance;
		sets = Resources.LoadAll<EnvironmentSet>("Sets");//Take scriptables EnvironmentSet from Resources on runtime
		int length = sets.Length;
		for (int i = 0; i < length; i++)
		{
			switch (sets[i].setType)
			{
				case SetType.Forest:
					if (CR.charactersInfo.characters[0].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Desert:
					if (CR.charactersInfo.characters[1].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Ice:
					if (CR.charactersInfo.characters[3].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Lava:
					if (CR.charactersInfo.characters[2].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
				case SetType.Candy:
					if (CR.charactersInfo.characters[4].unlocked)
					{
						setsList.Add(sets[i]);
					}
					break;
			}
		}
	}
	public void SetEnvironment()
	{
		set = setsList[Random.Range(0, setsList.Count)];//Set the first environment
		GameObject spawn = Instantiate(set.specialPrefabs[0]);//Instantiate the Spawn
		prefabsInstantiated.Add(spawn.GetComponent<EnvironmentPrefabController>());//Add the spawn to the list of instantiated prefabs
	}
	public void ClearList()
	{
		prefabsInstantiated.Clear();
	}
    void SetNewEnvironment()
    {
        if (setsList.Count == 1)
        {
            set = setsList[0];
        }
        else
        {
            EnvironmentSet temp = setsList[Random.Range(0, setsList.Count)];//Set the first environment
                                                                            //Debug.Log(set.name + "  " + temp.name);
            while (set == temp)//Redo the random until we get an environment different to the actual
            {
                temp = setsList[Random.Range(0, setsList.Count)];
                //Debug.Log("IN "+set.name + "  " + temp.name);
            }
            set = temp;//Set the environment        }

        }
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
		//Debug.Log("GAME ENDED");
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
            //Debug.Log("NewCave");
            caveBool = false;
			prefabTemp = Instantiate(cavePrefab);//instantiate the prefab
            SetNewEnvironment();//Change environment
		}
		else//if the next prefab wont be a cave
		{
            isCaveBool = false;
			if (Random.value <= floorPercentage)
			{
				//Debug.Log("InstantiatedFloor");
				prefabTemp = Instantiate(set.floorPrefabs[Random.Range(0, set.floorPrefabs.Length)]);
			}
			else
			{
				//Debug.Log("InstantiatedFloating");
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
			//Debug.Log(prefabsInstantiated.Count);
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
			yield return new WaitForSeconds(2f);
			characterSpeed += 0.1f;
		}
	}
}
