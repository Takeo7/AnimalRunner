using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScriptGemasShop : MonoBehaviour {
    public Text t;
    public int ID1;
    public int ID2;
    public int gems;
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
        t.text = LanguajesDic.instance.GetText(ID1) + ": " + gems + " " + LanguajesDic.instance.GetText(ID2);
    }
}
