﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuAnimator : MonoBehaviour {

    public int infoToSave;
    public int MaxMeters;
    [Space]
    public Text MaxMetersText;
    [Space]
    public GameObject achiveButton;
    public GameObject leadButton;
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
    [Space]
    public Text meters;
    public int currentMeters;
    public Transform rootTarget;

    private void Start()
    {
        StartClientConfiguration();
        EnvironmentController.instance.gameOverDelegate += ToogleDeadWindow;
    }
    private void Update()
    {
        currentMeters = Mathf.RoundToInt(rootTarget.transform.position.x);
        meters.text = Mathf.RoundToInt(rootTarget.transform.position.x) + " m";

        achiveButton.SetActive(Social.localUser.authenticated);
        leadButton.SetActive(Social.localUser.authenticated);
    }
    #region GoogleStuff
    #region SingIn
    public void StartClientConfiguration()
    {
        // Create client configuration
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            //.EnableSavedGames()
            .Build();




        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        // Try silent sign-in (second parameter is isSilent)
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {

        }
        else {
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
        }

        //UpdateLocalInfoRead();
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
            /*signInButtonText.text = "Sign In";
            authStatus.text = "";
            authStatus.text += PlayGamesPlatform.Instance.GetServerAuthCode();*/
        }
    }
    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(RunForLife) Signed in!");
            UpdateAchievement(achievements.achievement_new_animal);

            // Change sign-in button text
           /* signInButtonText.text = "Sign out";

            // Show the user's name
            authStatus.text = "Signed in as: " + Social.localUser.userName;
            ToogleIntro();*/

        }
        else
        {
            Debug.Log("(RunForLife) Sign-in failed...");

            // Show failure message
           /* signInButtonText.text = "Sign in";
            authStatus.text = "Sign-in failed";
            authStatus.text += PlayGamesPlatform.Instance.GetServerAuthCode();*/
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
            Debug.Log("Cannot show leaderboard: not authenticated");
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
                    Debug.Log("(RunForLife) Leaderboard update success: " + success);
                });

            //WriteUpdatedScore(maxScore);
        }
    }

    #endregion
    #region SaveGame
    /*public void ReadSavedGame(string filename,
                             Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(
            filename,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            callback);
    }

    public void WriteSavedGame(ISavedGameMetadata game, byte[] savedData,
                               Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedPlayedTime(TimeSpan.FromMinutes(game.TotalTimePlayed.Minutes + 1))
            .WithUpdatedDescription("Saved at: " + System.DateTime.Now);

        // You can add an image to saved game data (such as as screenshot)
        // byte[] pngData = <PNG AS BYTES>;
        // builder = builder.WithUpdatedPngCoverImage(pngData);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, callback);
    }

    public void WriteUpdatedScore(int newMaxScore)
    {
        // Local variable
        ISavedGameMetadata currentGame = null;

        // CALLBACK: Handle the result of a write
        Action<SavedGameRequestStatus, ISavedGameMetadata> writeCallback =
        (SavedGameRequestStatus status, ISavedGameMetadata game) => {
            Debug.Log("(RunForLife) Saved Game Write: " + status.ToString());
        };

        // CALLBACK: Handle the result of a binary read
        Action<SavedGameRequestStatus, byte[]> readBinaryCallback =
        (SavedGameRequestStatus status, byte[] data) => {
            Debug.Log("(RunForLife) Saved Game Binary Read: " + status.ToString());
            if (status == SavedGameRequestStatus.Success)
            {
                // Read score from the Saved Game
                int[] score = new int[infoToSave];
                try
                {
                    string scoreString = System.Text.Encoding.UTF8.GetString(data);
                    string[] scoreStringArr = scoreString.Split(',');
                    for (int i = 0; i < infoToSave; i++)
                    {
                        score[i] = Convert.ToInt32(scoreStringArr[i]);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("(RunForLife) Saved Game Write: convert exception");
                }

                // Increment score, convert to byte[]
                score[0] = newMaxScore;
                string newScoreString = "";
                for (int i = 0; i < infoToSave; i++)
                {
                    newScoreString += Convert.ToString(score[i]);
                }
                byte[] newData = System.Text.Encoding.UTF8.GetBytes(newScoreString);

                // Write new data
                WriteSavedGame(currentGame, newData, writeCallback);
            }
        };

        // CALLBACK: Handle the result of a read, which should return metadata
        Action<SavedGameRequestStatus, ISavedGameMetadata> readCallback =
        (SavedGameRequestStatus status, ISavedGameMetadata game) => {
            Debug.Log("(RunForLife) Saved Game Read: " + status.ToString());
            if (status == SavedGameRequestStatus.Success)
            {
                // Read the binary game data
                currentGame = game;
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game,
                                                    readBinaryCallback);
            }
        };

        // Read the current data and kick off the callback chain
        Debug.Log("(RunForLife) Saved Game: Reading");
        ReadSavedGame("file_maxMeters", readCallback);
    }

    public int[] ReadUpdatedSaveData()
    {
        // Local variable
        ISavedGameMetadata currentGame = null;

        int[] score = new int[infoToSave];

        // CALLBACK: Handle the result of a binary read
        Action<SavedGameRequestStatus, byte[]> readBinaryCallback =
        (SavedGameRequestStatus status, byte[] data) => {
            Debug.Log("(RunForLife) Saved Game Binary Read: " + status.ToString());
            if (status == SavedGameRequestStatus.Success)
            {
                // Read score from the Saved Game
                try
                {
                    string scoreString = System.Text.Encoding.UTF8.GetString(data);
                    string[] scoreStringArr = scoreString.Split(',');
                    for (int i = 0; i < infoToSave; i++)
                    {
                        score[i] = Convert.ToInt32(scoreStringArr[i]);
                    }                   
                }
                catch (Exception e)
                {
                    Debug.Log("(Lollygagger) Saved Game Write: convert exception");
                }
            }
        };
        // CALLBACK: Handle the result of a read, which should return metadata
        Action<SavedGameRequestStatus, ISavedGameMetadata> readCallback =
        (SavedGameRequestStatus status, ISavedGameMetadata game) => {
            Debug.Log("(RunForLife) Saved Game Read: " + status.ToString());
            if (status == SavedGameRequestStatus.Success)
            {
                // Read the binary game data
                currentGame = game;
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game,
                                                    readBinaryCallback);
            }
        };
        ReadSavedGame("file_maxMeters", readCallback);
        return score;
    }

    public void UpdateLocalInfoRead()
    {
        MaxMeters = ReadUpdatedSaveData()[0];
        MaxMetersText.text = MaxMeters+"m";
    }
    */
    #endregion
    #endregion
    public void ToogleDeadWindow()
    {
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
        if (PlayerPrefs.HasKey("MaxMeters"))
        {
            if (PlayerPrefs.GetInt("MaxMeters")<currentMeters)
            {
                PlayerPrefs.SetInt("MaxMeters", currentMeters);
                LeaderboardUpdate(currentMeters);
            }
        }
        else
        {
            PlayerPrefs.SetInt("MaxMeters", currentMeters);
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
