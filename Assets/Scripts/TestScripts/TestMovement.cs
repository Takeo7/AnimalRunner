using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

	EnvironmentController EC;

	private void Start()
	{
		EC = EnvironmentController.instance;
	}
	private void Update()
	{
		if (EC.inGame)
		{
			transform.Translate(Vector3.right * EC.characterSpeed * Time.deltaTime);
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
