using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class PlayerStats : MonoBehaviour {


	public int AmountHealth = 3;
    [SerializeField]
    Animator damageImage;
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
    public bool reallyDead;

    private void Start()
    {
        healthScript = Health.instance;
        damageImage = MainMenuAnimator.instance.damageImage;
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
            damageImage.SetTrigger("damage");
			AmountHealth -= i;
			healthScript.UpdateHearts(AmountHealth);
			CheckHealth();
		}
    }

    public void takeDammage(int i, bool b)
    {
        damageImage.SetTrigger("damage");
        AmountHealth -= i;
        healthScript.UpdateHearts(AmountHealth);
        CheckHealth();
    }

	void CheckHealth()
	{
		if (AmountHealth <= 0 && !isDead)
		{
			CharacterReferences CR = CharacterReferences.instance;
			isDead = true;
			gameObject.tag = "Untagged";
			CR.playerInfo.totalDeaths++;
			CR.playerInfo.totalMetersRunned += EnvironmentController.instance.UIC.currentMeters;
			CR.uic.metersRun = CR.uic.currentMeters;
			if (CR.playerInfo.metersRecord < CR.uic.metersRun)
			{
				CR.playerInfo.metersRecord = CR.uic.metersRun;
				if (PlayGamesPlatform.Instance.IsAuthenticated())
				{
					MainMenuAnimator.instance.GPL.LeaderboardUpdate(CR.uic.metersRun);
				}
			}
			//EnvironmentController.instance.gameOverDelegate();

			//gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
			//gameObject.SetActive(false);//Better to deactivate because of errors and its easy to only move and activate and not Instantiating another
			MainMenuAnimator.instance.ToggleRewardedVideoButton();
            if (reallyDead == true)
            {
                AC.DeathAnim();
            }
        }
	}
    public void Resucitate()
    {
        CharacterReferences CR = CharacterReferences.instance;
        CR.TM.dead = false;
        CR.FF.FollowFunc();
        isDead = false;
        gameObject.tag = "Player";
        reallyDead = false;
        AmountHealth = 3;
        healthScript.UpdateHearts(AmountHealth);
        CR.TM.jumps = 2;
        transform.position = new Vector3(transform.position.x, 13f, transform.position.z);
        CR.TM.rb.velocity = Vector3.zero;
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
		Rabbit,
		Unicorn,
		Okami
    }
}
