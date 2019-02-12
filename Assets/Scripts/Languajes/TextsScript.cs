using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextsScript : MonoBehaviour {

    public Text t;
    public int ID;
    public bool isHidden = true;
    private void Awake()
    {
        LanguajesDic.instance.delegadoLang += GetKey;
    }

    private void Start()
    {
        if (isHidden)
        {
            GetKey();
        }

    }
    public void GetKey()
    {
        t.text = LanguajesDic.instance.GetText(ID);
    }
    public void GetKey(int key)
    {
        t.text = LanguajesDic.instance.GetText(key);
    }
}
