using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {


    public Rigidbody2D rb;
    public float force;

	void Start () {
        rb.AddForce((Vector2.right * force) + Vector2.up*force/2 , ForceMode2D.Impulse);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false)
        {
            Destroy(gameObject,3f);
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
