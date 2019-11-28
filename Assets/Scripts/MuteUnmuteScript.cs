using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteUnmuteScript : MonoBehaviour {

	public void MuteUnmute()
    {
        SoundMainController.instance.MuteUnmute();
    }

    public void MuteUnmuteMusic()
    {
        SoundMainController.instance.MuteUnmuteMusic();
    }
}
