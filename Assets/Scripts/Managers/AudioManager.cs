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
    MainMenu, Tutorial, PreLevel, EndLevel, LevelEditor,

    //game environment
    GameLevel,
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
    PlayerAirplaneEngine,

    //weapons
    GunFire1, GunFire2, GunFire3, RocketFire1, RocketFire2,

    //impacts
    BulletGlassImpact, BulletMetalImpact1, BulletMetalImpact2, BulletMetalImpact3,
    BulletMetalImpact4, BulletMetalImpact5, BulletStoneImpact1, BulletStoneImpact2,
    BulletStoneImpact3, BulletWoodImpact1, BulletWoodImpact2,

    //explosions
    Blast1, Blast2, Blast3, Blast4, Blast5, Blast6,

    //enemies
    EnemyBomberEngine,

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
            { MusicSoundEffect.MainMenu, Resources.Load<AudioClip>("Audio/Music/Victoria 2 Soundtrack - We Have Independence") },
            { MusicSoundEffect.GameLevel, Resources.Load<AudioClip>("Audio/Music/Cyberden") },
            { MusicSoundEffect.Tutorial, Resources.Load<AudioClip>("Audio/Music/") },
            { MusicSoundEffect.PreLevel, Resources.Load<AudioClip>("Audio/Music/Hearts of Iron IV - Luftwaffe Strikers Again") },
            { MusicSoundEffect.EndLevel, Resources.Load<AudioClip>("Audio/Music/") },
            { MusicSoundEffect.LevelEditor, Resources.Load<AudioClip>("Audio/Music/") },
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
            { GameSoundEffect.Blast1 , Resources.Load<AudioClip>("Audio/Effects/Blast1")},
            { GameSoundEffect.Blast2 , Resources.Load<AudioClip>("Audio/Effects/Blast2")},
            { GameSoundEffect.Blast3 , Resources.Load<AudioClip>("Audio/Effects/Blast3")},
            { GameSoundEffect.Blast4 , Resources.Load<AudioClip>("Audio/Effects/Blast4")},
            { GameSoundEffect.Blast5 , Resources.Load<AudioClip>("Audio/Effects/Blast5")},
            { GameSoundEffect.Blast6 , Resources.Load<AudioClip>("Audio/Effects/Blast6")},
            { GameSoundEffect.BulletGlassImpact , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_glass22_01")},
            { GameSoundEffect.BulletMetalImpact1 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_metal02_22")},
            { GameSoundEffect.BulletMetalImpact2 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_metal03_22")},
            { GameSoundEffect.BulletMetalImpact3 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_metal04_22")},
            { GameSoundEffect.BulletMetalImpact4 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_metal05_22")},
            { GameSoundEffect.BulletMetalImpact5 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_metal06_me_22")},
            { GameSoundEffect.BulletStoneImpact1 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_stone01_22")},
            { GameSoundEffect.BulletStoneImpact2 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_stone02_22")},
            { GameSoundEffect.BulletStoneImpact3 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_stone22_02")},
            { GameSoundEffect.BulletWoodImpact1 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_wood22_01")},
            { GameSoundEffect.BulletWoodImpact2 , Resources.Load<AudioClip>("Audio/Effects/bullet_hit_wood22_02")},
            { GameSoundEffect.EnemyBomberEngine, Resources.Load<AudioClip>("Audio/Effects/Bomber sound")},
            { GameSoundEffect.GunFire1, Resources.Load<AudioClip>("Audio/Effects/gun_fixedgun22_07e")},
            { GameSoundEffect.GunFire2, Resources.Load<AudioClip>("Audio/Effects/gun_assault_rifle_withbullet22_01")},
            { GameSoundEffect.GunFire3 , Resources.Load<AudioClip>("Audio/Effects/gun_uzi_withbullet22_01d")},
            { GameSoundEffect.PlayerAirplaneEngine, Resources.Load<AudioClip>("Audio/Effects/Airplane sound")},
            { GameSoundEffect.RocketFire1, Resources.Load<AudioClip>("Audio/Effects/gun_rocketlauncher22")},
            { GameSoundEffect.RocketFire2, Resources.Load<AudioClip>("Audio/Effects/gun_hybrid_rocket01b_22")},
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

    /// <summary>
    /// The music volume
    /// </summary>
    public float MusicVolume
    { get; set; }

    /// <summary>
    /// The sound effects volume
    /// </summary>
    public float SoundEffectsVolume
    { get; set; }

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

    /// <summary>
    /// Gets a game sound effect audio clip
    /// </summary>
    /// <param name="effect">the clip to get</param>
    /// <returns>the audio clip</returns>
    public AudioClip GetAudioClip(GameSoundEffect effect)
    {
        if (gameSoundEffectsDict.ContainsKey(effect))
        {
            return gameSoundEffectsDict[effect];
        }
        else
        {
            return null;
        }
    }

    #endregion

    #endregion
}