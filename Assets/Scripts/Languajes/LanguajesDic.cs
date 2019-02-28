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
    public string[] English_Challenges;

    public string[] Spanish;
    public string[] Spanish_Errors;
    public string[] Spanish_Challenges;

    public string[] French;
    public string[] French_Errors;
    public string[] French_Challenges;

    public string[] Chinese;
    public string[] Chinese_Errors;
    public string[] Chinese_Challenges;


    string[] currentLangTexts;
    string[] currentLangErrorTexts;
    [SerializeField]
    string[] currentLangChallenges;

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
                currentLangChallenges = English_Challenges;
                break;
            case 1:
                currentLangTexts = Spanish;
                currentLangErrorTexts = Spanish_Errors;
                currentLangChallenges = Spanish_Challenges;
                break;
            case 2:
                currentLangTexts = French;
                currentLangErrorTexts = French_Errors;
                currentLangChallenges = French_Challenges;
                break;
            case 3:
                currentLangTexts = Chinese;
                currentLangErrorTexts = Chinese_Errors;
                currentLangChallenges = Chinese_Challenges;
                break;
            default:
                currentLangTexts = English;
                currentLangErrorTexts = English_Errors;
                currentLangChallenges = English_Challenges;
                break;
        }
        SetCharactersData();
        delegadoLang();
    }

    void SetCharactersData()
    {
        int startTexts = 20;
        CharactersInfo chi = CharacterReferences.instance.charactersInfo;
        int length = chi.characters.Length;
        for (int i = 0; i < length; i++)
        {
            chi.characters[i].name = currentLangTexts[startTexts];
            startTexts++;
            chi.characters[i].description = currentLangTexts[startTexts];
            startTexts++;
        }
    }

    public string GetText(int key)
    {
        return currentLangTexts[key];
    }

    public string GetErrorText(int errorID)
    {
        return currentLangErrorTexts[errorID];
    }

    public string GetChallengesText(int challengesID)
    {
        return currentLangChallenges[challengesID];
    }
}

