using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameCenterLogin : MonoBehaviour {
    #region Singleton
    public static GameCenterLogin instance;
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

    public void LogInIOS()
    {
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);
                PlayerPrefs.SetString("CustomID", Social.localUser.userName);
                PlayFabLogin.instance.LogInCustomPlayFab();
            }
            else
                Debug.Log("Authentication failed");
        });
    }

    
}
