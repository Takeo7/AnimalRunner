using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float velocity = 1;
    public int health = 100;
    public int direction;
    public int damage;
	[SerializeField]
	bool isOnSight = false;
	[SerializeField]
	bool isMelee;
	bool isAttacking;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	[SerializeField]
	Vector2 meleeAttackOffset;
	[SerializeField]
	float meleeAttackRadius;
	[SerializeField]
	LayerMask attackLayerMask;
	public float secondsToAttackAgain;

    TestMovement tm;
	[SerializeField]
    bool isDead = false;
    public bool dontDie;
    public Collider2D col;
	public Collider2D sight;

	public AnimatorController AC;//AC.TheAnimationYouWant(); EXAMPLE: AC.AttackAnim();
	public VFXAfterAction VFXAA;
	[SerializeField]
	float meleeAttackDelay;
	[SerializeField]
	GameObject coin;
	[SerializeField]
	SoundController SC;

	bool canDestroy;
	[SerializeField]
	bool flamethrower;
	bool firstflame;


	private void Start()
    {
        tm = TestMovement.instance;
        direction = Random.Range(-1, 1);
        if (direction == 0)
        {
            direction = -1;
        }
		
    }
    private void Update()
    {
        transform.Translate(new Vector3(direction * velocity*Time.deltaTime, 0));
		if (isMelee && isAttacking)
		{
			Collider2D[] temp = new Collider2D[5];
			ContactFilter2D temp2 = new ContactFilter2D();
			Physics2D.OverlapCircle(new Vector2(transform.position.x + meleeAttackOffset.x, transform.position.y + meleeAttackOffset.y), meleeAttackRadius, temp2, temp);

			byte length = (byte)temp.Length;
			for (byte i = 0; i < length; i++)
			{
				if (temp[i] != null)
				{
					if (temp[i].CompareTag("Player"))
					{
						isAttacking = false;
                        CharacterReferences.instance.PS.takeDammage(damage);
					}
				}
			}
		}
		else if(!isMelee && isAttacking && flamethrower)
		{
			Collider2D[] temp = new Collider2D[5];
			ContactFilter2D temp2 = new ContactFilter2D();
			Physics2D.OverlapCircle(new Vector2(transform.position.x + meleeAttackOffset.x, transform.position.y + meleeAttackOffset.y), meleeAttackRadius, temp2, temp);

			byte length = (byte)temp.Length;
			for (byte i = 0; i < length; i++)
			{
				if (temp[i] != null)
				{
					if (temp[i].CompareTag("Player"))
					{
						isAttacking = false;
						CharacterReferences.instance.PS.takeDammage(damage);
					}
				}
			}
		}
	}
    void ChangeDirection()
    {
        if (direction == 1)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallCheck")
        {
            ChangeDirection();
        }
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(tm.DealDamage());
        }
    }
    public void TakeDamage(int i)
    {
        health -= i;
        if (health <= 0 && isDead == false)
        {
            isAttacking = false;
            isDead = true;
            AC.TakeDamage(true);
            CharacterReferences.instance.PC.enemysKilled++;
			ChallengesController.instance.AddKills(1);
            StopCoroutine("Attacks");
			StartCoroutine("Die");
        }
		else if(health > 0 && isDead == false)
		{
			AC.TakeDamage(false);
		}

    }
	IEnumerator Die()
	{
        if (dontDie == false)
        {
			CharacterReferences.instance.playerInfo.totalEnemiesKilled++;
			StopCoroutine("Attacks");
			isAttacking = false;
			if (flamethrower)
			{
				VFXAA.VFXPersistent(false);
                SC.PauseSound();
			}
			else
			{
				VFXAA.DestroyVFX();
			}
			//Debug.Log("IsDing");
			AC.DeathAnim();
            col.enabled = false;
			sight.enabled = false;
			Instantiate(coin, transform.position+new Vector3(0,0,-7), Quaternion.identity);
			yield return new WaitForSeconds(2f);
			canDestroy = true;
        }

	}
	public void CharacterOnSight(bool _isOnSight)
	{
		isOnSight = _isOnSight;
		if (isOnSight && isDead == false)
		{
			StartCoroutine("Attacks");
		}
	}
	IEnumerator Attacks()
	{
        EnvironmentController ec = EnvironmentController.instance;
		while (isOnSight && ec.inGame)
		{
			isAttacking = true;
			if (!flamethrower)
			{
				AC.AttackAnim(false);
			}
			if (isMelee)
			{
				VFXAA.VFXInstantiate(meleeAttackDelay);
				SC.PlaySound();
				yield return new WaitForSeconds(AC.attackAnimDuration);
			}
			if (!isMelee)
			{
				if (flamethrower)
				{
					if (!firstflame)
					{
						firstflame = true;
						AC.AttackAnim(false);
						VFXAA.VFXPersistent(true);
                        SC.PlaySound(0, 0.3f);
                    }
					yield return new WaitForSeconds(AC.attackAnimDuration);
				}
				else
				{
					yield return new WaitForSeconds(AC.rangedAttackDelay);
                    GameObject bullet = Instantiate(bulletPrefab);
					VFXAA.VFXInstantiate();
                    SC.PlaySound();
                    bullet.transform.position = bulletSpawn.position;
					bullet.GetComponent<BulletController>().damage = damage;
					yield return new WaitForSeconds(AC.attackAnimDuration - AC.rangedAttackDelay);
				}
			}
			AC.IdleAnim();
			if (!flamethrower)
			{
				isAttacking = false;
			}
			yield return new WaitForSeconds(secondsToAttackAgain);
		}
		if(isOnSight == false)
		{
			AC.IdleAnim();
		}
	}
	private void OnBecameInvisible()
	{
		if (canDestroy)
		{
			Destroy(gameObject);
		}
	}
	private void OnDrawGizmosSelected()
	{
		if (isMelee)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(new Vector2(transform.position.x + meleeAttackOffset.x, transform.position.y + meleeAttackOffset.y), meleeAttackRadius);
		}
	}
}
