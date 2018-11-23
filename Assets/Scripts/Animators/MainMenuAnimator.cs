using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnimator : MonoBehaviour {

	public Animator animator;

    public GameObject nonTouch;
    public GameObject settingsWindow;
    public GameObject shopWindow;
    public GameObject IntroWindow;
    public GameObject deadWindow;

    public GameObject Lifes;
    public GameObject Meters;
    public GameObject AttackButton;


    private void Start()
    {
        ToogleIntro();
        EnvironmentController.instance.gameOverDelegate += ToogleDeadWindow;
    }

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
