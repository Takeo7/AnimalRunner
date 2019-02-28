using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpecial : MonoBehaviour {

	#region Singleton
	public static DragonSpecial instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	public GameObject specialBullet;
	CharacterReferences CR;
    public SoundController sc;

	public void Special()
	{
		CR = CharacterReferences.instance;
		StartCoroutine("SpecialCO");
	}
	IEnumerator SpecialCO()
	{
		CR.TM.AC.AttackAnim(true);
		//yield return new WaitForSeconds(CR.TM.AC.attackAnimDuration);
		Instantiate(specialBullet, CR.TM.bulletSpawnPoint.position, specialBullet.transform.rotation);
        sc.PlaySound(2);
		yield return new WaitForSeconds(5f);
		SpecialsUI.instance.SetCooldown();
	}
}
