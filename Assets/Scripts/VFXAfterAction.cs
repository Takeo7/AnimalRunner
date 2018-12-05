using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAfterAction : MonoBehaviour {

	[SerializeField]
	GameObject VFXPrefab;
	[SerializeField]
	Transform instantiateOrigin;

	public void VFXInstantiate()
	{
		Instantiate(VFXPrefab, instantiateOrigin.position,VFXPrefab.transform.rotation);
	}
}
