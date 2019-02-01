using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
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
    public GameObject LoadingScreen;
    private void Start()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //UpdateTexts();
        }
        PlayFabLogin.instance.GetVIV(CR.playerInfo, instance, EnvironmentController.instance, debugText, logInWindow);
        StartClientConfiguration();
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
    #region GoogleStuff
    #region SingIn
    public void StartClientConfiguration()
    {
        // Create client configuration
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .Build();




        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = false;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        // Try silent sign-in (second parameter is isSilent)
        if (PlayGamesPlatform.Instance.IsAuthenticated() == false)
        {
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
    }
    public void SingInGoogle()
    {
        SignIn();   
    }
    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();
            SignInButtonText.text = "Sign In";
        }
    }
    public void SignInCallback(bool success, string result)
    {
        if (success)
        {
            //DebugText
            debugText.text += "\nGoogleSignIn Success";

            //Logged With Google True
            CharacterReferences.instance.playerInfo.loggedWithGoogle = true;

            //Get user Name from google and Setting Playerprefs
            CR.playerInfo.playerName = PlayGamesPlatform.Instance.GetUserDisplayName();
            PlayerPrefs.SetString("Username", CR.playerInfo.playerName);
            PlayerPrefs.SetString("CustomID", CR.playerInfo.playerName);

            //DebugText
            debugText.text += "Username added to log in";

            //Update Name Shown
            logInUsername.text = CR.playerInfo.name;

            //Update First Achievement
            UpdateAchievement(achievements.achievement_new_animal);
            
            // Change sign-in button text
            SignInButtonText.text = "Sign out";

            // Log In With Custom ID
            PlayFabLogin.instance.LogInCustomPlayFab();
        }
        else
        {
            //DebugText
            debugText.text += "\nGoogle SignIn Failed: "+result;
            //Start Login Without Google
            PlayFabLogin.instance.StartLogIn();
        }
        
    }
    #endregion
    #region Achievements
    public enum achievements
    {
        achievement_new_animal
    }
    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            debugText.text += "\nCannot show Achievements, not logged in";
        }
    }
    public void UpdateAchievement(achievements achiv)
    {
        if (Social.localUser.authenticated)
        {
            switch (achiv)
            {
                case achievements.achievement_new_animal:
                    PlayGamesPlatform.Instance.ReportProgress(
                    GPGSIds.achievement_new_animal,
                    100.0f, (bool success) => {
                        debugText.text += "\n(RunForLife) Welcome Unlock: " +
                              success;
                    });
                    break;
            }
        }
       
    }
    public void UpdateAchievement(achievements achiv, int increment)
    {
        //Increment
        /*PlayGamesPlatform.Instance.IncrementAchievement(
        GPGSIds.achievement_sharpshooter,
                      1,
                      (bool success) => {
                          Debug.Log("(Lollygagger) Sharpshooter Increment: " +
                             success);
                      });*/
    }
    #endregion
    #region Leaderboards
    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            debugText.text = "Cannot show leaderboard: not authenticated";
        }
    }
    public void LeaderboardUpdate(int maxScore)
    {
       if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(maxScore,
                GPGSIds.leaderboard_meters,
                (bool success) =>
                {
                    debugText.text += "\nLeaderboard update success: " + success;
                });

            try
            {
                //WriteUpdatedScore(maxScore);
            }
            catch (Exception)
            {
                debugText.text += "\nCant WriteUpdatedScore";
                throw;
            } 
        }
    }

    #endregion
    #endregion
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

		CC.DieChallengesCheck();
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
        maxMetersPanel.SetActive(false);
        coinsGO.SetActive(false);
        gemsGO.SetActive(false);
        Lifes.GetComponent<Health>().SetHearts();
        Meters.SetActive(true);
        AttackButton.SetActive(true);
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
            LeaderboardUpdate(currentMeters);
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
