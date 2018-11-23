using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    #region Singleton
    public static TestMovement instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    #endregion

    EnvironmentController EC;
    public Rigidbody2D rb;
    public float jumpForce = 10;
    public byte jumps = 2;
    byte jumpCount = 0;
    public PlayerStats ps;
	public Animator animator;
	public AnimatorController AC;
    public Collider2D col;

    public bool dead = false;
	public bool isGrounded = true;
    public bool isJumping = false;
    [SerializeField]
    float overlapRadius;
    [SerializeField]
    LayerMask floorLayer;
    bool isAnimatedOnFloor;

    [SerializeField]
    Vector3 bottomOffset;
    [SerializeField]
    Vector3 topOffset;
    [SerializeField]
    float secondOverlapRadius;
    [SerializeField]
    Vector3 sideOffset;
    [SerializeField]
    Vector2 sideSize;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int bulletDamage;

    private void Start()
	{
		EC = EnvironmentController.instance;
	}
	private void Update()
	{
        if (Physics2D.OverlapCircle(transform.position, overlapRadius,floorLayer) && isGrounded == false)
        {
            isGrounded = true;
            isJumping = false;
            ResetJumps();
            if (!ps.isDead)
            {
                AC.RunAnim();
            }
        }
        else if(Physics2D.OverlapCircle(transform.position, overlapRadius, floorLayer) && isGrounded)
        {
            isGrounded = true;
        }
        else if(Physics2D.OverlapCircle(transform.position, overlapRadius, floorLayer) == false)
        {
            isGrounded = false;
            isJumping = true;
            isAnimatedOnFloor = false;
        }

        if (Physics2D.OverlapCircle(transform.position + bottomOffset, secondOverlapRadius, floorLayer))
        {
            col.isTrigger = false;
        }
        if (Physics2D.OverlapCircle(transform.position + topOffset, secondOverlapRadius, floorLayer))
        {
            col.isTrigger = true;
        }
        if (isJumping)
        {
            if (Physics2D.OverlapBox(transform.position + sideOffset, sideSize, 0f,floorLayer))
            {
                col.isTrigger = true;
            }
        }




        if (EC.inGame)
		{
			transform.position += Vector3.right * EC.characterSpeed * Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && jumpCount<jumps)
            {
				if(jumpCount == 0)
				{
					AC.JumpOneAnim();
				}
				else if(jumpCount == 1)
				{
					AC.JumpTwoAnim();
				}
				rb.velocity = Vector3.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
		}
		if (Input.GetMouseButtonDown(1))
		{
            AttackRanged();
        }
	}

    public void AttackRanged()
    {
        AC.AttackAnim(true);
        StartCoroutine("AttackCoroutine");
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, overlapRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + bottomOffset, secondOverlapRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + topOffset, secondOverlapRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + sideOffset, sideSize);
    }

    public int DealDamage()
    {
        return bulletDamage;
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.47f);
        GameObject g = Instantiate(bulletPrefab, bulletSpawnPoint);
        StopCoroutine("AttackCoroutine");
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         //Debug.Log(collision.collider.name);
         if (collision.collider.CompareTag("Floor"))
         {
             //Debug.Log("FloorEntered");
             ResetJumps();
             if (!ps.isDead)
             {
                 AC.RunAnim();
             }
         }
     }*/
}
