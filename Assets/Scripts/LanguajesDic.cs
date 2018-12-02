using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LanguajesDic : ScriptableObject {

    Languajes currentlang;

    Dictionary<int, string> Dic = new Dictionary<int, string>();
    
    public string GetText(int key)
    {
        return Dic[key];
    }

    public enum Languajes
    {
        English,
        Spanish,
        French,
        Chiniese
    }
}
