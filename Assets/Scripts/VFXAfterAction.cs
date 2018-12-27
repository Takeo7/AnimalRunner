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
	public void VFXInstantiate(float time)
	{
		StartCoroutine("VFXInstantiateWDelay", time);
	}
	IEnumerator VFXInstantiateWDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		VFXInstantiate();
	}
}
