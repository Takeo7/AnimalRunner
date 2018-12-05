﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AnimatorController : MonoBehaviour {

	public SkeletonAnimation anim;
	public bool isAnimatingSpine = false;

	const byte movementTrack = 0;
	const byte actionTrack = 1;
	[SerializeField]
	string run = "run";
	[SerializeField]
	string jump = "jump";
	[SerializeField]
	string attack = "attack";
	[SerializeField]
	string secondAttack = "attack 2";
	[SerializeField]
	string secondJump = "jump2";
	[SerializeField]
	string death = "death";
	[SerializeField]
	string idle = "idle";
	[SerializeField]
	string takeDamage = "takeDamage";
	[SerializeField]
	string specialAttack = "protect";
	[SerializeField]
	float takeDamageDuration;
	[SerializeField]
	float attackAnimDuration;
	[SerializeField]
	float secondAttackAnimDuration;

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
	public void DeathAnim()
	{
		ChangeAnim(0, death, false);
	}
	public void SpecialAttack()
	{
		ChangeAnim(0, specialAttack, false);
	}
	public void TakeDamage(bool isDeadAfter)
	{
		StartCoroutine("TakeDamageAnimCO",isDeadAfter);
	}
	public void ChangeAnim(byte track,string name,bool isLoop)
	{
		if (isAnimatingSpine)
		{
			anim.state.SetAnimation(track, name, isLoop);
		}
	}
	void ClearAttackAnim()
	{
		anim.state.ClearTrack(actionTrack);
	}
	public void AttackAnim(bool isTrack)
	{
		if (isTrack)
		{
			StartCoroutine("AttackAnimCO");
		}
		else
		{
			ChangeAnim(0, attack, false);
		}
	}
	IEnumerator AttackAnimCO()
	{
		ChangeAnim(actionTrack, attack, false);
		yield return new WaitForSeconds(attackAnimDuration);
		ClearAttackAnim();
	}
	IEnumerator TakeDamageAnimCO(bool isDeadAfter)
	{
		ChangeAnim(0, takeDamage, false);
		yield return new WaitForSeconds(takeDamageDuration);
		if (isDeadAfter && anim.state.ToString() != "death")
		{
			DeathAnim();
		}
		else if(!isDeadAfter && anim.state.ToString() != "death")
		{
			IdleAnim();
		}
	}
	public void IdleAnim()
	{
		ChangeAnim(0, idle, true);
	}
	public void isFlipX(bool isFlipped)
	{
		anim.initialFlipX = isFlipped;

	}
}
