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
	public AudioSource[] speakers;
	public Image muteButton;
	public Sprite[] muteButtonImages;//0 sound 1 muted
	public Slider soundSlider;

	private void Start()
	{
		musicSpeaker.clip = SS.musicClips[Random.Range(0, SS.musicClips.Length)];
		musicSpeaker.Play();
		if (SS.mute)
		{
			//muteButton.sprite = muteButtonImages[1];
		}
		else
		{
			//muteButton.sprite = muteButtonImages[0];
		}
		ChangeVolume(SS.volume);
		MuteUnmute(SS.mute);
	}
	public void ChangeVolume()
	{
		float volume = soundSlider.value;
		int length = speakers.Length;
		for (int i = 0; i < length; i++)
		{
			speakers[i].volume = volume;
		}
		SS.volume = volume;
	}
	public void ChangeVolume(float volume)
	{
		int length = speakers.Length;
		for (int i = 0; i < length; i++)
		{
			speakers[i].volume = volume;
		}
	}
	public void MuteUnmute()
	{
		bool isMute = SS.mute;
		if (isMute)
		{
			isMute = false;
			//muteButton.sprite = muteButtonImages[0];
		}
		else
		{
			isMute = true;
			//muteButton.sprite = muteButtonImages[1];
		}
		int length = speakers.Length;
		for (int i = 0; i < length; i++)
		{
			speakers[i].mute = isMute;
		}
		SS.mute = isMute;
	}
	public void MuteUnmute(bool isMute)
	{
		int length = speakers.Length;
		for (int i = 0; i < length; i++)
		{
			speakers[i].mute = isMute;
		}
	}
}
