using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxChecker : MonoBehaviour {

	public SpriteRenderer sprite;
	public ParallaxController PC;

	private void OnBecameVisible()
	{
		//isVisible = true;
	}
	private void OnBecameInvisible()
	{
		//isVisible = false;
		if (PC.inTransition)
		{
			switch (PC.parallaxType)
			{
				case ParallaxType.Background:
					GetParallaxElements(0);
					break;
				case ParallaxType.Back:
					GetParallaxElements(1);
					break;
				case ParallaxType.Front:
					GetParallaxElements(2);
					break;
			}
		}
	}
	void GetParallaxElements(byte x)
	{
		if(EnvironmentController.instance.set.parallaxElements[0] != null)
		{
			sprite.enabled = true;
			sprite.sprite = EnvironmentController.instance.set.parallaxElements[x];
		}
		else if(EnvironmentController.instance.set.parallaxElements[0] == null)
		{
			sprite.enabled = false;
		}
	}
}
