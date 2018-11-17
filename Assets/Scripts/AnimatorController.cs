using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimatorController : MonoBehaviour {

	public SkeletonAnimation anim;
	public bool isAnimatingSpine = false;

	const byte movementTrack = 0;
	const byte actionTrack = 1;

	string run = "run";
	string jump = "jump";
	string attack = "attack";
	string secondJump = "jump2";

	private void Start()
	{
		anim.state.Data.SetMix(run, jump, 0.15f);
		anim.state.Data.SetMix(jump, run, 0.15f);
	}
    public void RunAnim()
	{
		anim.timeScale = 1;
		ChangeAnim(movementTrack, run, true);
	}
	public void JumpOneAnim()
	{
		ChangeAnim(0, jump, false);
	}
	public void JumpTwoAnim()
	{
		anim.timeScale = 1.5f;
		ChangeAnim(0, secondJump, false);
	}
	public void ChangeAnim(byte track,string name,bool isLoop)
	{
		if (isAnimatingSpine)
		{
			anim.state.SetAnimation(track, name, isLoop);
		}
	}
	void ClearDamageAnim()
	{
		anim.state.ClearTrack(actionTrack);
	}
	public void DamageAnim()
	{
		StartCoroutine("DamageAnimCO");
	}
	IEnumerator DamageAnimCO()
	{
		ChangeAnim(actionTrack, attack, false);
		yield return new WaitForSeconds(0.5f);
		ClearDamageAnim();
	}
}
