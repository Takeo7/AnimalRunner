using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    #region Singleton
    public static TestMovement instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public SoundController sc;
    [Space]
    public EnvironmentController EC;
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
	public GameObject muzzleFlashPrefab;
	public bool hasMuzzleFlash;
    public int bulletDamage;
	public bool jumpAnimEnabled;
    public bool jumpVFXEnabled;
    public bool secondJumpAnimEnabled;
	public bool hasSecondJumpAnim;

    public bool isSpecial;


    public CharacterVFXController VFX;

    bool attackBool = true;
	CharacterReferences CR;
	private void Start()
	{
		EC = EnvironmentController.instance;
		CR = CharacterReferences.instance;
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
        }
        if (Input.GetMouseButtonDown(1))
		{
            AttackRanged();
        }
	}

    public void Jump()
    {
        if (jumpCount < jumps && EC.inGame)
        {
			CR.playerInfo.totalJumps++;
            if (jumpCount == 0)
            {
				if (jumpAnimEnabled)
				{
					AC.JumpOneAnim();
                    if (jumpVFXEnabled)
                    {
                        if (sc != null)
                        {
                            sc.PlaySound(0);
                        }
                        VFX.JumpFromFloorVFX();
                    }

				}
            }
            else if (jumpCount == 1)
            {
				if (secondJumpAnimEnabled)
				{
					if (hasSecondJumpAnim)
					{
						AC.JumpTwoAnim();
					}
					else
					{
						AC.JumpOneAnim();
					}
					if (jumpVFXEnabled)
                    {
                        if (sc != null)
                        {
                            sc.PlaySound(0);
                        }
                        VFX.JumpSecond();
                    }
				}
            }
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    public void AttackRanged()
    {
        if (EC.inGame && attackBool && isSpecial == false)
        {
            attackBool = false;
            switch (ps.PlayerType)
            {
                case PlayerStats.Characters.Turtle:
                    AC.AttackAnim(true);
                    sc.PlaySound(1);
                    AttacksUI.instance.UpdateAttacks();
                    StartCoroutine("AttackCoroutine");
                    break;
                case PlayerStats.Characters.Elephant:
                    AC.AttackAnim(false);
                    sc.PlaySound(1);
                    AttacksUI.instance.UpdateAttacks();
                    StartCoroutine("AttackCoroutine");
                    break;
				case PlayerStats.Characters.Dragon:
					AC.AttackAnim(true);
                    sc.PlaySound(1);
                    AttacksUI.instance.UpdateAttacks();
					StartCoroutine("AttackCoroutine");
					break;
				case PlayerStats.Characters.Rabbit:
					AC.AttackAnim(true);
                    sc.PlaySound(1);
                    AttacksUI.instance.UpdateAttacks();
					StartCoroutine("AttackCoroutine");
					break;
				case PlayerStats.Characters.Unicorn:
					AC.AttackAnim(true);
                    sc.PlaySound(1);
                    AttacksUI.instance.UpdateAttacks();
					StartCoroutine("AttackCoroutine");
					break;
				case PlayerStats.Characters.Okami:
					AC.AttackAnim(true);
                    sc.PlaySound(1);
                    AttacksUI.instance.UpdateAttacks();
					StartCoroutine("AttackCoroutine");
					break;
			}           
        }
    }

    public void UpdateAttack(bool b)
    {
        attackBool = b;
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
        yield return new WaitForSeconds(0.01f);
        GameObject g = Instantiate(bulletPrefab, bulletSpawnPoint.position,Quaternion.identity);
		if (hasMuzzleFlash)
		{
			GameObject m = Instantiate(muzzleFlashPrefab, bulletSpawnPoint.position, muzzleFlashPrefab.transform.rotation);
			//m.transform.localScale = new Vector3(1, 1, 1);
		}
        //g.transform.SetParent(null);
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
