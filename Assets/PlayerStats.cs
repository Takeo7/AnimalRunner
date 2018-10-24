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
            Destroy(gameObject);
        }
    }
}
