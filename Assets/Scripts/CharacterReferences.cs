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

	public CharacterInfo playerInfo;
	public CharactersInfo charactersInfo;
	public Transform characterTransform;
	public TestMovement TM;
	public PlayerStats PS;
	public AnimatorController AC;
	public GameObject gameObj;
    [Space]
    public PointsController PC;

	private void Start()
	{
		int charSelected = playerInfo.selectedCharacter;
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
		playerInfo.totalAttacks++;
	}

	public void Special()
	{
		playerInfo.totalSpecialUsed++;
		switch (PS.PlayerType)
		{
			case PlayerStats.Characters.Turtle:
				TurtleSpecial.instance.Special();
				break;
			case PlayerStats.Characters.Elephant:
				ElephantSpecial.instance.Special();
				break;
			case PlayerStats.Characters.Dragon:
				DragonSpecial.instance.Special();
				break;
			case PlayerStats.Characters.Rabbit:
				RabbitSpecial.instance.Special();
				break;
			case PlayerStats.Characters.Unicorn:
				UnicornSpecial.instance.Special();
				break;
			case PlayerStats.Characters.Okami:
				OkamiSpecial.instance.Special();
				break;
		}
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
