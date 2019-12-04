using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsInstantiatorDebug : MonoBehaviour
{

	public bool shoot;
	public GameObject prefab;
	public CapsuleCollider2D col;
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			shoot = true;
		}
		if (Input.GetMouseButtonDown(1))
		{
			col.enabled = true;
		}
		if (shoot)
		{
			shoot = false;
			Instantiate(prefab, transform.position,transform.rotation);
		}
    }
}
