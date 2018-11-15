using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimatorController : MonoBehaviour {

	public SkeletonAnimation anim;
	public bool isAnimatingSpine = false;

	public void ChangeAnim(string name,bool isLoop)
	{
		if (isAnimatingSpine)
		{
			anim.state.SetAnimation(0, name, isLoop);
		}
	}
}
