using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

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
	[Space]
	[Header("Stats")]
	public Text levelStats;
	public Text metersRecordStats;
	public Text totalAttacksStats;
	public Text totalChallengesCompletedStats;
	public Text totalCoinsEarnedStats;
	public Text totalDeathsStats;
	public Text totalEnemiesKilledStats;
	public Text totalJumpsStats;
	public Text TotalMetersRunnedStats;
	public Text TotalSpecialUsedStats;

	LanguajesDic LANG;
    public GooglePlayLogin GPL;

    [Space]
    public Text pointsText;
    public Animator pointTextAnim;
    public GameObject goBackButton;
    [Space]
    public GameObject rewardedVideoButton;
    public Text rewardedCountdown;
	public GameObject continueAfterAd;
    [SerializeField]
    bool seenRewardedVideo;
	public Text metersRunDeadWindow;
    [Space]
    public GameObject tutorial;
    [Space]
    public GameObject internetConection;

	public bool isPoints;
	public bool isLevel;
	public bool isDebugAd;
    private void Start()
    {
        GPL = GooglePlayLogin.instance;
        if (PlayFab.PlayFabAuthenticationAPI.IsEntityLoggedIn() == false && Social.localUser.authenticated == false && PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            PlayFabLogin.instance.LogInPlayFabOS();
        }
        else
        {
            LoadingScreen.SetActive(false);
        }        
		LANG = LanguajesDic.instance;
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
        
        GPL.GetMMA(instance);
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //UpdateTexts();
        }
        PlayFabLogin.instance.GetVIV(CR.playerInfo, instance, EnvironmentController.instance, debugText, logInWindow, PlayFabLogInText);
        EnvironmentController.instance.gameOverDelegate += ToogleDeadWindow;

        UpdateCoinsText();
        UpdateMeters();
        UpdateStatsText();

        if (PlayFabLogin.instance.noInternet)
        {
            internetConection.SetActive(true);
        }
        else if (PlayFabLogin.instance.noInternet == false)
        {
            internetConection.SetActive(false);
        }
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
    public void OpenTutorial()
    {
        StartCoroutine("OpenTutorialCO");
    }
    IEnumerator OpenTutorialCO()
    {
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
			PlayerPrefs.SetInt("Tutorial",1);
            tutorial.SetActive(true);
            Time.timeScale = 0;
        }
        StopCoroutine("OpenTutorialCO");
    }
    public void ToggleRewardedVideoButton()
    {
        if (!seenRewardedVideo)
        {
            seenRewardedVideo = true;
            rewardedVideoButton.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine("RewardedVideoCountdown");
        }
        else
        {
            CR.PS.reallyDead = true;
            EnvironmentController.instance.gameOverDelegate();
            CR.TM.enabled = false;
        }
    }
    IEnumerator RewardedVideoCountdown()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            rewardedCountdown.text = countdown + " " + LANG.GetText(48);
            yield return new WaitForSecondsRealtime(1);
            countdown--;
        }
        if(countdown <= 0)
        {
            //Debug.Log("DEAD");
            Time.timeScale = 1;
            CR.PS.AC.DeathAnim();
			rewardedVideoButton.SetActive(false);
			CC.UIC.meters.gameObject.SetActive(false);
            EnvironmentController.instance.gameOverDelegate();
        }
    }
	public void SeeRewardedVideoShop()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult2 };
			Advertisement.Show("rewardedVideo", options);
		}
	}
	void HandleShowResult2(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
                debugText.text += "The ad was successfully shown.";
                Debug.Log("The ad was successfully shown.");
				CoinsController.instance.SetCoins(10);
				break;
			case ShowResult.Skipped:
                debugText.text += "The ad was skipped before reaching the end.";
                Debug.Log("The ad was skipped before reaching the end.");
				//Debug.Log("DEAD");
				
				break;
			case ShowResult.Failed:
                debugText.text += "The ad failed to be shown.";
                Debug.LogError("The ad failed to be shown.");
				//Debug.Log("DEAD");
				
				break;
		}
	}
	public void SeeRewardedVideo()
    {
		if (!isDebugAd)
		{
			StopCoroutine("RewardedVideoCountdown");
			if (Advertisement.IsReady("rewardedVideo"))
			{
				var options = new ShowOptions { resultCallback = HandleShowResult };
				Advertisement.Show("rewardedVideo", options);
			}
		}
		else
		{
			StopCoroutine("RewardedVideoCountdown");
			CR.PS.Resucitate();
			continueAfterAd.SetActive(true);
		}
    }
	void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
                debugText.text += "The ad was successfully shown.";
                Debug.Log("The ad was successfully shown.");
				CR.PS.Resucitate();
				continueAfterAd.SetActive(true);
				break;
			case ShowResult.Skipped:
                debugText.text += "The ad was skipped before reaching the end.";
                Debug.Log("The ad was skipped before reaching the end.");
				//Debug.Log("DEAD");
				Time.timeScale = 1;
				CR.PS.AC.DeathAnim();
				rewardedVideoButton.SetActive(false);
				CC.UIC.meters.gameObject.SetActive(false);
				EnvironmentController.instance.gameOverDelegate();
				break;
			case ShowResult.Failed:
                debugText.text += "The ad failed to be shown.";
                Debug.LogError("The ad failed to be shown.");
				//Debug.Log("DEAD");
				Time.timeScale = 1;
				CR.PS.AC.DeathAnim();
				rewardedVideoButton.SetActive(false);
				CC.UIC.meters.gameObject.SetActive(false);
				EnvironmentController.instance.gameOverDelegate();
				break;
		}
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
		metersRunDeadWindow.text = CC.UIC.metersRun+" " + LANG.GetText(5);
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
    public void UpdateTexts(bool login)
    {
        //Debug.Log("UpdatedTexts");
        UpdateCoinsText();
        UpdateMeters();
        if (login)
        {
            LoadingScreen.SetActive(false);
        }       
        UpdateStatsText();
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
	void UpdateStatsText()
	{
		//UPDATE STATS TEXT
		
		levelStats.text = LANG.GetText(11)+":\n "+CR.playerInfo.playerLevel;
		metersRecordStats.text = "<color=white>"+LANG.GetText(36) + ": "+"</color>" +CR.playerInfo.metersRecord;
		totalAttacksStats.text = "<color=silver>" + LANG.GetText(35) + ": " + "</color>" + CR.playerInfo.totalAttacks;
		totalChallengesCompletedStats.text = "<color=white>" + LANG.GetText(37) + ": " + "</color>" + CR.playerInfo.totalChallengesCompleted;
		totalCoinsEarnedStats.text = "<color=silver>" + LANG.GetText(38) + ": " + "</color>" + CR.playerInfo.totalCoinsEarned;
		totalDeathsStats.text = "<color=white>" + LANG.GetText(39) + ": " + "</color>" + CR.playerInfo.totalDeaths;
		totalEnemiesKilledStats.text = "<color=silver>" + LANG.GetText(40) + ": " + "</color>" + CR.playerInfo.totalEnemiesKilled;
		totalJumpsStats.text = "<color=white>" + LANG.GetText(41) + ": " + "</color>" + CR.playerInfo.totalJumps;
		TotalMetersRunnedStats.text = "<color=silver>" + LANG.GetText(42) + ": " + "</color>" + CR.playerInfo.totalMetersRunned;
		TotalSpecialUsedStats.text = "<color=white>" + LANG.GetText(43) + ": " + "</color>" + CR.playerInfo.totalSpecialUsed;
	}
    public void UpdatePointText(int points)
    {
        StartCoroutine("UpdatePointsCoroutine", points);
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
        if (CR.playerInfo.metersRecord < CharacterReferences.instance.uic.metersRun)
        {
			CR.playerInfo.metersRecord = CharacterReferences.instance.uic.metersRun;
			if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                GPL.LeaderboardUpdate(CharacterReferences.instance.uic.metersRun);
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
    IEnumerator UpdatePointsCoroutine(int points)
    {
        CoinsController cc = CoinsController.instance;
        string textPoints = LANG.GetText(12);
        string textCoins = LANG.GetText(34);
        int temp = 0;
        int temp2 = 0;
        int pointsTotal = points;
        float time = 0.2f;
        if (pointsTotal >= 30)
        {
            time = 0.1f;
        }
        while (temp <= points)
        {
            yield return new WaitForSeconds(time);
            temp++;
            temp2++;
            if (temp2 == 10)
            {
                cc.coinsOnRun++;
                pointTextAnim.SetTrigger("OnePlusTrigger");
                coinsTakenTXT.text = textCoins + ": " + cc.coinsOnRun;
                temp2 = 0;
            }
            pointsText.text = textPoints + ": " + temp;
        }
        cc.SetCoins(cc.coinsOnRun);
		isPoints = true;
		Debug.Log("isPoints");
        if(isPoints && isLevel)
		{
			goBackButton.SetActive(true);
		}
    }

	public void GoBackButton()
	{
		EnvironmentController.instance.UploadUserDataEC();
		goBackButton.SetActive(true);
	}

}
