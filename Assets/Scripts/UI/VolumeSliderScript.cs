using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    //slider reference
    Slider slider;

    //which volume to set
    [SerializeField]
    bool musicSlider;
    [SerializeField]
    bool sfxSlider;
    [SerializeField]
    bool uiSlider;

	// Use this for initialization
	void Awake()
    {
        slider = GetComponent<Slider>();

        if (musicSlider)
        {
            slider.normalizedValue = AudioManager.Instance.MusicVolume;
        }
        else if (sfxSlider)
        {
            slider.normalizedValue = AudioManager.Instance.SoundEffectsVolume;
        }
        else if (uiSlider)
        {
            slider.normalizedValue = AudioManager.Instance.UIVolume;
        }
	}
	
    /// <summary>
    /// Updates the volume in AudioManager based on what slider it is.
    /// </summary>
	public void UpdateVolume()
    {
        if (musicSlider)
        {
            AudioManager.Instance.MusicVolume = slider.normalizedValue;
        }
        else if (sfxSlider)
        {
            AudioManager.Instance.SoundEffectsVolume = slider.normalizedValue;
        }
        else if (uiSlider)
        {
            AudioManager.Instance.UIVolume = slider.normalizedValue;
        }
    }

    /// <summary>
    /// If the user clicks cancel after changing values and enters settings again,
    /// this will reset the values on the sliders to where they were before
    /// </summary>
    private void OnEnable()
    {
        if (musicSlider)
        {
            slider.normalizedValue = AudioManager.Instance.MusicVolume;
        }
        else if (sfxSlider)
        {
            slider.normalizedValue = AudioManager.Instance.SoundEffectsVolume;
        }
        else if (uiSlider)
        {
            slider.normalizedValue = AudioManager.Instance.UIVolume;
        }
    }
}
