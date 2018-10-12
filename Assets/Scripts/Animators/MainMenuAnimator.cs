using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimator : MonoBehaviour {

	public Animator animator;

	public void ToggleOff()
	{
		animator.SetTrigger("ToggleOff");
	}

	public void StartGame()
	{
		EnvironmentController.instance.StartGame();
	}
}
