using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSightController : MonoBehaviour {

	public EnemyMovement EM;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			EM.CharacterOnSight(true);
		}
	}
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			EM.CharacterOnSight(false);
		}
	}

}
