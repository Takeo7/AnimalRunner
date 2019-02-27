using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialsUI : MonoBehaviour {

	#region  Singleton
	public static SpecialsUI instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	PlayerStats ps;

    public Button specialattackbutton;

	public Image cooldownImage;
	public Image buttonImage;
	public Button button;

    public Sprite[] attackButtonsForest;
    public Sprite[] attackButtonsDesert;
    public Sprite[] attackButtonsIce;

    private void Start()
    {
        SetSpecialAttackButtonSprite();
    }

    public void SetSpecialAttackButtonSprite()
    {
        SpriteState sps = new SpriteState();
        switch (EnvironmentController.instance.set.setType)
        {
            case SetType.Forest:
                specialattackbutton.image.sprite = attackButtonsForest[0];
				cooldownImage.sprite = attackButtonsForest[0];
				sps.pressedSprite = attackButtonsForest[1];
                //Debug.Log("Special button Forest");
                break;
            case SetType.Desert:
                specialattackbutton.image.sprite = attackButtonsDesert[0];
				cooldownImage.sprite = attackButtonsDesert[0];
				sps.pressedSprite = attackButtonsDesert[1];
                //Debug.Log("Special button Desert");
                break;
            case SetType.Ice:
                specialattackbutton.image.sprite = attackButtonsIce[0];
				cooldownImage.sprite = attackButtonsIce[0];
				sps.pressedSprite = attackButtonsIce[1];
                //Debug.Log("Special button Ice");
                break;
            default:
                break;
        }
        specialattackbutton.spriteState = sps;
    }

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
	public void SetColorToPressed()
	{
		buttonImage.color = buttonColor;
	}
}
