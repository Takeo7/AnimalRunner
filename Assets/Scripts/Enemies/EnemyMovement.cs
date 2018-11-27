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

	public AnimatorController AC;//AC.TheAnimationYouWant(); EXAMPLE: AC.AttackAnim();

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
						PlayerStats.instance.takeDammage(damage);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            ChangeDirection();
        }
		else if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().UpdateHealth(damage);
			StartCoroutine("Die");
        }
    }
    public void TakeDamage(int i)
    {
        health -= i;
        if (health <= 0 && isDead == false)
        {
			AC.TakeDamage(true);
			isDead = true;
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
            //Debug.Log("IsDing");
            AC.DeathAnim();
            yield return new WaitForSeconds(4f);
            Destroy(gameObject);
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
		while (isOnSight)
		{
			isAttacking = true;
			AC.AttackAnim(false);
			yield return new WaitForSeconds(0.7f);
			if (!isMelee)
			{
				GameObject bullet = Instantiate(bulletPrefab);
				bullet.transform.position = bulletSpawn.position;
				bullet.GetComponent<BulletController>().damage = damage;
			}
			AC.IdleAnim();
			isAttacking = false;
			yield return new WaitForSeconds(secondsToAttackAgain);
		}
		if(isOnSight == false)
		{
			AC.IdleAnim();
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
