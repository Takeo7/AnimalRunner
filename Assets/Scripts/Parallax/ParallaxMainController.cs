using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMainController : MonoBehaviour {

	#region Singleton
	public static ParallaxMainController instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion
	public ParallaxController[] secondControllers;
	public void SetInTransition()
	{
		int length = secondControllers.Length;
		for (int i = 0; i < length; i++)
		{
			secondControllers[i].inTransition = true;
		}
	}
}
public enum ParallaxType { Background, Back, Front }
