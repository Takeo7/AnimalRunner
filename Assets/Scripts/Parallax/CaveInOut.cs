using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveInOut : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			ParallaxMainController.instance.SetParallaxNewElements();
			Debug.Log("CaveInOut");
		}
	}
}
