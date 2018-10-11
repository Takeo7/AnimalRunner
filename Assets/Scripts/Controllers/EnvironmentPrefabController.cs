using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPrefabController : MonoBehaviour {

	public SpriteRenderer floorRenderer;
	EnvironmentController EC;
	public BoxCollider2D collider;

	private void Start()
	{
		EC = EnvironmentController.instance;
		//StartCoroutine("DestroyAfterX", EC.timeToDestroy);
	}
	IEnumerator DestroyAfterX(float timeToDestroy)
	{
		yield return new WaitForSeconds(timeToDestroy);
		if (EC.canDestroy)
		{
			EC.prefabsInstantiated.Remove(this);
			Destroy(gameObject);
		}
	}
}
