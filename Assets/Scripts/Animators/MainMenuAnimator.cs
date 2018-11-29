using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuAnimator : MonoBehaviour {

    public Text signInButtonText;
    public Text authStatus;
    public GameObject achiveButton;
    [Space]
    public Animator animator;
    [Space]
    public GameObject nonTouch;
    public GameObject settingsWindow;
    public GameObject shopWindow;
    public GameObject IntroWindow;
    public GameObject deadWindow;
    [Space]
    public GameObject Lifes;
    public GameObject Meters;
    public GameObject AttackButton;
    public GameObject JumpButton;


    private void Start()
    {
        StartClientConfiguration();
        EnvironmentController.instance.gameOverDelegate += ToogleDeadWindow;
    }
    private void Update()
    {
        achiveButton.SetActive(Social.localUser.authenticated);
    }
    #region GoogleStuff
    #region SingIn
    public void StartClientConfiguration()
    {
        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        // Try silent sign-in (second parameter is isSilent)
        
        //PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
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

            // Reset UI
            signInButtonText.text = "Sign In";
            authStatus.text = "";
            authStatus.text += PlayGamesPlatform.Instance.GetServerAuthCode();
        }
    }
    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(RunForLife) Signed in!");

            // Change sign-in button text
            signInButtonText.text = "Sign out";

            // Show the user's name
            authStatus.text = "Signed in as: " + Social.localUser.userName;
            ToogleIntro();

        }
        else
        {
            Debug.Log("(RunForLife) Sign-in failed...");

            // Show failure message
            signInButtonText.text = "Sign in";
            authStatus.text = "Sign-in failed";
            authStatus.text += PlayGamesPlatform.Instance.GetServerAuthCode();
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
            Debug.Log("Cannot show Achievements, not logged in");
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
                        Debug.Log("(RunForLife) Welcome Unlock: " +
                              success);
                    });
                    break;
            }
        }
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
    #endregion
    public void ToogleDeadWindow()
    {
        deadWindow.SetActive(true);
    }
    public void ToogleSettingsWindow()
    {
        settingsWindow.SetActive(!settingsWindow.active);
    }
    public void ToogleShopWindow()
    {
        shopWindow.SetActive(!shopWindow.active);
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
	public void StartGame()
	{
        ToggleOff();
        ToogleNonTouch(true);
        if (settingsWindow.active == true)
        {
            ToogleSettingsWindow();
        }
        if (shopWindow.active == true)
        {
            ToogleShopWindow();
        }
        Lifes.SetActive(true);
        Lifes.GetComponent<Health>().SetHearts();
        Meters.SetActive(true);
        AttackButton.SetActive(true);
        JumpButton.SetActive(true);
		EnvironmentController.instance.StartGame();
	}
    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ToogleNonTouch(false);
        IntroWindow.SetActive(false);
        StopCoroutine("IntroCoroutine");
    }

}
