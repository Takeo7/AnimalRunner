using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScriptErrors : MonoBehaviour {

    public Text t;

    private void Start()
    {
        PlayFabLogin.instance.SetErrorText(this);
        ResetText();
    }

    public void GetErrorKey(int key)
    {
        t.text = LanguajesDic.instance.GetErrorText(key);
    }

    public void ResetText()
    {
        t.text = "";
    }
}
