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
    public string[] English_Errors;
    public string[] Spanish;
    public string[] Spanish_Errors;
    public string[] French;
    public string[] French_Errors;
    public string[] Chinese;
    public string[] Chinese_Errors;
    public string[] German;
    public string[] German_Errors;

    string[] currentLangTexts;
    string[] currentLangErrorTexts;

    private void Start()
    {
		LoadCurrentLang(CR.playerInfo.language);
    }

    public delegate void Delegadolang();
    public Delegadolang delegadoLang;

    public void LoadCurrentLang(int i)
    {
        CharacterReferences.instance.playerInfo.SetLanguage(i);
        switch (i)
        {
            case 0:
                currentLangTexts = English;
                currentLangErrorTexts = English_Errors;
                break;
            case 1:
                currentLangTexts = Spanish;
                currentLangErrorTexts = Spanish_Errors;
                break;
            case 2:
                currentLangTexts = French;
                currentLangErrorTexts = French_Errors;
                break;
            case 3:
                currentLangTexts = Chinese;
                currentLangErrorTexts = Chinese_Errors;
                break;
            default:
                currentLangTexts = English;
                currentLangErrorTexts = English_Errors;
                break;
        }
        delegadoLang();
    }

    public string GetText(int key)
    {
        return currentLangTexts[key];
    }

    public string GetErrorText(int errorID)
    {
        return currentLangErrorTexts[errorID];
    }
}

