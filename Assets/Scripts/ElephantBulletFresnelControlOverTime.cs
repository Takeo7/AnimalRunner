using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBulletFresnelControlOverTime : MonoBehaviour {

	public SpriteRenderer mesh;

	private void Update()
	{
		mesh.material.SetFloat("_FresnelControl",Mathf.PingPong(Time.time, 1f));
	}
}
