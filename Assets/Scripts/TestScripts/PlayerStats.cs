using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class PlayerStats : MonoBehaviour {

    int maxHealth;
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
	[Space]
	public float resucitateRadius;
	public LayerMask resucitateMask;


	private void Start()
    {
        maxHealth = AmountHealth;
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
		//DETECT POSITION
		RaycastHit2D hit2d = new RaycastHit2D();
		bool hit = false;
		ContactFilter2D filter = new ContactFilter2D();
		filter.layerMask = resucitateMask;
		filter.useLayerMask = true;
		RaycastHit2D[] results = new RaycastHit2D[10];
		int resultsint =  Physics2D.CircleCast(transform.position, resucitateRadius, Vector3.zero, new ContactFilter2D(),results);
		int length = results.Length;
		for (int i = 0; i < length; i++)
		{
			if (results[i].collider.gameObject.CompareTag("Floor"))
			{
				hit = true;
				hit2d = results[i];
				break;
			}
		}
		if (hit)
		{
			BoxCollider2D box = hit2d.transform.GetComponent<BoxCollider2D>();
			Vector3 tempTransform = new Vector3();
			tempTransform.x = hit2d.transform.position.x + box.offset.x;
			tempTransform.y = hit2d.transform.position.y + box.offset.y;
			tempTransform.z = transform.position.z;
			transform.position = tempTransform;
			Debug.Log(tempTransform);
		}

        CharacterReferences CR = CharacterReferences.instance;
        CR.TM.dead = false;
        CR.FF.FollowFunc();
        isDead = false;
        Debug.Log(gameObject.tag);
        gameObject.tag = "Player";
        reallyDead = false;
        AmountHealth = maxHealth;
        healthScript.UpdateHearts(AmountHealth);
        CR.TM.jumps = 2;
        //transform.position = new Vector3(transform.position.x, 13f, transform.position.z);
        CR.TM.rb.velocity = Vector3.zero;
		canDie = false;
		StartCoroutine("CanDieAfterResucitate");
    }

	IEnumerator CanDieAfterResucitate()
	{
		yield return new WaitForSeconds(3f);
		canDie = true;
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, resucitateRadius);
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
