using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterX : MonoBehaviour {

	[SerializeField]
	float secondsToDestroy;
	[SerializeField]
	float rotation;
	[SerializeField]
	bool rotate;

	private void Start()
	{
		if (rotate)
		{
			transform.eulerAngles = new Vector3(rotation, 0, 0);
		}
		StartCoroutine("DestroyAfterXCO");
	}
	IEnumerator DestroyAfterXCO()
	{
		yield return new WaitForSeconds(secondsToDestroy);
		//Debug.Log(transform.rotation);
		Destroy(gameObject);
	}
}
