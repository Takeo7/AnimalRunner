using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSetter : MonoBehaviour
{
	
	public AudioSource AS;
    SoundMainController SMC;
    public bool ismusic;
    // Use this for initialization
    void Start()
	{
		SMC = SoundMainController.instance;		
        if (ismusic)
        {
            AS.volume = SMC.SS.volumeMusic;
            AS.mute = SMC.SS.muteMusic;
        }
        else
        {
            AS.volume = SMC.SS.volume;
            AS.mute = SMC.SS.mute;
            SMC.speakers.Add(AS);
        }
	}
    public void DeleteThisFromSpeakers()
    {
        if(AS != null)
        {
            SMC.speakers.Remove(AS);
        }
    }
}
