using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInWindowController : MonoBehaviour {

	public void SaveEmail(Text Email)
    {
        PlayerPrefs.SetString("Email", Email.text);
    }
    public void SavePassword(Text Password)
    {
        PlayerPrefs.SetString("Password", Password.text);
    }
}
