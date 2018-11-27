using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {


    public float speed;
    public int damage;

    private void Start()
    {
        StartCoroutine("DestroyAfterX", 1);
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
        DestroyBullet(false);
    }
    void DestroyBullet(bool enemyCollided)
    {
        //Effect
        Destroy(gameObject);
    }
}
