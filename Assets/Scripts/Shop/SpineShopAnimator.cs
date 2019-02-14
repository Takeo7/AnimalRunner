using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineShopAnimator : MonoBehaviour {

	[SerializeField]
	SkeletonAnimation SA;
	[SerializeField]
	float attackTime;
	private void Start()
	{
		ShopConfirmer.instance.SSA = this;
	}
	public void Attack()
	{
		StartCoroutine("AttackCO");
	}
	IEnumerator AttackCO()
	{
		ChangeAnim(1, "attack", false);
		yield return new WaitForSeconds(attackTime);
		ClearAttackAnim();
	}
	public void ChangeAnim(byte track, string name, bool isLoop)
	{
		SA.state.SetAnimation(track, name, isLoop);
	}
	void ClearAttackAnim()
	{
		SA.state.ClearTrack(1);
	}
}
