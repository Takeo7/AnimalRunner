using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAfterAction : MonoBehaviour {

	[SerializeField]
	GameObject VFXPrefab;
	[SerializeField]
	Transform instantiateOrigin;
	[SerializeField]
	ParticleSystem VFXInScene;

	[SerializeField]
	List<GameObject> effects;

	private void Start()
	{
		if(VFXInScene != null)
		{
			VFXInScene.Stop();
		}
	}
	public void VFXInstantiate()
	{
		GameObject t = Instantiate(VFXPrefab, instantiateOrigin.position,VFXPrefab.transform.rotation);
		effects.Add(t);
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
	public void DestroyVFX()
	{
		StopCoroutine("VFXInstantiateWDelay");
		int length = effects.Count;
		for (int i = 0; i < length; i++)
		{
			Destroy(effects[i]);
		}
	}
	public void VFXPersistent(bool p)
	{
		if (p)
		{
			VFXInScene.Play();
		}
		else
		{
			VFXInScene.Stop();
		}
	}
}
