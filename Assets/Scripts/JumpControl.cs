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
			if (Input.GetMouseButtonDown(0) && IsPointerOverUIObject() == false)
			{
				CR.Jump();
				//Debug.Log("Jump");
			}
		}
	}

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
