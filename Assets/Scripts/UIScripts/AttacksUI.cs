using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttacksUI : MonoBehaviour {

    #region Singleton
    public static AttacksUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    PlayerStats ps;

    public Image attackCooldownButton;
    public Image buttonImage;
    public Button attackbutton;

    public Sprite[] attackButtonsForest;
    public Sprite[] attackButtonsDesert;
    public Sprite[] attackButtonsIce;

    public Color coldownColor;
    public Color avaiableColor;
    public Color buttonColor;

    float coldownTime;

    private void Start()
    {
        SetAttackButtonSprite();
    }

    public void SetAttackButtonSprite()
    {
        SpriteState sps = new SpriteState();
        switch (EnvironmentController.instance.set.setType)
        {
            case SetType.Forest:
                attackbutton.image.sprite = attackButtonsForest[0];
				attackCooldownButton.sprite = attackButtonsForest[0];
				sps.pressedSprite = attackButtonsForest[1];
				break;
            case SetType.Desert:
                attackbutton.image.sprite = attackButtonsDesert[0];
				attackCooldownButton.sprite = attackButtonsDesert[0];
				sps.pressedSprite = attackButtonsDesert[1];
                break;
            case SetType.Ice:
                attackbutton.image.sprite = attackButtonsIce[0];
				attackCooldownButton.sprite = attackButtonsIce[0];
				sps.pressedSprite = attackButtonsIce[1];
                break;
            default:
                break;
        }
        attackbutton.spriteState = sps;
    }

    public void SetAttacks()
    {
        ps = CharacterReferences.instance.PS;
        attackCooldownButton.color = avaiableColor;
        coldownTime = ps.coldownTime;
    }

    public void UpdateAttacks()
    {
        //Debug.Log("UpdateAttacks");
        StartCoroutine("ColdownAttack");        
    }



    IEnumerator ColdownAttack()
    {
        float actualColdown = 0;
        buttonImage.color = buttonColor;
        attackCooldownButton.color = coldownColor;
        attackCooldownButton.fillAmount = 1;
        while(actualColdown < coldownTime)
        {
            yield return new WaitForSeconds(0.01f);
            attackCooldownButton.fillAmount -= 0.01f;
            actualColdown += 0.01f;
        }
        CharacterReferences.instance.TM.UpdateAttack(true);
        buttonImage.color = Color.white;
        attackCooldownButton.fillAmount = 1;
        attackCooldownButton.color = avaiableColor;
        StopCoroutine("ColdownAttack");
    }


}
