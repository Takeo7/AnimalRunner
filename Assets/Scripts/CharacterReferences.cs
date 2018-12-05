using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReferences : MonoBehaviour {

	#region Singleton
	public static CharacterReferences instance;
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}
	#endregion

	public Transform characterTransform;
	public TestMovement TM;
	public PlayerStats PS;
	public AnimatorController AC;

	public void Jump()
	{
		TM.Jump();
	}
	public void Attack()
	{
		TM.AttackRanged();
	}

}
