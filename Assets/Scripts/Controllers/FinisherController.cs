using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour {

	public Transform player;
    public TestMovement playerScript;
	EnvironmentController EC;

	private void Start()
	{
		EC = EnvironmentController.instance;
		StartCoroutine("Follow");
	}

	IEnumerator Follow()
	{
		while (playerScript.dead == false)
		{
			yield return new WaitForSeconds(0.01f);
			transform.position = new Vector3(player.position.x, transform.position.y);
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.dead = true;
    }
}
