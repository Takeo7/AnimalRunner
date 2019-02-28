using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour {

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
        t.text = LanguajesDic.instance.GetTutorialText(ID);
    }
    public void GetKey(int key)
    {
        t.text = LanguajesDic.instance.GetTutorialText(key);
    }
}
