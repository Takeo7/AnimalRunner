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

    public Image ColdownImage;
    public Image ButtonImage;

    public Color coldownColor;
    public Color avaiableColor;
    public Color buttonColor;

    float coldownTime;

    public void SetAttacks()
    {
        ps = CharacterReferences.instance.PS;
        ColdownImage.color = avaiableColor;
        coldownTime = ps.coldownTime;
    }

    public void UpdateAttacks()
    {
        Debug.Log("UpdateAttacks");
        StartCoroutine("ColdownAttack");
        
    }



    IEnumerator ColdownAttack()
    {
        float actualColdown = 0;
        ButtonImage.color = buttonColor;
        ColdownImage.color = coldownColor;
        ColdownImage.fillAmount = 1;
        while(actualColdown < coldownTime)
        {
            yield return new WaitForSeconds(0.01f);
            ColdownImage.fillAmount -= 0.01f;
            actualColdown += 0.01f;
        }
        CharacterReferences.instance.TM.UpdateAttack(true);
        ButtonImage.color = Color.white;
        ColdownImage.fillAmount = 1;
        ColdownImage.color = avaiableColor;
        StopCoroutine("ColdownAttack");
    }


}
