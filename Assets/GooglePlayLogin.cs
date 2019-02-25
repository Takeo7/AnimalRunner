using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class GooglePlayLogin : MonoBehaviour {

    #region Singleton
    public static GooglePlayLogin instance;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

    MainMenuAnimator MMA;

    public bool isGoogleLogged = false;

    private void Start()
    {
        MMA = MainMenuAnimator.instance;
        StartClientConfiguration();
    }

    public void GetMMA(MainMenuAnimator mma)
    {
        MMA = mma;
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
        }
    }
    public void SignInCallback(bool success, string result)
    {
        if (success)
        {
            isGoogleLogged = true;
            //DebugText
            MMA.debugText.text += "\nGoogleSignIn Success";

            //Logged With Google True
            CharacterReferences.instance.playerInfo.loggedWithGoogle = true;

            //Get user Name from google and Setting Playerprefs
            MMA.CR.playerInfo.playerName = PlayGamesPlatform.Instance.GetUserDisplayName();
            PlayerPrefs.SetString("Username", MMA.CR.playerInfo.playerName);
            PlayerPrefs.SetString("CustomID", MMA.CR.playerInfo.playerName);

            //DebugText
            MMA.debugText.text += " - Username added to log in";

            //Update Name Shown
            MMA.logInUsername.text = MMA.CR.playerInfo.name;

            //Update First Achievement
            UpdateAchievement(achievements.achievement_new_animal);

            // Change sign-in button text

            // Log In With Custom ID
            PlayFabLogin.instance.LogInCustomPlayFab();
        }
        else
        {
            //DebugText
            MMA.debugText.text += "\nGoogle SignIn Failed: " + result;
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
            MMA.debugText.text += "\nCannot show Achievements, not logged in";
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
                        MMA.debugText.text += "\n(RunForLife) Welcome Unlock: " +
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
            MMA.debugText.text = "Cannot show leaderboard: not authenticated";
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
                    MMA.debugText.text += "\nLeaderboard update success: " + success;
                });
        }
    }

    #endregion
    #endregion
}
