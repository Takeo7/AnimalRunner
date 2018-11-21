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
					sprite.sprite = EnvironmentController.instance.set.parallaxElements[0];
					break;
				case ParallaxType.Back:
					sprite.sprite = EnvironmentController.instance.set.parallaxElements[1];
					break;
				case ParallaxType.Front:
					sprite.sprite = EnvironmentController.instance.set.parallaxElements[2];
					break;
			}
		}
	}
}
