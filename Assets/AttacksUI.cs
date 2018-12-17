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

    public GameObject AttackGO;
    public int initialAttacks;
    public int attacksleft;

    public Image[] attacksImage;

    public void SetAttacks()
    {
        ps = CharacterReferences.instance.PS;
        initialAttacks = ps.numAttacks;
        attacksleft = initialAttacks;
        attacksImage = new Image[initialAttacks];
        CreateAttacks(initialAttacks);
    }

    void CreateAttacks(int h)
    {
        for (int i = 0; i < h; i++)
        {
            GameObject g = Instantiate(AttackGO, transform);
            attacksImage[i] = g.GetComponent<Image>();
        }
    }

    public void UpdateAttacks()
    {
        attacksImage[attacksleft - 1].fillAmount = 0;
        StartCoroutine("ColdownAttack");
        attacksleft--;       
    }

    IEnumerator ColdownAttack()
    {
        while(attacksImage[attacksleft-1].fillAmount < 1)
        {
            yield return new WaitForSeconds(0.01f);
            attacksImage[attacksleft - 1].fillAmount += 0.01f;
        }
        attacksleft++;
        CharacterReferences.instance.TM.UpdateAttacks();
        StopCoroutine("ColdownAttack");
    }


}
