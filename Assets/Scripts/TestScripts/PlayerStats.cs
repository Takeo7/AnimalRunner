using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int Health = 3;
    public Health healthScript;
    public int attackDamage = 1;
    public AnimatorController ac;

    public AttackType attackType;

    public void UpdateHealth(int i)
    {
        Health += i;
        healthScript.UpdateHearts(Health);
        if (Health <= 0)
        {
            EnvironmentController.instance.gameOverDelegate();
            gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
        }
    }
    public void takeDammage(int i)
    {
        ac.DamageAnim();
        Health -= i;
        healthScript.UpdateHearts(Health);
        if (Health <= 0)
        {
            EnvironmentController.instance.gameOverDelegate();
            gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
        }
    }

    public enum AttackType
    {
        Body,
        Ranged
    }
}
