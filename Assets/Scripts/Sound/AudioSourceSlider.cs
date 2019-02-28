using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSourceSlider : MonoBehaviour {

	public Slider slider;
	public Image muteButton;
	SoundMainController SMC;
    public bool isMusic;

	private void Start()
	{
        SMC = SoundMainController.instance;
        if (isMusic)
        {
            SMC.muteButtonMusic = muteButton;
            SMC.soundSliderMusic = slider;
            slider.value = SMC.SS.volumeMusic;
        }
        else
        {
            SMC.muteButton = muteButton;
            SMC.soundSlider = slider;
            slider.value = SMC.SS.volume;
        }
		slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}
	public void ValueChangeCheck()
	{
        if (isMusic)
        {
            SMC.ChangeVolumeMusic();
        }
        else
        {
            SMC.ChangeVolume();
        }
		
	}
}
