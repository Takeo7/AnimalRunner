using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

	EnvironmentController EC;
    public Rigidbody2D rb;
    public float jumpForce = 10;
    public byte jumps = 2;
    byte jumpCount = 0;
    public PlayerStats ps;
	public Animator animator;
	public AnimatorController AC;

    public bool dead = false;
	public bool isGrounded = true;


    private void Start()
	{
		EC = EnvironmentController.instance;
	}
	private void Update()
	{
		if (EC.inGame)
		{
			transform.position += Vector3.right * EC.characterSpeed * Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && jumpCount<jumps)
            {
				animator.SetBool("isGrounded", false);
				if(jumpCount == 0)
				{
					animator.SetTrigger("Jump");
					AC.JumpOneAnim();
				}
				else if(jumpCount == 1)
				{
					animator.SetTrigger("SecondJump");
					AC.JumpTwoAnim();
				}
				rb.velocity = Vector3.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
		}
		if (Input.GetMouseButtonDown(1))
		{
			AC.DamageAnim();
		}
	}
    void ResetJumps()
    {
        jumpCount = 0;
    }
	private void OnTriggerEnter2D(Collider2D col)
	{

		if (col.CompareTag("Destroyer"))
		{
			EC.ChangeEnviron();
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
		Debug.Log(collision.collider.name);
        if (collision.collider.CompareTag("Floor"))
        {
			//Debug.Log("FloorEntered");
            ResetJumps();
			animator.SetBool("isGrounded", true);
			AC.RunAnim();
		}
    }
}
