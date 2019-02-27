using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {


	[SerializeField]
	GameObject hit;
    public float speed;
    public int damage;
	public bool isSpecial;

    

    private void Start()
    {
		if (isSpecial)
		{
			StartCoroutine("DestroyAfterX", 5);
		}
		else
		{
			StartCoroutine("DestroyAfterX", 1);
		}

    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyMovement>().TakeDamage(damage);
            DestroyBullet(true);
        }
    }
    IEnumerator DestroyAfterX(float seconds)
    {
        yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
    }
    void DestroyBullet(bool enemyCollided)
    {
		//Effect
		if (!isSpecial)
		{
			Destroy(gameObject);
		}
		if (enemyCollided)
		{
            if (hit != null)
            {
                Instantiate(hit, transform.position, hit.transform.rotation);
            }			
		}
    }
}
