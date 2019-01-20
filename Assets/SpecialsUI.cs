using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialsUI : MonoBehaviour {

	PlayerStats ps;

	public Image ColdownImage;
	public Image ButtonImage;

	public Color coldownColor;
	public Color avaiableColor;
	public Color buttonColor;

	public float cooldownTime;

	public void SetCooldown()
	{
		StartCoroutine("CooldownAttack");
	}
	IEnumerator CooldownAttack()
	{
		float actualColdown = 0;
		ButtonImage.color = buttonColor;
		ColdownImage.color = coldownColor;
		ColdownImage.fillAmount = 1;
		while (actualColdown < cooldownTime)
		{
			yield return new WaitForSeconds(0.01f);
			ColdownImage.fillAmount -= 0.01f;
			actualColdown += 0.01f;
		}
		ButtonImage.color = Color.white;
		ColdownImage.fillAmount = 1;
		ColdownImage.color = avaiableColor;
		StopCoroutine("ColdownAttack");
	}
}
