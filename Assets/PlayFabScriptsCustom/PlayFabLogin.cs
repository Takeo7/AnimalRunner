using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using System.Collections.Generic;

public class PlayFabLogin : MonoBehaviour
{
    #region Singleton
    public static PlayFabLogin instance;
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

    public Text DebugText;

    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "144"; // Please change this value to your own titleId from PlayFab Game Manager
        } 
    }

    public void LogInPlayFabMobile()
    {
        /*if (Application.platform == RuntimePlatform.Android)
        {
            var request = new LoginWithGoogleAccountRequest { CreateAccount = true, ServerAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode() };
            PlayFabClientAPI.LoginWithGoogleAccount(request, OnLoginSuccess, OnLoginFailure);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            /*var request = new LoginWithGoogleAccountRequest { CreateAccount = true };
            PlayFabClientAPI.LoginWithGoogleAccount(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
            var request = new LoginWithCustomIDRequest { CustomId = "TestPlayer", CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }*/
        Debug.Log("PlayFab - LogIn");
        var request = new LoginWithCustomIDRequest { CustomId = "TestPlayer", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

    }

    public void UploadUserData()
    {
        Debug.Log("PlayFab - UpdateData");
        Dictionary<string, string> data = new Dictionary<string, string>();
        data = CharacterReferences.instance.playerInfo.GetData();
        var request = new UpdateUserDataRequest { Data = data };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataSuccess, OnUpdateUserDataFailure);
    }

    private void OnUpdateUserDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("PlayFab - UpdatedDataSuccess");
        DebugText.text = "PlayFab - UpdatedDataSuccess";
    }
    private void OnUpdateUserDataFailure(PlayFabError error)
    {
        Debug.Log("PlayFab - UpdatedDataERROR");
        Debug.Log(error);
        DebugText.text = "PlayFab - UpdatedDataError";
        DebugText.text = ""+error;
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("PlayFab - Login Successful");
        DebugText.text = "PlayFab - Login Successful";
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("PlayFab - Login Failed");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        DebugText.text = "PlayFab - Login Failed || "+error;
    }
}
