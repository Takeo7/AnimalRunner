using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSetter : MonoBehaviour
{
	
	public AudioSource AS;
    SoundMainController SMC;
    // Use this for initialization
    void Start()
	{
		SMC = SoundMainController.instance;
        Debug.Log(gameObject.name);
		SMC.speakers.Add(AS);
		AS.volume = SMC.SS.volume;
		AS.mute = SMC.SS.mute;
	}
    public void DeleteThisFromSpeakers()
    {
        SMC.speakers.Remove(AS);
    }
}
