using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class FillAmountVariation : MonoBehaviour {

	public OutlineEffect OE;
	public float speed;
	public float maxOutline;

	private void Update()
	{
		OE.fillAmount = Mathf.PingPong(Time.time*speed, maxOutline);
	}
}
