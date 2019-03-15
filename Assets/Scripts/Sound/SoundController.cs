using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	[SerializeField]
	AudioSource AS;
	[SerializeField]
	AudioClip[] sounds;

    [Space]
    [SerializeField]
    bool onStart = false;
    public bool isFlame = false;


	private void Start()
	{
		SoundMainController SMC = SoundMainController.instance;
		AS.volume = SMC.SS.volume;
		AS.mute = SMC.SS.mute;
        if (onStart)
        {
            PlaySound();
        }
	}

    public void FadeSound()
    {
        if (isFlame)
        {
            StartCoroutine("FadeSoundCoroutine");
        }
        else
        {
            AS.mute = true;
        }
        
    }

	public void PlaySound()
	{
		AS.clip = sounds[Random.Range(0, sounds.Length)];
		AS.Play();
	}

    public void PlaySound(byte sound)
    {
        //Debug.Log("Sound made");
        AS.clip = sounds[sound];
        AS.Play();
    }

    public void PlaySound(byte sound, float volume)
    {
        //Debug.Log("Sound made");
        AS.volume = AS.volume - volume;
        AS.clip = sounds[sound];
        AS.Play();
    }

    public void PauseSound()
    {
        AS.Stop();
    }

    public IEnumerator FadeSoundCoroutine()
    {
        while (AS.volume > 0)
        {
            AS.volume -= 0.1f;
            yield return new WaitForSeconds(0.25f);
        }
        AS.Stop();
    }
}
