using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour {

    GameObject[] Enemys;
    public GameObject attackCollider;
    public PlayerStats ps;

    public void Attack()
    {
        switch (ps.attackType)
        {
            case PlayerStats.AttackType.Body:
                attackCollider.SetActive(true);
                StartCoroutine("StopAttack");
                break;
            case PlayerStats.AttackType.Ranged:
                break;
            default:
                break;
        }
    }

    public void SetEnemys(GameObject[] e)
    {
        Enemys = e;
    }
    public void SetEnemys(GameObject e)
    {
        Enemys = new GameObject[1];
        Enemys[0] = e;
    }

    public void AttackEnemys()
    {
        int length = Enemys.Length;
        for (int i = 0; i < length; i++)
        {
            Enemys[i].GetComponent<EnemyMovement>().TakeDamage(ps.attackDamage);
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(0.1f);
        AttackEnemys();
    }
    

}
