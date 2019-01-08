using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesController : MonoBehaviour {

	ChallengesScriptable[] challengesScriptables;
	List<ChallengesScriptable> killsScriptables = new List<ChallengesScriptable>();
	List<ChallengesScriptable> metersScriptables= new List<ChallengesScriptable>();
	public ChallengesScriptable[] currentChallenges = new ChallengesScriptable[3];

	int[] currentChallengesIndex = new int[3];//-1 == completed
	int[] currentChallengesProgress = new int[3];
	[SerializeField]
	bool[] currentChallengesDone = new bool[3];
	float metersRunGrounded = 0;
	float metersRunTotal = 0;
	bool showingCompleted = false;

	float currKills;

	byte xpToAdd;

	public Text[] dieChallenges = new Text[2];
	public GameObject[] dieChallengesDone = new GameObject[2];
	public Text[] newDieChallenges = new Text[3];
	public GameObject[] newDieChallengesImage = new GameObject[2];

	public GameObject[] medals;
	public GameObject[] newMedals;
	public GameObject[] medalsHolders;
	public MedalsHolderController[] MHC;
	public Text levelText;

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
		if(CR.playerInfo.challengesIndex[0] == 0 && CR.playerInfo.challengesIndex[1] == 0 && CR.playerInfo.challengesIndex[2] == 0)
		{
			CR.playerInfo.challengesIndex[0] = metersScriptables[Random.Range(0,metersScriptables.Count+1)].index;
			CR.playerInfo.challengesIndex[1] = killsScriptables[Random.Range(0, killsScriptables.Count + 1)].index;
		}
		currentChallengesIndex[0] = CR.playerInfo.challengesIndex[0];
		currentChallengesIndex[1] = CR.playerInfo.challengesIndex[1];
		//currentChallengesIndex[2] = PlayerPrefs.GetInt("Challenge2");

		for (int i = 0; i < 2; i++)//Set "i < 2" to "i < 3" when miscelaneous challenges added
		{
			currentChallenges[i] = challengesScriptables[currentChallengesIndex[i]];
		}

		currentChallengesProgress[0] = CR.playerInfo.challengesProgress[0];
		currentChallengesProgress[1] = CR.playerInfo.challengesProgress[1];
		//currentChallengesProgress[2] = PlayerPrefs.GetInt("Progress2");

		//Refill dead window with the challenges
		for (int i = 0; i < 2; i++)
		{
			dieChallenges[i].text = currentChallenges[i].name;
			dieChallengesDone[i].SetActive(false);
		}
		medalsHolders[CR.playerInfo.medalHolder].SetActive(true);
		MHC[CR.playerInfo.medalHolder].isExistent = true;
		MHC[CR.playerInfo.medalHolder].SetMedalsRef(this);
		for (int i = 0; i < CR.playerInfo.currentMedals; i++)
		{
			medals[i].SetActive(true);
		}
		levelText.text = CR.playerInfo.playerName + " Level: " + CR.playerInfo.playerLevel.ToString();

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
		xpToAdd += currentChallenges[pos].xp;
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
	void NewChallenge(byte pos)
	{
		currentChallenges[pos] = null;
		if (pos == 0)
		{
			CR.playerInfo.challengesIndex[0] = metersScriptables[Random.Range(0, metersScriptables.Count)].index;
		}
		else if(pos == 1)
		{
			CR.playerInfo.challengesIndex[1] = killsScriptables[Random.Range(0, killsScriptables.Count + 1)].index;
		}
	}
	public void DieChallengesCheck()
	{
		StartCoroutine("AddNewChallenges");
	}
	IEnumerator AddNewChallenges()
	{
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < 2; i++)
		{
			if (currentChallengesDone[i])
			{
				dieChallengesDone[i].SetActive(true);
			}
			else
			{
				dieChallengesDone[i].SetActive(false);
			}
		}
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < 2; i++)
		{
			if (currentChallenges[i] == null)
			{
				NewChallenge((byte)i);
				newDieChallenges[i].text = challengesScriptables[CR.playerInfo.challengesIndex[i]].name;
				newDieChallengesImage[i].SetActive(true);
				dieChallengesDone[i].SetActive(false);
			}
		}
		//Show the level going up
		for (int i = xpToAdd; i > 0; i--)
		{
			CR.playerInfo.currentMedals++;
			xpToAdd--;
			Debug.Log(CR.playerInfo.currentMedals - 1);
			newMedals[CR.playerInfo.currentMedals-1].SetActive(true);
			yield return new WaitForSeconds(1f);
			if ((CR.playerInfo.currentMedals) == CR.playerInfo.medalsToNextRank)
			{
				CR.playerInfo.medalHolder++;
				if(CR.playerInfo.medalHolder > 8)
				{
					CR.playerInfo.medalHolder = 0;
				}
				CR.playerInfo.playerLevel++;
				CR.playerInfo.currentMedals = 0;
				CR.playerInfo.medalsToNextRank++;
				if(CR.playerInfo.medalsToNextRank > 10)
				{
					CR.playerInfo.medalsToNextRank = 3;
				}
				medalsHolders[CR.playerInfo.medalHolder].SetActive(true);
				MHC[CR.playerInfo.medalHolder].isExistent = false;
				MHC[CR.playerInfo.medalHolder].SetTextRef(this);
				levelText.text = CR.playerInfo.playerName + " Level: " + CR.playerInfo.playerLevel.ToString();
			}
		}
	}

}
