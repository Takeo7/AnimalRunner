using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantSpecialHolder : MonoBehaviour {

	[SerializeField]
	float rotationSpeed;
	[SerializeField]
	ElephantSpecial ES;
	private void Start()
	{
		RaycastHit hit;
		if (Physics.SphereCast(transform.position,ES.radius,Vector3.zero,out hit))
		{
			if (hit.collider.CompareTag("Enemy"))
			{
				ES.Fire(hit.transform.position + new Vector3(0, 0.5f, 0));
			}
		}
	}
	private void Update()
	{
		transform.Rotate(new Vector3(0, 0, 1 * rotationSpeed * Time.deltaTime), Space.Self);
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Enemy") && ES.canFire)
		{
			ES.Fire(col.transform.position+new Vector3(0,0.5f,0));
		}
	}
}
