using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LanguajesDic : MonoBehaviour {
    //https://pngtree.com/free-icons/
    #region DontDestroy/Singleton
    public static LanguajesDic instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }       
    }
	#endregion
	public CharacterReferences CR;

    public string[] English;
    public string[] Spanish;
    public string[] French;
    public string[] Chinese;
    public string[] German;

    string[] currentLangTexts;
    private void Start()
    {
		LoadCurrentLang(CR.playerInfo.language);
    }

    public delegate void Delegadolang();
    public Delegadolang delegadoLang;

    public void LoadCurrentLang(int i)
    {
        switch (i)
        {
            case 0:
                currentLangTexts = English;
                break;
            case 1:
                currentLangTexts = Spanish;
                break;
            case 2:
                currentLangTexts = French;
                break;
            default:
                break;
        }
        delegadoLang();
    }

    public string GetText(int key)
    {
        return currentLangTexts[key];
    }
}

