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
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float secondsToAttackAgain;

    TestMovement tm;
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
            isDead = true;
            StopCoroutine("Attacks");
			StartCoroutine("Die");
        }

    }
	IEnumerator Die()
	{
        if (dontDie)
        {
            Debug.Log("IsDing");
            AC.DeathAnim();
            yield return new WaitForSeconds(3f);
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
			AC.AttackAnim(false);
			yield return new WaitForSeconds(0.7f);
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = bulletSpawn.position;
			bullet.GetComponent<BulletController>().damage = damage;
			AC.IdleAnim();
			yield return new WaitForSeconds(secondsToAttackAgain);
		}
		if(isOnSight == false)
		{
			AC.IdleAnim();
		}
	}
}
