using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesController : MonoBehaviour {

	#region Singleton
	public static ChallengesController instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
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

	[SerializeField]
	float currKills;

	byte xpToAdd;

	public Text[] dieChallenges = new Text[2];
	public GameObject[] dieChallengesDone = new GameObject[2];
	public Text[] newDieChallenges = new Text[3];
	public GameObject[] newDieChallengesImage = new GameObject[2];
	public Text[] dieChallengesStars = new Text[2];
	public Text[] newDieChallengesStars = new Text[2];

	public GameObject[] medals;
	public GameObject[] newMedals;
	public GameObject[] medalsHolders;
	public MedalsHolderController[] MHC;
	public Text levelText;

	public Text challenge01Pause;
	public Text challenge02Pause;
	public Text challenge01PauseStars;
	public Text challenge02PauseStars;

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
			CR.playerInfo.challengesIndex[0] = metersScriptables[Random.Range(0,metersScriptables.Count)].index;
			CR.playerInfo.challengesIndex[1] = killsScriptables[Random.Range(0, killsScriptables.Count)].index;
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
			dieChallenges[i].text = LanguajesDic.instance.GetChallengesText(currentChallenges[i].index);
			if(currentChallenges[i].xp == 1)
			{
				dieChallengesStars[i].gameObject.SetActive(false);
			}
			else if(currentChallenges[i].xp > 1)
			{
				dieChallengesStars[i].gameObject.SetActive(true);
				dieChallengesStars[i].text = "x"+currentChallenges[i].xp;
			}
			dieChallengesDone[i].SetActive(false);
		}
		medalsHolders[CR.playerInfo.medalHolder].SetActive(true);
		MHC[CR.playerInfo.medalHolder].isExistent = true;
		MHC[CR.playerInfo.medalHolder].SetMedalsRef(this);
		for (int i = 0; i < CR.playerInfo.currentMedals; i++)
		{
			medals[i].SetActive(true);
		}
		levelText.text = PlayerPrefs.GetString("Username") + " "+LanguajesDic.instance.GetText(11)+": " + CR.playerInfo.playerLevel.ToString();

		//Refill pause window with the challenges
		challenge01Pause.text = LanguajesDic.instance.GetChallengesText(currentChallenges[0].index);
        challenge02Pause.text = LanguajesDic.instance.GetChallengesText(currentChallenges[1].index);
        if (currentChallenges[0].xp == 1)
		{
			challenge01PauseStars.gameObject.SetActive(false);
		}
		else if(currentChallenges[0].xp > 1)
		{
			challenge01PauseStars.gameObject.SetActive(true);
			challenge01PauseStars.text = "x" + currentChallenges[0].xp;
		}
		if (currentChallenges[1].xp == 1)
		{
			challenge02PauseStars.gameObject.SetActive(false);
		}
		else if (currentChallenges[1].xp > 1)
		{
			challenge02PauseStars.gameObject.SetActive(true);
			challenge02PauseStars.text = "x" + currentChallenges[1].xp;
		}


	}
    public void RefillCurrentChallengesText()
    {
        dieChallenges[0].text = LanguajesDic.instance.GetChallengesText(currentChallenges[0].index);
        dieChallenges[1].text = LanguajesDic.instance.GetChallengesText(currentChallenges[1].index);
        challenge01Pause.text = LanguajesDic.instance.GetChallengesText(currentChallenges[0].index);
        challenge02Pause.text = LanguajesDic.instance.GetChallengesText(currentChallenges[1].index);
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
		if (!currentChallengesDone[1])
		{
			CheckKills();
		}
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
		CR.playerInfo.totalChallengesCompleted++;
		xpToAdd += currentChallenges[pos].xp;
		currentChallengesDone[pos] = true;
		StartCoroutine(ChallengeCompletedCO(pos));
	}
	IEnumerator ChallengeCompletedCO(byte pos)
	{
		yield return new WaitForSeconds(0.5f);
		//Debug.Log("showThis "+pos);
		ShowChallengeCompletedUI(pos);
		yield return new WaitForSeconds(1f);
		showingCompleted = false;
	}
	void ShowChallengeCompletedUI(byte pos)
	{
		CCUIC.text.text = LanguajesDic.instance.GetChallengesText(currentChallenges[pos].index);
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
			CR.playerInfo.challengesIndex[1] = killsScriptables[Random.Range(0, killsScriptables.Count)].index;
		}
	}
	public void DieChallengesCheck()
	{
		StartCoroutine("AddNewChallenges");
	}
	IEnumerator AddNewChallenges()
	{
		yield return new WaitForSeconds(1f);
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
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < 2; i++)
		{
			if (currentChallenges[i] == null)
			{
				NewChallenge((byte)i);
				newDieChallenges[i].text = challengesScriptables[CR.playerInfo.challengesIndex[i]].name;
				if(challengesScriptables[CR.playerInfo.challengesIndex[i]].xp == 1)
				{
					newDieChallengesStars[i].gameObject.SetActive(false);
				}
				else if(challengesScriptables[CR.playerInfo.challengesIndex[i]].xp > 1)
				{
					newDieChallengesStars[i].gameObject.SetActive(true);
					newDieChallengesStars[i].text = "x" + challengesScriptables[CR.playerInfo.challengesIndex[i]].xp;
				}
				newDieChallengesImage[i].SetActive(true);
				dieChallengesDone[i].SetActive(false);
			}
		}
		//Show the level going up
		for (int i = xpToAdd; i > 0; i--)
		{
			CR.playerInfo.currentMedals++;
			xpToAdd--;
			//Debug.Log(CR.playerInfo.currentMedals - 1);
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
                MHC[CR.playerInfo.medalHolder].SetMedalsRef(this);
				ShopConfirmer.instance.DoAttack();
			}
		}
		MainMenuAnimator MMA = MainMenuAnimator.instance;
		MMA.isLevel = true;
		Debug.Log("isLevel");
		if(MMA.isPoints && MMA.isLevel)
		{
			MMA.GoBackButton();
		}
	}

}
