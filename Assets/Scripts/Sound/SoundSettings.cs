using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundSettings : ScriptableObject {

	public float volume;
	public bool mute;
    public bool muteMusic;
    public float volumeMusic;

	public AudioClip[] musicClips;
}
