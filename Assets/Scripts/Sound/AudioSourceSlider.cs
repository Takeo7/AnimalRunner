using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSourceSlider : MonoBehaviour {

	public Slider slider;
	public Image muteButton;
	SoundMainController SMC;
	private void Start()
	{
		SMC = SoundMainController.instance;
		SMC.muteButton = muteButton;
		SMC.soundSlider = slider;
		slider.value = SMC.SS.volume;
		slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}
	public void ValueChangeCheck()
	{
		SMC.ChangeVolume();
	}
}
