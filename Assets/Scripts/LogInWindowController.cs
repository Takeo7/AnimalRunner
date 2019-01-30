using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInWindowController : MonoBehaviour {


    PlayFabLogin PFL;
    public Text UsernameText;

    private void Start()
    {
        PFL = PlayFabLogin.instance;
        UsernameText.text = PlayerPrefs.GetString("Username");
    }

    public void SaveUsername(Text Username)
    {
        PlayerPrefs.SetString("Username", Username.text);
    }
    public void SavePassword(Text Password)
    {
        PlayerPrefs.SetString("Password", Password.text);
    }

    public void LogInUsername()
    {
        PFL.LogInPlayFabUsername();
    }

    public void Register()
    {
        PFL.RegisterUserPlayFab();
    }
}
