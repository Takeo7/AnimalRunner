using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {


	[SerializeField]
	GameObject hit;
    public float speed;
    public int damage;
	public bool isSpecial;
	public bool cantMove = false;
	public ParticleSystem[] FX;
	public BoxCollider2D col;
	public GameObject sprite;

    

    private void Start()
    {
		if (isSpecial)
		{
			StartCoroutine("DestroyAfterX", 5);
		}
		else
		{
			StartCoroutine("DestroyAfterX", 2);
		}

    }

    private void Update()
    {
		if (!cantMove)
		{
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
			collision.GetComponent<EnemyMovement>().TakeDamage(damage);
            DestroyBullet(true);
        }
    }
	void StopFX()
	{
		if(sprite != null)
		{
			sprite.SetActive(false);
		}
		byte length = (byte)FX.Length;
		for (int i = 0; i < length; i++)
		{
			FX[i].Stop();
		}
	}
    IEnumerator DestroyAfterX(float seconds)
    {
        yield return new WaitForSeconds(seconds);
		if (cantMove == false)
		{
			col.enabled = false;
			cantMove = true;
			StopFX();
			yield return new WaitForSeconds(2f);
		}
		Destroy(gameObject);
    }
    void DestroyBullet(bool enemyCollided)
    {
		//Effect
		if (!isSpecial)
		{
			col.enabled = false;
			cantMove = true;
			StopFX();
			StartCoroutine("DestroyAfterX", 2);
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
