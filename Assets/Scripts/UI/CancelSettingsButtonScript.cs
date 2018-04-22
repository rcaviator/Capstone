using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSettingsButtonScript : MonoBehaviour
{
    //cancel reset variables
    float previousMusicVolume;
    float previousSFXVolume;
    float previousUIVolume;

	// Use this for initialization
	void OnEnable()
    {
        previousMusicVolume = AudioManager.Instance.MusicVolume;
        previousSFXVolume = AudioManager.Instance.SoundEffectsVolume;
        previousUIVolume = AudioManager.Instance.UIVolume;
	}
	
    /// <summary>
    /// Resets the volume to what it was before.
    /// </summary>
	public void CancelVolumeChanges()
    {
        AudioManager.Instance.MusicVolume = previousMusicVolume;
        AudioManager.Instance.SoundEffectsVolume = previousSFXVolume;
        AudioManager.Instance.UIVolume = previousUIVolume;
    }
}
