using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMeteor : MonoBehaviour {

	[SerializeField]
	int damage = 1;
	[SerializeField]
	bool canDestroy;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<EnemyMovement>().TakeDamage(damage);
			canDestroy = true;
		}
	}
	private void OnBecameInvisible()
	{
		if (canDestroy)
		{
			Destroy(gameObject);
		}
	}
}
