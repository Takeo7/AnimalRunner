using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimatorController : MonoBehaviour {

	public SkeletonAnimation anim;
	public bool isAnimatingSpine = false;

	const byte movementTrack = 0;
	const byte actionTrack = 1;

	private void Start()
	{
		anim.state.Data.SetMix("run", "jump", 0.15f);
		anim.state.Data.SetMix("jump", "run", 0.15f);
		anim.state.SetAnimation(actionTrack,"idle",true);
		anim.state.SetAnimation(movementTrack, "run", true);
		
	}

	public void ChangeAnim(byte track,string name,bool isLoop)
	{
		if (isAnimatingSpine)
		{
			anim.state.SetAnimation(track, name, isLoop);
		}
	}
}
