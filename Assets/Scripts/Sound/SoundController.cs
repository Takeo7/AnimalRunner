using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	[SerializeField]
	AudioSource AS;
	[SerializeField]
	AudioClip[] sounds;
	private void Start()
	{
		SoundMainController SMC = SoundMainController.instance;
		AS.volume = SMC.SS.volume;
		AS.mute = SMC.SS.mute;
	}
	public void PlaySound()
	{
		AS.clip = sounds[Random.Range(0, sounds.Length)];
		AS.Play();
	}
}
