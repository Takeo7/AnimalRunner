using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {


	public int AmountHealth = 3;
    [SerializeField]
    Health healthScript;
    public int attackDamage = 1;
    public int numAttacks;
    public AnimatorController AC;
	public bool isDead;
    public float coldownTime;
	public bool canDie = true;

    public Image AttackImage;

    public Characters PlayerType;
    public AttackType attackType;

    private void Start()
    {
        healthScript = Health.instance;
        //Debug.Log(healthScript);
    }

    public void UpdateHealth(int i)
    {
		if (!isDead)
		{
			AmountHealth += i;
			healthScript.UpdateHearts(AmountHealth);
			CheckHealth();
		}
    }
    public void takeDammage(int i)
    {
		if (canDie && !isDead)
		{
			AmountHealth -= i;
			healthScript.UpdateHearts(AmountHealth);
			CheckHealth();
		}
    }

	void CheckHealth()
	{
		if (AmountHealth <= 0 && !isDead)
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

    public enum Characters
    {
        Turtle,
        Elephant,
		Dragon,
		Rabbit
    }
}
