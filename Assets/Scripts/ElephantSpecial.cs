using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantSpecial : MonoBehaviour {

	#region Singleton
	public static ElephantSpecial instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
	[SerializeField]
	GameObject customBullet;
	[SerializeField]
	Transform specialHolder;
	[SerializeField]
	Transform[] ballParents;
	[SerializeField]
	PlayerBulletScript[] PBS;
	CharacterReferences CR;
	[SerializeField]
	byte bulletFiredCount;

	public void Special()
	{
		StartCoroutine("Countdown");
		if (CR == null)
		{
			CR = CharacterReferences.instance;
		}
		bulletFiredCount = 0;
		specialHolder.gameObject.SetActive(true);
		int length = ballParents.Length;
		for (int i = 0; i < length; i++)
		{
			PBS[i] = Instantiate(customBullet, ballParents[i]).GetComponent<PlayerBulletScript>();
		}
	}
	public void Fire(Vector3 targetPosition)
	{
		PBS[bulletFiredCount].transform.parent = null;
		Vector3 direction = targetPosition - transform.position;
		Quaternion toRotation = Quaternion.FromToRotation(transform.right, direction);
		PBS[bulletFiredCount].transform.rotation = toRotation;
		PBS[bulletFiredCount].enabled = true;
		bulletFiredCount++;
		if (bulletFiredCount >=7)
		{
			EndSpecial();
		}
	}
	public void EndSpecial()
	{
		specialHolder.gameObject.SetActive(false);
	}
	IEnumerator Countdown()
	{
		yield return new WaitForSeconds(28f);
		int length = PBS.Length;
		for (int i = 0; i < length; i++)
		{
			if(PBS[i] != null)
			{
				Fire(transform.position+new Vector3(20,0,0));
			}
		}
	}
}
