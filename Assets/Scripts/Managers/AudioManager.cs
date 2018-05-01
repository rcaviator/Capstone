using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
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
    MenuButtonFocused, MenuForward, MenuBackward, MenuSwitch,

    //items and other
    BuyItem, StartGame,
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
    EnergyShield,

    //environment
    Thunder,
}

/// <summary>
/// AudioManager is the singleton that handles all the audio in the game.
/// </summary>
[Serializable]
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

    //music volume
    float musicVolume;

    //sfx volume
    float sfxVolume;

    //ui volume
    float uiVolume;

    //file of the audio settings
    string file;

    //reference to currently playing background music
    MusicSoundEffect currentMusic;

    #endregion

    #region Constructor

    //private constructor
    private AudioManager()
    {
        //create and populate the music dictionary
        if (Constants.USING_RELEASE_ASSETS)
        {
            musicSoundEffectsDict = new Dictionary<MusicSoundEffect, AudioClip>()
            { 
                //leave MusicSoundEffect.None out
                { MusicSoundEffect.MainMenu, Resources.Load<AudioClip>("Audio/Release Music/Take a Chance") },
                { MusicSoundEffect.GameLevel, Resources.Load<AudioClip>("Audio/Release Music/Volatile Reaction") },
                { MusicSoundEffect.Tutorial, Resources.Load<AudioClip>("Audio/Release Music/") },
                { MusicSoundEffect.PreLevel, Resources.Load<AudioClip>("Audio/Release Music/Undaunted") },
                { MusicSoundEffect.EndLevel, Resources.Load<AudioClip>("Audio/Release Music/") },
                { MusicSoundEffect.LevelEditor, Resources.Load<AudioClip>("Audio/Release Music/") },
            };
        }
        else
        {
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
        }

        //create and populate the game play sound effects dictionary
        if (Constants.USING_RELEASE_ASSETS)
        {
            gameSoundEffectsDict = new Dictionary<GameSoundEffect, AudioClip>()
            {
                //leave GameSoundEffect.None out
                { GameSoundEffect.Blast1 , Resources.Load<AudioClip>("Audio/Release Effects/Explosion 1")},
                { GameSoundEffect.Blast2 , Resources.Load<AudioClip>("Audio/Release Effects/Explosion 2")},
                { GameSoundEffect.Blast3 , Resources.Load<AudioClip>("Audio/Release Effects/Explosion 3")},
                { GameSoundEffect.Blast4 , Resources.Load<AudioClip>("Audio/Release Effects/Explosion 4")},
                { GameSoundEffect.Blast5 , Resources.Load<AudioClip>("Audio/Release Effects/Explosion 5")},
                { GameSoundEffect.Blast6 , Resources.Load<AudioClip>("Audio/Release Effects/Explosion 6")},
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
                { GameSoundEffect.GunFire1, Resources.Load<AudioClip>("Audio/Release Effects/Bullet")},
                { GameSoundEffect.GunFire2, Resources.Load<AudioClip>("Audio/Release Effects/Tank Shell")},
                { GameSoundEffect.GunFire3 , Resources.Load<AudioClip>("Audio/Effects/")},
                { GameSoundEffect.PlayerAirplaneEngine, Resources.Load<AudioClip>("Audio/Effects/Airplane sound")},
                { GameSoundEffect.RocketFire1, Resources.Load<AudioClip>("Audio/Release Effects/Rocket 1")},
                { GameSoundEffect.RocketFire2, Resources.Load<AudioClip>("Audio/Release Effects/Rocket 2")},
                { GameSoundEffect.EnergyShield, Resources.Load<AudioClip>("Audio/Effects/Energy Shield") },
                { GameSoundEffect.Thunder, Resources.Load<AudioClip>("Audio/Effects/Thunder") },
            };
        }
        else
        {
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
                { GameSoundEffect.EnergyShield, Resources.Load<AudioClip>("Audio/Effects/Energy Shield") },
                { GameSoundEffect.Thunder, Resources.Load<AudioClip>("Audio/Effects/Thunder") },
            };
        }

        //create and populate the UI dictionary
        uiSoundEffectsDict = new Dictionary<UISoundEffect, AudioClip>()
        {
            //leave UISoundEffect.None out
            { UISoundEffect.MenuButtonFocused, Resources.Load<AudioClip>("Audio/UI/Button Focused") },
            { UISoundEffect.MenuForward, Resources.Load<AudioClip>("Audio/UI/Forward") },
            { UISoundEffect.MenuBackward, Resources.Load<AudioClip>("Audio/UI/Back") },
            { UISoundEffect.MenuSwitch, Resources.Load<AudioClip>("Audio/UI/Switch Menu") },
            { UISoundEffect.BuyItem, Resources.Load<AudioClip>("Audio/UI/Buy Item") },
            { UISoundEffect.StartGame, Resources.Load<AudioClip>("Audio/UI/Start Game") },
        };

        //create audio game object
        audioController = new GameObject("AudioController");
        GameObject.DontDestroyOnLoad(audioController);

        //create audio source references
        musicAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;
        gameAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;
        uiAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;

        //set audio sources for ignore pausing
        musicAudioSource.ignoreListenerPause = true;
        gameAudioSource.ignoreListenerPause = false;
        uiAudioSource.ignoreListenerPause = true;

        //set file path for settings
        file = Application.dataPath + "/GameData/" + "AudioSettings.dat";

        //load audio settings
        LoadAudioSettings();
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
    {
        get { return musicVolume; }
        set
        {
            musicVolume = value;
            musicVolume = Mathf.Clamp(musicVolume, 0f, 1f);

            musicAudioSource.volume = musicVolume;
        }
    }

    /// <summary>
    /// The sound effects volume
    /// </summary>
    public float SoundEffectsVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = value;
            sfxVolume = Mathf.Clamp(sfxVolume, 0f, 1f);

            gameAudioSource.volume = sfxVolume;
        }
    }

    /// <summary>
    /// The UI volume
    /// </summary>
    public float UIVolume
    {
        get { return uiVolume; }
        set
        {
            uiVolume = value;
            uiVolume = Mathf.Clamp(uiVolume, 0f, 1f);

            uiAudioSource.volume = uiVolume;
        }
    }

    #endregion

    #region Save and Load audio settings file

    /// <summary>
    /// Loads the saved audio settings from file
    /// </summary>
    public void LoadAudioSettings()
    {
        //load the file if it exists
        if (File.Exists(file))
        {
            //open stream
            using (Stream fs = File.OpenRead(file))
            {
                //create reader
                using (BinaryReader br = new BinaryReader(fs))
                {
                    //verify the file is the correct format
                    string head = new string(br.ReadChars(4));
                    if (!head.Equals(Constants.AUDIO_SETTINGS_FILE_HEADER))
                    {
                        Debug.Log("File not of correct format");
                        return;
                    }

                    //get the music volume
                    MusicVolume = br.ReadSingle();

                    //get the sfx volume
                    SoundEffectsVolume = br.ReadSingle();

                    //get the ui volume
                    UIVolume = br.ReadSingle();
                }
            }
        }
        else
        {
            //set the default music volume
            MusicVolume = Constants.AUDIO_DEFAULT_MUSIC_VOLUME;

            //set the default sfx volume
            SoundEffectsVolume = Constants.AUDIO_DEFAULT_SOUNDEFFECTS_VOLUME;

            //set the default ui volume
            UIVolume = Constants.AUDIO_DEFAULT_UI_VOLUME;

            //save data
            SaveAudioSettings();
        }
    }

    /// <summary>
    /// Saves the audio settings to file
    /// </summary>
    public void SaveAudioSettings()
    {
        //create the folder if it does not exist
        if (!Directory.Exists(Application.dataPath + "/GameData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/GameData");
        }

        //override the file
        if (File.Exists(file))
        {
            //Debug.Log("Deleting " + file);
            File.Delete(file);
        }

        //open stream
        using (Stream fs = File.OpenWrite(file))
        {
            //create writer
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                //write header
                bw.Write(Constants.AUDIO_SETTINGS_FILE_HEADER.ToCharArray());

                //write the music volume
                bw.Write(MusicVolume);

                //write the sfx volume
                bw.Write(SoundEffectsVolume);

                //write the ui volume
                bw.Write(UIVolume);
            }
        }
    }

    #endregion

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
}