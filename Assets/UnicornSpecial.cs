using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornSpecial : MonoBehaviour {

	#region Singleton
	public static UnicornSpecial instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	public GameObject meteorPrefab;
	public GameObject particles;
	public bool isActive;

	public void Special()
	{
		CharacterReferences.instance.TM.AC.AttackAnim(true);
		particles.SetActive(true);
		isActive = true;
		StartCoroutine("SpecialCO");
	}
	IEnumerator SpecialCO()
	{
		yield return new WaitForSeconds(10f);
		isActive = false;
		SpecialsUI.instance.SetCooldown();
		particles.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Enemy") && isActive)
		{
			//Debug.Log("METEOR INSTANTIATED");
			Instantiate(meteorPrefab, col.transform.position, meteorPrefab.transform.rotation);
		}
	}
}
