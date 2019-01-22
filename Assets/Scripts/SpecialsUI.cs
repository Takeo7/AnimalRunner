using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialsUI : MonoBehaviour {

	PlayerStats ps;

	public Image cooldownImage;
	public Image buttonImage;
	public Button button;

	public Color coldownColor;
	public Color avaiableColor;
	public Color buttonColor;

	public float cooldownTime;

	public void SetCooldown()
	{
		StartCoroutine("Countdown");
	}
	private IEnumerator Countdown()
	{
		button.interactable = false;
		float duration = cooldownTime; // 3 seconds you can change this 
		buttonImage.color = buttonColor;
		cooldownImage.color = coldownColor;                         //to whatever you want
		float normalizedTime = 0;
		while (normalizedTime <= 1f)
		{
			cooldownImage.fillAmount = normalizedTime;
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
		buttonImage.color = Color.white;
		cooldownImage.color = avaiableColor;
		button.interactable = true;
	}
}
