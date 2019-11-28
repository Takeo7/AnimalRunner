using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class okamiEspecialColliderScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyMovement>().TakeDamage(1);
        }
    }
}
