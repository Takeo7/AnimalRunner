using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class languaje : ScriptableObject {

    public string[] dicArray;
    public Dictionary<int, string> dic = new Dictionary<int, string>();

    public void LoadLanguage()
    {
        dic = new Dictionary<int, string>();
        int length = dicArray.Length;
        for (int i = 0; i < length; i++)
        {
            dic.Add(i, dicArray[i]);
        }
    }
    
}

[CustomEditor(typeof(languaje))]
public class LanguajeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        languaje myScript = (languaje)target;
        if (GUILayout.Button("Load Languaje"))
        {
            myScript.LoadLanguage();
            EditorGUILayout.TextField("LOADED");
        }
    }
}
