using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextsScriptMultiple : MonoBehaviour {

    public Text t;
    public int ID1;
    public int ID2;
    public bool isHidden = true;

    private void Start()
    {
        LanguajesDic.instance.delegadoLang += GetKeys;
        if (isHidden)
        {
            GetKeys();
        }

    }

    public void GetKeys()
    {
        t.text = LanguajesDic.instance.GetText(ID1)+ ": "+CharacterReferences.instance.playerInfo.metersRecord+" "+LanguajesDic.instance.GetText(ID2);
    }
}
