using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFloor : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FloorCheck"))
        {
            ///GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
