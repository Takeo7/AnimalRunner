﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ShopConfirmer : MonoBehaviour {
	
	#region Singleton
	public static ShopConfirmer instance;
	private void Awake()
	{

		instance = this;
	}
	#endregion

	public GameObject character;
	public SpineShopAnimator SSA;
	public Transform characterPos;
	public GameObject popUp;
	public ShopItem SI;

	public GameObject cam;
	public GameObject[] characterList;

	public bool doit;

	private void Update()
	{
		if (doit)
		{
			doit = false;
			DoAttack();
		}
	}
	public void PurchaseCharacter(bool coins)
	{
		if (coins)
		{
			//Coin purchase
			SI.SelectCharacter(true);
		}
		else
		{
			//Gem purchase
			SI.SelectCharacter(false);
		}
	}
	public void HideCameraAndChar()
	{
		if(character != null)
		{
			character.SetActive(false);
		}
		cam.SetActive(false);
	}
	public void InstantiateCharacter(byte index)
	{
		if(character != null)
		{
			Destroy(character);
		}
		character = Instantiate(characterList[index],characterPos);

	}
	public void DoAttack()
	{
		SSA.Attack();
	}
}
