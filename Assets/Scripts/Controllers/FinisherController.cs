using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour {

	public Transform target;
	EnvironmentController EC;

	private void Start()
	{
		EC = EnvironmentController.instance;
		StartCoroutine("Follow");
	}

	IEnumerator Follow()
	{
		while (EC.inGame)
		{
			yield return new WaitForSeconds(0.5f);
			transform.position = new Vector3(target.position.x+5, transform.position.y, transform.position.z);
		}
	}
}
