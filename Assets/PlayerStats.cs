using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int Health = 3;

    public void UpdateHealth(int i)
    {
        Health += i;
        if (Health <= 0)
        {
            EnvironmentController.instance.gameOverDelegate();
            gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
        }
    }
}
