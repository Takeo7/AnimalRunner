﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReferences : MonoBehaviour {

	#region Singleton
	public static CharacterReferences instance;
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}
	#endregion

	public CharactersInfo charactersInfo;
	public Transform characterTransform;
	public TestMovement TM;
	public PlayerStats PS;
	public AnimatorController AC;
	public GameObject gameObj;

	private void Start()
	{
		int charSelected = PlayerPrefs.GetInt("CharacterSelected");
		Destroy(gameObj);
		GameObject newChar = Instantiate(charactersInfo.characters[charSelected].prefab);
		NewReference(newChar.transform, newChar.GetComponent<TestMovement>(), newChar.GetComponent<PlayerStats>(), newChar.GetComponent<AnimatorController>(), newChar);
	}
	public void Jump()
	{
		TM.Jump();
	}
	public void Attack()
	{
		TM.AttackRanged();
	}

	public void NewReference(Transform newTransfrom,TestMovement newTM,PlayerStats newPS,AnimatorController newAC,GameObject newlast)
	{
		characterTransform = newTransfrom;
		TM = newTM;
		PS = newPS;
		AC = newAC;
		gameObj = newlast;
	}

}