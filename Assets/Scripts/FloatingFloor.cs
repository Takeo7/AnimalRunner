using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFloor : MonoBehaviour {

    public Collider2D upChecker;
    public Collider2D floorCol;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("MakeNotTrigger");
            floorCol.isTrigger = false;
        }
    }
}
