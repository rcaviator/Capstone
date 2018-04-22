using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonScript : ButtonScript
{
    //slider references
    [SerializeField]
    Slider musicVolumeSlider;
    [SerializeField]
    Slider sfxVolumeSlider;
    [SerializeField]
    Slider uiVolumeSlider;

    private void Awake()
    {
        
    }

    /// <summary>
    /// Calls upon, uses, and saves the audio settings with the default values.
    /// </summary>
    public void UseDefaultSettings()
    {
        //reset default music volume
        musicVolumeSlider.normalizedValue = Constants.AUDIO_DEFAULT_MUSIC_VOLUME;
        AudioManager.Instance.MusicVolume = Constants.AUDIO_DEFAULT_MUSIC_VOLUME;

        //reset default sfx volume
        sfxVolumeSlider.normalizedValue = Constants.AUDIO_DEFAULT_SOUNDEFFECTS_VOLUME;
        AudioManager.Instance.SoundEffectsVolume = Constants.AUDIO_DEFAULT_SOUNDEFFECTS_VOLUME;

        //reset default ui volume
        uiVolumeSlider.normalizedValue = Constants.AUDIO_DEFAULT_UI_VOLUME;
        AudioManager.Instance.UIVolume = Constants.AUDIO_DEFAULT_UI_VOLUME;

        //save the settings
        AudioManager.Instance.SaveAudioSettings();
    }

    /// <summary>
    /// Sets the AudioManager volumes with the values of the sliders and then saves
    /// those values to file.
    /// </summary>
    public void ApplyAndSaveSettings()
    {
        //set music volume
        AudioManager.Instance.MusicVolume = musicVolumeSlider.normalizedValue;

        //set sfx volume
        AudioManager.Instance.SoundEffectsVolume = sfxVolumeSlider.normalizedValue;

        //set ui volume
        AudioManager.Instance.UIVolume = uiVolumeSlider.normalizedValue;

        //save the settings
        AudioManager.Instance.SaveAudioSettings();
    }
}
