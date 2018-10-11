using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Transform target;

	public Text meters;

	private void Update()
	{
		meters.text = Mathf.RoundToInt(target.transform.position.x) + " m";
	}

}
