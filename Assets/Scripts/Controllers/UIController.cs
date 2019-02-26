using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Transform target;

	public Text meters;
	public int currentMeters;

    public int metersRun;

	private void Update()
	{
		currentMeters = Mathf.RoundToInt(target.transform.position.x);
		meters.text = Mathf.RoundToInt(target.transform.position.x) + " m";
	}

}
