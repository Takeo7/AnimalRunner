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
            if (PlayerPrefs.HasKey("Languaje"))
            {
                currentlang = PlayerPrefs.GetInt("Languaje");
            }
            else
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.English:
                        currentlang = 0;
                        break;
                    case SystemLanguage.Spanish:
                        currentlang = 1;
                        break;
                    case SystemLanguage.French:
                        currentlang = 2;
                            break;
                    case SystemLanguage.German:
                    case SystemLanguage.Chinese:
                    case SystemLanguage.ChineseSimplified:
                    default:
                        currentlang = 0;
                        break;
                }
            }
        }       
    }
    #endregion
    int currentlang = 0;

    public string[] English;
    public string[] Spanish;
    public string[] French;
    public string[] Chiniese;
    public string[] German;

    string[] currentLangTexts;

    private void Start()
    {
        LoadCurrentLang(currentlang);
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
        currentlang = i;
        PlayerPrefs.SetInt("Languaje", currentlang);
        delegadoLang();
    }

    public string GetText(int key)
    {
        return currentLangTexts[key];
    }
}

