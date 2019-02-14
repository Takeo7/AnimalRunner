﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkamiSpecial : MonoBehaviour {

	#region Singleton
	public static OkamiSpecial instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
	CharacterReferences CR;
	public GameObject particles;
	private void Start()
	{
		CR = CharacterReferences.instance;
	}
	public void Special()
	{
		CR.PS.canDie = false;
		StartCoroutine("SpecialCO");
		CR.TM.AC.AttackAnim(true);
		particles.SetActive(true);
	}
	IEnumerator SpecialCO()
	{
		yield return new WaitForSeconds(10f);
		CR.PS.canDie = true;
		SpecialsUI.instance.SetCooldown();
		particles.SetActive(false);
	}
}