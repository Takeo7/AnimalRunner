using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSetter : MonoBehaviour
{
	
	public AudioSource AS;
	// Use this for initialization
	void Start()
	{
		SoundMainController SMC = SoundMainController.instance;
		SMC.speakers.Add(AS);
		AS.volume = SMC.SS.volume;
		AS.mute = SMC.SS.mute;
	}
}
