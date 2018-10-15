using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

	EnvironmentController EC;
    public Rigidbody2D rb;
    public float jumpForce = 10;

    public bool dead = false;


    private void Start()
	{
		EC = EnvironmentController.instance;
	}
	private void Update()
	{
		if (EC.inGame)
		{
			transform.position += Vector3.right * EC.characterSpeed * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
		}
	}
	private void OnTriggerEnter2D(Collider2D col)
	{

		if (col.CompareTag("Destroyer"))
		{
			Debug.Log("TriggerEnter");
			EC.ChangeEnviron();
		}
	}
}
