using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class languaje : ScriptableObject {

    public LanguajesDic.Languajes lang;

    public string[] dicArray;
    Dictionary<int, string> dic = new Dictionary<int, string>();


    
}
