using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMainController : MonoBehaviour {

	#region DontDestroyOnLoad & singleton
	public static SoundMainController instance;
	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	public SoundSettings SS;
	public AudioSource musicSpeaker;
	public List<AudioSource> speakers = new List<AudioSource>();
	public Image muteButton;
	public Sprite[] muteButtonImages;//0 sound 1 muted
	public Slider soundSlider;

    public AudioSource Music;
    public Image muteButtonMusic;
    public Slider soundSliderMusic;

	private void Start()
	{
		musicSpeaker.clip = SS.musicClips[Random.Range(0, SS.musicClips.Length)];
		musicSpeaker.Play();
		if (SS.mute)
		{
			muteButton.sprite = muteButtonImages[1];
		}
		else
		{
			muteButton.sprite = muteButtonImages[0];
		}
		if(speakers.Count > 0)
		{
			ChangeVolume(SS.volume);
			MuteUnmute(SS.mute);
		}
        EnvironmentController.instance.gameOverDelegate += StopSoundsDead;
    }
	public void ChangeVolume()
	{
		float volume = soundSlider.value;
		for (int i = 0; i < speakers.Count; i++)
		{
			if(speakers[i] == null)
			{
				speakers.Remove(speakers[i]);
			}
			else if(i != 3)
			{
				speakers[i].volume = volume;
			}
		}
		SS.volume = volume;
	}
	public void ChangeVolume(float volume)
	{
		for (int i = 0; i < speakers.Count; i++)
		{
			if (speakers[i] == null)
			{
				speakers.Remove(speakers[i]);
			}
			else
			{
                if (i == 3)
                {
                    speakers[i].volume = SS.volumeMusic;
                }
                else
                {
                    speakers[i].volume = volume;
                }				
			}
		}
	}

    public void ChangeVolumeMusic()
    {
        float volume = soundSliderMusic.value;       
        Music.volume = volume;        
        SS.volumeMusic = volume;
    }

    public void MuteUnmuteMusic()
    {
        bool isMute = SS.muteMusic;
        if (isMute)
        {
            isMute = false;
            muteButtonMusic.sprite = muteButtonImages[2];
        }
        else
        {
            isMute = true;
            muteButtonMusic.sprite = muteButtonImages[3];
        }
        if (Music != null)
        {
            Music.mute = isMute;
        }
        SS.muteMusic = isMute;
    }
	public void MuteUnmute()
	{
		bool isMute = SS.mute;
		if (isMute)
		{
			isMute = false;
			muteButton.sprite = muteButtonImages[0];
		}
		else
		{
			isMute = true;
			muteButton.sprite = muteButtonImages[1];
		}
		int length = speakers.Count;
		for (int i = 0; i < length; i++)
		{
            if (speakers[i] != null)
            {
                speakers[i].mute = isMute;
            }else
            {
                speakers.Remove(speakers[i]);
            }
			
		}
		SS.mute = isMute;
	}
	public void MuteUnmute(bool isMute)
	{
		int length = speakers.Count;
		for (int i = 0; i < length; i++)
		{
			speakers[i].mute = isMute;
		}
	}
    public void StopSoundsDead()
    {
        int length = speakers.Count;
        for (int i = 0; i < length; i++)
        {
            if (speakers[i] != null)
            {
                speakers[i].Stop();
            }            
        }
    }
}
