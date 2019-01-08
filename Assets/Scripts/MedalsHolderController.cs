using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalsHolderController : MonoBehaviour {

	ChallengesController CC;
	public GameObject[] medals;
	public GameObject[] newMedals;
	public bool isExistent;
	public Animator animator;
	public Text levelText;

	public void SetMedalsRef(ChallengesController CC_)
	{
		CC = CC_;
		CC.medals = medals;
		CC.newMedals = newMedals;
		CC.levelText = levelText;
	}
	public void SetTextRef(ChallengesController CC_)
	{
		CC = CC_;
		CC.levelText = levelText;
	}
	private void Start()
	{
		if (isExistent)
		{
			animator.SetTrigger("Existent");
		}
		else
		{
			animator.SetTrigger("New");
		}
	}
}
