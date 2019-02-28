using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour {

	public CharacterReferences CR;
    public EnvironmentController enviroment;
	EnvironmentController EC;

	private void Start()
	{
		EC = EnvironmentController.instance;
		StartCoroutine("Follow");
	}

    public void FollowFunc()
    {
        StartCoroutine("Follow");
    }
	IEnumerator Follow()
	{
		while (CR.TM.dead == false)
		{
			yield return new WaitForSeconds(0.01f);
			transform.position = new Vector3(CR.TM.transform.position.x, transform.position.y);
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
			CR.TM.dead = true;
            CR.PS.takeDammage(CR.PS.AmountHealth, true);
			MainMenuAnimator.instance.isFall = true;
			//enviroment.TriggerEndGame();
        }

    }
}
