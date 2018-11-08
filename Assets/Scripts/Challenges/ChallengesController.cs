using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengesController : MonoBehaviour {

	ChallengesScriptable[] challengesScriptables;
	public ChallengesScriptable[] currentChallenges = new ChallengesScriptable[3];

	int[] currentChallengesIndex = new int[3];//-1 == completed
	int[] currentChallengesProgress = new int[3];

	float metersRunGrounded = 0;
	float metersRunTotal = 0;
	bool showingCompleted = false;

	public UIController UIC;
	public TestMovement CC;
	public EnvironmentController EC;

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

	}
	void RefillCurrentChallenges()
	{
		currentChallengesIndex[0] = PlayerPrefs.GetInt("Challenge0");
		currentChallengesIndex[1] = PlayerPrefs.GetInt("Challenge1");
		currentChallengesIndex[1] = PlayerPrefs.GetInt("Challenge2");

		for (int i = 0; i < 3; i++)
		{
			currentChallenges[i] = challengesScriptables[currentChallengesIndex[i]];
		}

		currentChallengesProgress[0] = PlayerPrefs.GetInt("Progress0");
		currentChallengesProgress[1] = PlayerPrefs.GetInt("Progress1");
		currentChallengesProgress[2] = PlayerPrefs.GetInt("Progress2");
	}
	void CheckMeters()
	{
		if (currentChallenges[0].mustBeOnce && !currentChallenges[0].mustBeGrounded)
		{
			if(currentChallenges[0].metersToRun == UIC.currentMeters)
			{
				ChallengeCompleted(0);
			}
		}
		else if (currentChallenges[0].mustBeGrounded && !currentChallenges[0].mustBeOnce)
		{
			if (CC.isGrounded)
			{
				metersRunGrounded += EC.characterSpeed * Time.deltaTime;
				if((metersRunGrounded + currentChallengesProgress[0]) >= currentChallenges[0].metersToRun)
				{
					ChallengeCompleted(0);
				}
			}
		}
		else if(currentChallenges[0].mustBeGrounded && currentChallenges[0].mustBeOnce)
		{
			if (CC.isGrounded)
			{
				metersRunGrounded += EC.characterSpeed * Time.deltaTime;
				if (metersRunGrounded >= currentChallenges[0].metersToRun)
				{
					ChallengeCompleted(0);
				}
			}
		}
		else if(!currentChallenges[0].mustBeGrounded && !currentChallenges[0].mustBeOnce)
		{
			if (currentChallenges[0].metersToRun <= (UIC.currentMeters+currentChallengesProgress[0]))
			{
				ChallengeCompleted(0);
			}
		}
	}
	void ChallengeCompleted(byte pos)
	{
		//give XP
		while (showingCompleted == true)
		{

		}
		currentChallenges[pos] = null;
		StartCoroutine(ChallengeCompletedCO(pos));
	}
	IEnumerator ChallengeCompletedCO(byte pos)
	{
		showingCompleted = true;

		yield return new WaitForSeconds(1f);
		Debug.Log("showThis");
		ShowChallengeCompletedUI(pos);
		yield return new WaitForSeconds(1f);
		showingCompleted = false;

	}
	void ShowChallengeCompletedUI(byte pos)
	{
		//Fill Challenge Completed OBJ
		//Activate ChallengeCompleted OBJ
	}

}
