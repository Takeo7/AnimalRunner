using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	#region Singleton
	public static PlayerStats instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
	public int Health = 3;
    public Health healthScript;
    public int attackDamage = 1;
    public AnimatorController AC;
	public bool isDead;

    public AttackType attackType;

    public void UpdateHealth(int i)
    {
        Health += i;
        healthScript.UpdateHearts(Health);
		CheckHealth();
    }
    public void takeDammage(int i)
    {
        Health -= i;
        healthScript.UpdateHearts(Health);
		CheckHealth();
    }

	void CheckHealth()
	{
		if (Health <= 0)
		{
			isDead = true;
			EnvironmentController.instance.gameOverDelegate();
			//gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
			//gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
			AC.DeathAnim();
		}
	}

    public enum AttackType
    {
        Body,
        Ranged
    }
}
