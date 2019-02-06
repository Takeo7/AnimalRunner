using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public Vector3 direction;
	public float speed = 1f;

	void Update()
	{
		transform.Rotate(direction * (speed * Time.deltaTime * 100f));
	}
}
