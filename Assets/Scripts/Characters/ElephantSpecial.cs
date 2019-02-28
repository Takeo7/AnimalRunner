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
	public float radius;
	public bool canFire;
    [Space]
    public SoundController sc;
	public void Special()
	{
		StartCoroutine("Countdown");
		StartCoroutine("CanFire");
		if (CR == null)
		{
			CR = CharacterReferences.instance;
		}
		bulletFiredCount = 0;
		int length = ballParents.Length;
		for (int i = 0; i < length; i++)
		{
			PBS[i] = Instantiate(customBullet, ballParents[i]).GetComponent<PlayerBulletScript>();
		}
		specialHolder.gameObject.SetActive(true);
        sc.PlaySound(2);
		Debug.Log("Instantiated Bullets");
	}
	public void Fire(Vector3 targetPosition)
	{
		PBS[bulletFiredCount].tag = "PlayerBullet";
		PBS[bulletFiredCount].transform.parent = null;
		Vector3 direction = targetPosition - transform.position;
		Quaternion toRotation = Quaternion.FromToRotation(transform.right, direction);
		PBS[bulletFiredCount].transform.rotation = toRotation;
		PBS[bulletFiredCount].enabled = true;
        sc.PlaySound(1);
		bulletFiredCount++;
		Debug.Log("Firing"+bulletFiredCount);
		if (bulletFiredCount >=8)
		{
			EndSpecial();
		}
	}
	public void Fire(Vector3 targetPosition,int i)
	{
		PBS[i].transform.parent = null;
		Vector3 direction = targetPosition - transform.position;
		Quaternion toRotation = Quaternion.FromToRotation(transform.right, direction);
		PBS[i].transform.rotation = toRotation;
		PBS[i].enabled = true;
        sc.PlaySound(1);
		Debug.Log("Firing" + bulletFiredCount);
	}
	public void EndSpecial()
	{
		StopCoroutine("Countdown");
		specialHolder.gameObject.SetActive(false);
		SpecialsUI.instance.SetCooldown();
	}
	IEnumerator Countdown()
	{
		yield return new WaitForSeconds(10f);
		Debug.Log("Countdown");
		int length = PBS.Length;
		for (int i = 0; i < length; i++)
		{
			if(PBS[i] != null)
			{
				Fire(transform.position+new Vector3(20,0,0),i);
			}
		}
		bulletFiredCount = 0;
		EndSpecial();
	}
	IEnumerator CanFire()
	{
		yield return new WaitForSeconds(2f);
		canFire = true;
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
