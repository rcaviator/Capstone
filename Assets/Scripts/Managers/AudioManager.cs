//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using UnityEngine;

/// <summary>
/// Music enum
/// </summary>
public enum MusicSoundEffect
{
    //default
    None,

    //menus
    MainMenu,

    //game environment

}

/// <summary>
/// UI sound effects enum
/// </summary>
public enum UISoundEffect
{
    //default
    None,

    //menus
    MenuButtonFocused, MenuForward, MenuBackward,

    //items and other

}

/// <summary>
/// Gameplay sound effects enum
/// </summary>
public enum GameSoundEffect
{
    //default
    None,

    //game and general

    
    //player

    
    //enemies


    //npcs


    //items


    //environment

}

/// <summary>
/// AudioManager is the singleton that handles all the audio in the game.
/// </summary>
class AudioManager
{
    #region Fields

    //singleton instance
    static AudioManager instance;

    //music, ui, and game sound effects dictionaries
    Dictionary<MusicSoundEffect, AudioClip> musicSoundEffectsDict;
    Dictionary<UISoundEffect, AudioClip> uiSoundEffectsDict;
    Dictionary<GameSoundEffect, AudioClip> gameSoundEffectsDict;

    //game object for audio sources
    GameObject audioController;

    //audio source references
    AudioSource musicAudioSource;
    AudioSource uiAudioSource;
    AudioSource gameAudioSource;

    //reference to currently playing background music
    MusicSoundEffect currentMusic;

    #endregion

    #region Constructor

    //private constructor
    private AudioManager()
    {
        //create and populate the music dictionary
        musicSoundEffectsDict = new Dictionary<MusicSoundEffect, AudioClip>()
        {
            //leave MusicSoundEffect.None out
            { MusicSoundEffect.MainMenu, Resources.Load<AudioClip>("Audio/Music/") },

        };

        //create and populate the UI dictionary
        uiSoundEffectsDict = new Dictionary<UISoundEffect, AudioClip>()
        {
            //leave UISoundEffect.None out
            { UISoundEffect.MenuButtonFocused, Resources.Load<AudioClip>("Audio/UI/") },
            { UISoundEffect.MenuForward, Resources.Load<AudioClip>("Audio/UI/") },
            { UISoundEffect.MenuBackward, Resources.Load<AudioClip>("Audio/UI/") },
            
        };

        //create and populate the game play sound effects dictionary
        gameSoundEffectsDict = new Dictionary<GameSoundEffect, AudioClip>()
        {
            //leave GameSoundEffect.None out
            //{ GameSoundEffect. , Resources.Load<AudioClip>("Audio/Effects/")},
            
        };

        //create audio game object
        audioController = new GameObject("AudioController");
        GameObject.DontDestroyOnLoad(audioController);

        //create audio source references
        uiAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;
        musicAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;
        gameAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;

        //set audio sources for ignore pausing
        uiAudioSource.ignoreListenerPause = true;
        musicAudioSource.ignoreListenerPause = true;
        gameAudioSource.ignoreListenerPause = false;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the audio manager
    /// </summary>
    public static AudioManager Instance
    {
        get { return instance ?? (instance = new AudioManager()); }
    }

    #region Background Music

    /// <summary>
    /// Plays a background music on loop. If a track is already playing,
    /// stops current track and plays new one.
    /// </summary>
    /// <param name="name">the enum name of the track to play</param>
    public void PlayMusic(MusicSoundEffect name)
    {
        if (musicSoundEffectsDict.ContainsKey(name))
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
                musicAudioSource.clip = musicSoundEffectsDict[name];
                musicAudioSource.Play();
                musicAudioSource.loop = true;
            }
            else
            {
                musicAudioSource.clip = musicSoundEffectsDict[name];
                musicAudioSource.Play();
                musicAudioSource.loop = true;
            }

            //set current music enum
            currentMusic = name;
        }
        else
        {
            Debug.Log("Music name " + name.ToString() + " is not in the music sound effects dictionary!");
        }
    }

    /// <summary>
    /// Stops the current music
    /// </summary>
    public void StopMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            currentMusic = MusicSoundEffect.None;
        }
        else
        {
            Debug.Log("No music is playing to stop.");
        }
    }

    /// <summary>
    /// Pauses the current music
    /// </summary>
    public void PauseMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
        }
        else
        {
            Debug.Log("No music is playing to pause.");
        }
    }

    /// <summary>
    /// Unpauses the current music
    /// </summary>
    public void UnpauseMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.UnPause();
        }
        else
        {
            Debug.Log("No music is paused to unpause.");
        }
    }

    /// <summary>
    /// Gets the enum of the current music playing
    /// </summary>
    /// <returns>the current music enum</returns>
    public MusicSoundEffect WhatMusicIsPlaying()
    {
        return currentMusic;
    }

    #endregion

    #region UI Sound effects

    /// <summary>
    /// Plays a UI sound effect
    /// </summary>
    /// <param name="name">the name of the sound effect</param>
    public void PlayUISoundEffect(UISoundEffect name)
    {
        if (uiSoundEffectsDict.ContainsKey(name))
        {
            uiAudioSource.PlayOneShot(uiSoundEffectsDict[name]);
        }
        else
        {
            Debug.Log("UI sound effect " + name.ToString() + " is not in the UI sound effects dictionary!");
        }
    }

    #endregion

    #region Game Play Sound Effects

    /// <summary>
    /// Plays a game play sound effect
    /// </summary>
    /// <param name="name">the name of the sound effect</param>
    public void PlayGamePlaySoundEffect(GameSoundEffect name)
    {
        if (gameSoundEffectsDict.ContainsKey(name))
        {
            gameAudioSource.PlayOneShot(gameSoundEffectsDict[name]);
        }
    }

    /// <summary>
    /// Pauses the game play sound effects with AudioListener
    /// </summary>
    public void PauseGamePlaySoundEffects()
    {
        if (!AudioListener.pause)
        {
            AudioListener.pause = true;
        }
        else
        {
            Debug.Log("AudioListener is already paused.");
        }
    }

    /// <summary>
    /// Unpauses the game play sound effects with AudioListener
    /// </summary>
    public void UnpauseGamePlaySoundEffects()
    {
        if (AudioListener.pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            Debug.Log("AudioListener is already unpaused.");
        }
    }

    #endregion

    #endregion
}