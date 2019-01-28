using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantSpecialHolder : MonoBehaviour {

	[SerializeField]
	float rotationSpeed;
	[SerializeField]
	ElephantSpecial ES;
	private void Update()
	{
		transform.Rotate(new Vector3(0, 0, 1 * rotationSpeed * Time.deltaTime), Space.Self);
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("EnemiesSight") && ES.canFire)
		{
			ES.Fire(col.transform.position+new Vector3(0,0.5f,0));
		}
	}
}
