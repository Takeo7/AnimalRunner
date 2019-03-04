using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpControl : MonoBehaviour {

	public CharacterReferences CR;
	public EnvironmentController EC;

	private void Update()
	{
		if (EC.inGame)
		{
			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				CR.Jump();
				//Debug.Log("Jump");
			}
		}
	}
}
