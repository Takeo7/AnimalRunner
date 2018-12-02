using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class LanguajesDic : ScriptableObject {

    int currentlang;

    public languaje[] LangList;

    Dictionary<int, string> Dic = new Dictionary<int, string>();

    public delegate void Delegadolang();
    public Delegadolang delegadoLang;

    public void LoadCurrentLang(int l)
    {
        Dic = LangList[l].dic;
        currentlang = l;
        PlayerPrefs.SetInt("Languaje", currentlang);
        //delegadoLang();
    }

    public string GetText(int key)
    {
        return LangList[currentlang].dic[key];
    }
}

