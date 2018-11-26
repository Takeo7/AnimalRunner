using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public float damage;

	private void Start()
	{
		StartCoroutine("DestroyAfterX",3);
	}

	private void Update()
	{
		transform.Translate(Vector3.left * speed * Time.deltaTime);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			DestroyBullet(true);
		}
	}
	IEnumerator DestroyAfterX(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		DestroyBullet(false);
	}
	void DestroyBullet(bool characterCollided)
	{
		if (characterCollided)
		{
			PlayerStats.instance.takeDammage((int)damage);
		}
		//Effect
		Destroy(gameObject);
	}

}
