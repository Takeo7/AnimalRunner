using System.Collections;
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
    public SoundController sc;
    public SoundController scSpecial;
    public GameObject especialCollider;
    

	private void Start()
	{
		CR = CharacterReferences.instance;
	}
	public void Special()
	{
        CR.TM.isSpecial = true;
		CR.PS.canDie = false;
		StartCoroutine("SpecialCO");
		CR.TM.AC.AttackAnim(true);
		particles.SetActive(true);
        especialCollider.SetActive(true);
        scSpecial.PlaySound(2,0.3f);
	}
	IEnumerator SpecialCO()
	{
		yield return new WaitForSeconds(10f);
		CR.PS.canDie = true;
		SpecialsUI.instance.SetCooldown();
		particles.SetActive(false);
        especialCollider.SetActive(false);
        StartCoroutine(scSpecial.FadeSoundCoroutine());
        CR.TM.isSpecial = false;
	}
}
