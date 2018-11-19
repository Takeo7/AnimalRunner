using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float velocity = 1;
    public int health = 100;
    public int direction;
    public int damage;

	public AnimatorController AC;//AC.TheAnimationYouWant(); EXAMPLE: AC.AttackAnim();

    private void Start()
    {
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
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int i)
    {
        health -= i;
        if (health <= 0)
        {
			AC.DeathAnim();
            //Destroy(gameObject);
        }

    }
	IEnumerator DestroyAfterX(float x)
	{
		yield return new WaitForSeconds(x);
		Destroy(gameObject);
	}
	//ANIMS:
	//On changing direction: 
	//To make him look at right: isFlipX(true);
	//To make him look at left: isFLipX(false);

}
