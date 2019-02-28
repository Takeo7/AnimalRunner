using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpecial : MonoBehaviour {

	#region Singleton
	public static TurtleSpecial instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	CharacterReferences CR;
	bool isRotating;
	Transform rotationTarget;
	public float rotationSpeed;
    public SoundController sc;

    private void Start()
    {
        CR = CharacterReferences.instance;
    }

    public void Special()
	{
        CR.TM.isSpecial = true;
		StartCoroutine("AnimTo0");
		if(CR == null)
		{
			CR = CharacterReferences.instance;
			//rotationTarget = CR.AC.anim.transform;
		}
		CR.AC.SpecialAttack();//Set Anim
        sc.PlaySound(2);
		isRotating = true;
		CR.PS.canDie = false;
		CR.AC.isAnimatingSpine = false;
		StartCoroutine("EndSpecialCO");
	}
	public void EndSpecial()
	{
		CR.AC.isAnimatingSpine = true;
		CR.AC.anim.timeScale = 0;
		CR.AC.RunAnim();
		isRotating = false;
		//rotationTarget.localEulerAngles = Vector3.zero;
		CR.PS.canDie = true;
		SpecialsUI.instance.SetCooldown();
        CR.TM.isSpecial = false;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Special();
		}
		if (!isRotating)
			return;
		//rotationTarget.Rotate(new Vector3(0, -(1 * rotationSpeed * Time.deltaTime),0), Space.Self);
	}
	IEnumerator AnimTo0()
	{
		yield return new WaitForSeconds(1f);
		CR.AC.anim.timeScale = 0;
	}
	IEnumerator EndSpecialCO()
	{
		yield return new WaitForSeconds(5f);
		EndSpecial();
	}
}
