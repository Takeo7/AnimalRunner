using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengesController : MonoBehaviour {

	ChallengesScriptable[] challengesScriptables;
	List<ChallengesScriptable> killsScriptables = new List<ChallengesScriptable>();
	List<ChallengesScriptable> metersScriptables= new List<ChallengesScriptable>();
	public ChallengesScriptable[] currentChallenges = new ChallengesScriptable[3];

	int[] currentChallengesIndex = new int[3];//-1 == completed
	int[] currentChallengesProgress = new int[3];
	bool[] currentChallengesDone = new bool[3];
	float metersRunGrounded = 0;
	float metersRunTotal = 0;
	bool showingCompleted = false;

	float currKills;
	

	public UIController UIC;
	public CharacterReferences CR;
	public EnvironmentController EC;
	public ChallengeCompletedUIController CCUIC;

	void Start()
	{
		LoadScriptables();
		RefillCurrentChallenges();
	}
	void Update()
	{	
		if (EC.inGame)
		{
			CheckMeters();
		}
		if (Input.GetKeyDown(KeyCode.T))//Complete meters
		{
			metersRunTotal += currentChallenges[0].metersToRun;
			metersRunGrounded += currentChallenges[0].metersToRun;
		}
		if (Input.GetKeyDown(KeyCode.Y))//Complete kills
		{
			AddKills((byte)currentChallenges[1].unitsToKill);
		}
	}
	void LoadScriptables()
	{
		ChallengesScriptable[] challengesScriptablesTemp = Resources.LoadAll<ChallengesScriptable>("Challenges");//Take scriptables EnvironmentSet from Resources on runtime
		int length = challengesScriptablesTemp.Length;
		ChallengesScriptable[] challengesScriptablesNew = new ChallengesScriptable[length];
		for (int i = 0; i < length; i++)
		{
			for (int x = 0; x < length; x++)
			{
				if(challengesScriptablesTemp[x].index == i)
				{
					challengesScriptablesNew[i] = challengesScriptablesTemp[x];
					break;
				}
			}
		}
		challengesScriptables = challengesScriptablesNew;
		int length2 = challengesScriptables.Length;
		for (int i = 0; i < length2; i++)
		{
			if(challengesScriptables[i].type == challengeType.Meters)
			{
				metersScriptables.Add(challengesScriptables[i]);
			}
			else if (challengesScriptables[i].type == challengeType.Killing)
			{
				killsScriptables.Add(challengesScriptables[i]);
			}
		}

	}
	void RefillCurrentChallenges()
	{
		if((PlayerPrefs.GetInt("Challenge0") == 0) && (PlayerPrefs.GetInt("Challenge1") == 0) && (PlayerPrefs.GetInt("Challenge2") == 0))
		{
			PlayerPrefs.SetInt("Challenge0",metersScriptables[Random.Range(0,metersScriptables.Count+1)].index);
			PlayerPrefs.SetInt("Challenge1", killsScriptables[Random.Range(0, killsScriptables.Count + 1)].index);
		}
		currentChallengesIndex[0] = PlayerPrefs.GetInt("Challenge0");
		currentChallengesIndex[1] = PlayerPrefs.GetInt("Challenge1");
		//currentChallengesIndex[2] = PlayerPrefs.GetInt("Challenge2");

		for (int i = 0; i < 2; i++)//Set "i < 2" to "i < 3" when miscelaneous challenges added
		{
			currentChallenges[i] = challengesScriptables[currentChallengesIndex[i]];
		}

		currentChallengesProgress[0] = PlayerPrefs.GetInt("Progress0");
		currentChallengesProgress[1] = PlayerPrefs.GetInt("Progress1");
		//currentChallengesProgress[2] = PlayerPrefs.GetInt("Progress2");
	}
	void CheckMeters()
	{
		if (currentChallengesDone[0] == false)
		{
			if (currentChallenges[0].mustBeOnce && !currentChallenges[0].mustBeGrounded)
			{
				if (currentChallenges[0].metersToRun == UIC.currentMeters)
				{
					ChallengeCompleted(0);
				}
			}
			else if (currentChallenges[0].mustBeGrounded && !currentChallenges[0].mustBeOnce)
			{
				if (CR.TM.isGrounded)
				{
					metersRunGrounded += EC.characterSpeed * Time.deltaTime;
					if ((metersRunGrounded + currentChallengesProgress[0]) >= currentChallenges[0].metersToRun)
					{
						ChallengeCompleted(0);
					}
				}
			}
			else if (currentChallenges[0].mustBeGrounded && currentChallenges[0].mustBeOnce)
			{
				if (CR.TM.isGrounded)
				{
					metersRunGrounded += EC.characterSpeed * Time.deltaTime;
					if (metersRunGrounded >= currentChallenges[0].metersToRun)
					{
						ChallengeCompleted(0);
					}
				}
			}
			else if (!currentChallenges[0].mustBeGrounded && !currentChallenges[0].mustBeOnce)
			{
				if (currentChallenges[0].metersToRun <= (UIC.currentMeters + currentChallengesProgress[0]))
				{
					ChallengeCompleted(0);
				}
			}
		}
	}
	public void AddKills(byte kills)
	{
		currKills += kills;
		CheckKills();
	}
	void CheckKills()
	{
		if (currentChallenges[1].mustBeOnce)
		{
			if(currKills >= currentChallenges[1].unitsToKill)
			{
				ChallengeCompleted(1);
			}
		}
		else if (!currentChallenges[1].mustBeOnce)
		{
			if ((currKills + currentChallengesProgress[1]) >= currentChallenges[1].unitsToKill)
			{
				ChallengeCompleted(1);
			}
		}
	}
	void ChallengeCompleted(byte pos)
	{
		//give XP
		currentChallengesDone[pos] = true;
		StartCoroutine(ChallengeCompletedCO(pos));
	}
	IEnumerator ChallengeCompletedCO(byte pos)
	{
		yield return new WaitForSeconds(1f);
		//Debug.Log("showThis "+pos);
		ShowChallengeCompletedUI(pos);
		yield return new WaitForSeconds(1f);
		showingCompleted = false;
	}
	void ShowChallengeCompletedUI(byte pos)
	{
		CCUIC.text.text = currentChallenges[pos].name;
		CCUIC.SetIcon(pos);
		CCUIC.gameObject.SetActive(true);
		currentChallenges[pos] = null;
	}
	void GiveNewChallengeOnFinish()
	{
		for (int i = 0; i < 2; i++)
		{
			if(currentChallenges[i] == null)
			{
				NewChallenge((byte)i);
			}
		}
	}
	void NewChallenge(byte pos)
	{
		currentChallenges[pos] = null;
		if (pos == 0)
		{
			PlayerPrefs.SetInt("Challenge0", metersScriptables[Random.Range(0, metersScriptables.Count + 1)].index);
		}
		else if(pos == 1)
		{
			PlayerPrefs.SetInt("Challenge1", killsScriptables[Random.Range(0, killsScriptables.Count + 1)].index);
		}
	}

}
