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
	public SpriteRenderer BackgroundSky;
	private void Start()
	{
		SetParallaxNewElements();
		//Debug.Log("START");
	}
	public void SetParallaxNewElements()
	{
		BackgroundSky.sprite = EnvironmentController.instance.set.parallaxElements[3];
		byte length = (byte)secondControllers.Length;
		for (int i = 0; i < length; i++)
		{
			secondControllers[i].set = EnvironmentController.instance.set.setType;
			for (int j = 0; j < 3; j++)
			{
				SetElements(secondControllers[i],secondControllers[i].sprites[j]);
			}
			secondControllers[i].SetHeight();
		}
	}
	void SetElements(ParallaxController secondController,SpriteRenderer sprite)
	{
		//Debug.Log("SEt ELEMENTS");
		switch (secondController.parallaxType)
		{
			case ParallaxType.Background:
				GetParallaxElements(0,sprite);
				break;
			case ParallaxType.Back:
				GetParallaxElements(1, sprite);
				break;
			case ParallaxType.Front:
				GetParallaxElements(2, sprite);
				break;
		}
	}
	void GetParallaxElements(byte x,SpriteRenderer sprite)
	{
		if (EnvironmentController.instance.set.parallaxElements[x] != null)
		{
			sprite.enabled = true;
			sprite.sprite = EnvironmentController.instance.set.parallaxElements[x];
		}
		else if (EnvironmentController.instance.set.parallaxElements[x] == null)
		{
			sprite.enabled = false;
		}
	}
}
public enum ParallaxType { Background, Back, Front }
