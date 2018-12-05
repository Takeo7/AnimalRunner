using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVFXController : MonoBehaviour {

	[SerializeField]
	MeshRenderer meshRenderer;
	[Space]
	[Header("Prefabs")]
	[SerializeField]
	GameObject JumpFromFloor;
	[SerializeField]
	GameObject SecondJump;
	[SerializeField]
	GameObject AttackRanged;
	[SerializeField]
	GameObject Idle;
	[SerializeField]
	GameObject Death;
	[Space]
	[Header("Materials")]
	[SerializeField]
	Material StandardMaterial;
	[SerializeField]
	Material DeathMaterial;

	public void JumpFromFloorVFX()
	{
		Instantiate(JumpFromFloor, new Vector3(transform.position.x+1f,transform.position.y,transform.position.z-3f), Quaternion.identity);
	}

}
