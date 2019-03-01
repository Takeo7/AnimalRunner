using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyerScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<SoundController>().FadeSound();
        }
    }
}
