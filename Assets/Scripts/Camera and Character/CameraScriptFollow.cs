using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptFollow : MonoBehaviour {

    public Transform player;

    public Rigidbody2D rb;

    public float inicialY;
    public float offsetX;
    public float height;

    public bool dead = false;

    public void Start()
    {
        inicialY = transform.position.y;
    }

    private void Update()
    {
        if (dead == false)
        {
            if (player.transform.position.y >= height)
            {
                Debug.Log("Salto");
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if(player.transform.position.y <= inicialY)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            }           
        }
    }
}
