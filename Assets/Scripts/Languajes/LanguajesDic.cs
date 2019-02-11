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
		if (CR.playerInfo.language > -1)
		{
			
		}
		else
		{
			switch (Application.systemLanguage)
			{
				case SystemLanguage.English:
					CR.playerInfo.language = 0;
					break;
				case SystemLanguage.Spanish:
					CR.playerInfo.language = 1;
					break;
				case SystemLanguage.French:
					CR.playerInfo.language = 2;
					break;
				case SystemLanguage.German:
				case SystemLanguage.Chinese:
				case SystemLanguage.ChineseSimplified:
				default:
					CR.playerInfo.language = 0;
					break;
			}
		}
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

