using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextsScript : MonoBehaviour {

    public Text t;
    public int ID;
    public LanguajesDic LD;

    private void Start()
    {
        LD.delegadoLang += GetKey;
    }

    public void GetKey()
    {
        t.text = LD.GetText(ID);
    }
}
