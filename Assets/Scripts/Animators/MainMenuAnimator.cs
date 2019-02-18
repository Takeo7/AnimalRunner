using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuAnimator : MonoBehaviour {
    #region Singleton
    public static MainMenuAnimator instance;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    public Animator damageImage;
    [Space]
    public Text debugText;
    [Space]
    public CoinsController cc;
    public Text coinsText;
    public Text gemsText;
    public GameObject coinsGO;
    public GameObject gemsGO;
    [Space]
    public int infoToSave;
    public int MaxMeters;
    [Space]
    public Text MaxMetersText;
    [Space]
    public Text SignInButtonText;
    [Space]
    public Animator animator;
    [Space]
    public GameObject nonTouch;
    public GameObject settingsWindow;
    public GameObject logInWindow;
    public TextsScript GoogleLogInText;
    public TextsScript PlayFabLogInText;
    public GameObject shopWindow;
    public GameObject IntroWindow;
    public GameObject deadWindow;
    public GameObject maxMetersPanel;
	public GameObject shopPopup;
	public Text shopPopupCharacterDescription;
	public Text shopPopupCharacterSpecialDescription;
    [Space]
    public GameObject Lifes;
    public GameObject Meters;
    public GameObject AttackButton;
    public GameObject AttackColdown;
	public GameObject pauseButton;
    public GameObject JumpButton;
    [Space]
    public Text meters;
    [Space]
    public Text logInUsername;
    public Text PlayernameText;
    public int currentMeters;
	public CharacterReferences CR;
	public ChallengesController CC;
	public bool isFall;
    [Space]
    public GameObject LoadingScreen;
	[Space]
	public Text coinsTakenTXT;

    GooglePlayLogin GPL;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime") != 1)
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            switch (Application.systemLanguage)
            {         
                case SystemLanguage.English:
                    CR.playerInfo.language = 0;
                    LanguajesDic.instance.LoadCurrentLang(0);
                    break;
                case SystemLanguage.French:
                    CR.playerInfo.language = 2;
                    LanguajesDic.instance.LoadCurrentLang(2);
                    break;
                case SystemLanguage.Spanish:
                    CR.playerInfo.language = 1;
                    LanguajesDic.instance.LoadCurrentLang(1);
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    CR.playerInfo.language = 0;
                    LanguajesDic.instance.LoadCurrentLang(0);
                    break;
                default:
                    CR.playerInfo.language = 0;
                    LanguajesDic.instance.LoadCurrentLang(0);
                    break;
            }
        }
        GPL = GooglePlayLogin.instance;
        if (GPL.isGoogleLogged == true || PlayFabLogin.instance.isPlayFabLogged == true)
        {
            LoadingScreen.SetActive(false);
        }
        GPL.GetMMA(instance);
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //UpdateTexts();
        }
        PlayFabLogin.instance.GetVIV(CR.playerInfo, instance, EnvironmentController.instance, debugText, logInWindow, PlayFabLogInText);
        EnvironmentController.instance.gameOverDelegate += ToogleDeadWindow;
        
        UpdateCoinsText();
    }
    private void Update()
    {
		currentMeters = Mathf.RoundToInt(CR.characterTransform.transform.position.x);
        meters.text = Mathf.RoundToInt(CR.characterTransform.transform.position.x) + " m";

        /*achiveButton.SetActive(Social.localUser.authenticated);
        leadButton.SetActive(Social.localUser.authenticated);
        saveGameButton.SetActive(Social.localUser.authenticated);
        if (Social.localUser.authenticated == true)
        {
            SignInButtonText.text = LanguajesDic.instance.GetText(9);
        }
        else
        {
            SignInButtonText.text = LanguajesDic.instance.GetText(8);
        }*/
    }
    #region ToggleWindows
    public void ToogleDeadWindow()
    {
		StartCoroutine("ToggleDeadWindowCO",isFall);
    }
	IEnumerator ToggleDeadWindowCO(bool isFall)
	{
		if (isFall)
		{
			yield return new WaitForSeconds(0f);
		}
		else
		{
			yield return new WaitForSeconds(2f);
		}
		ShopConfirmer.instance.cam.SetActive(true);
		ShopConfirmer.instance.InstantiateCharacter((byte)CR.playerInfo.selectedCharacter);
		CC.DieChallengesCheck();
		coinsTakenTXT.text =  LanguajesDic.instance.GetText(34)+": "+CoinsController.instance.coinsOnRun;
		deadWindow.SetActive(true);
	}

	public void ToogleSettingsWindow()
    {
        settingsWindow.SetActive(!settingsWindow.active);
        if (settingsWindow.active)
        {
            shopWindow.SetActive(false);
        }
    }
    public void ToogleLogInWindow()
    {
        logInWindow.SetActive(!logInWindow.active);
    }
    public void ToogleShopWindow()
    {
        shopWindow.SetActive(!shopWindow.active);
        if (shopWindow.active)
        {
            settingsWindow.SetActive(false);
        }
    }
    public void ToogleNonTouch(bool b)
    {
        nonTouch.SetActive(b);
    }
	public void ToggleOff()
	{
		animator.SetTrigger("ToggleOff");
	}
    public void ToogleIntro()
    {
        StartCoroutine("IntroCoroutine");   
    }
    #endregion
    #region UpdateTexts
    public void UpdateTexts()
    {
        UpdateCoinsText();
        UpdateMeters();
        LoadingScreen.SetActive(false);
    }
    public void UpdateCoinsText()
    {
        coinsText.text = CR.playerInfo.coins.ToString();
        gemsText.text = CR.playerInfo.gems.ToString();
        UpdatePlayerName();
    }
    public void UpdateMeters()
    {
        maxMetersPanel.GetComponentInChildren<TextsScriptMultiple>().GetKeys();
    }
    public void UpdatePlayerName()
    {
        PlayernameText.text = PlayerPrefs.GetString("Username");
    }
    #endregion

    public void StartGame()
	{
        ToggleOff();
        //ToogleNonTouch(true);
        if (settingsWindow.active == true)
        {
            ToogleSettingsWindow();
        }
        if (shopWindow.active == true)
        {
            ToogleShopWindow();
        }
        //maxMetersPanel.SetActive(false);
        coinsGO.SetActive(false);
        gemsGO.SetActive(false);
        Lifes.GetComponent<Health>().SetHearts();
        Meters.SetActive(true);
        AttackButton.SetActive(true);
		pauseButton.SetActive(true);
        AttackColdown.SetActive(true);
        AttackButton.GetComponent<AttacksUI>().SetAttacks();
        JumpButton.SetActive(true);
		EnvironmentController.instance.StartGame();
	}
    public void ResetScene()
    {
        if (CR.playerInfo.metersRecord < currentMeters)
        {
			CR.playerInfo.metersRecord = currentMeters;
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                GPL.LeaderboardUpdate(currentMeters);
            }           
        }    
        SceneManager.LoadScene(1);
    }
    IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        ToogleNonTouch(false);
        IntroWindow.SetActive(false);
        StopCoroutine("IntroCoroutine");
    }

}
