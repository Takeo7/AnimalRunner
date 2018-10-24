using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderTest : MonoBehaviour {

    public TestAttack ta;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            ta.SetEnemys(collision.gameObject);
        }
    }
}
