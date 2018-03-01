using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// enum for all the scenes in the game
/// </summary>
public enum Scenes
{
    //default
    None,

    //test scenes
    LevelEditor,

    //main menu scenes
    MainMenu,

    //game scenes
    Tutorial, PreLevel, GameLevel, LevelComplete, Defeat, Victory,
}

/// <summary>
/// enum for all player starting locations per scene
/// </summary>
public enum PlayerSceneLocations
{
    //default
    None,

    //test levels
    LevelEditor,

    //all other scenes
    MainMenu, Tutorial, PreLevel, GameLevel, LevelComplete, Defeat, Victory,
}

/// <summary>
/// MyScenemanager is the singleton that handles all scene information
/// </summary>
class MySceneManager
{
    #region Fields

    //singleton instance
    static MySceneManager instance;

    //reference to event system object
    EventSystem eventSystem;

    //dictionary of scenes
    Dictionary<Scenes, string> sceneDict;

    //dictionary to hold what soundtrack to play per scene
    Dictionary<Scenes, MusicSoundEffect> soundtrackDict;

    //dictionary to hold player starting locations in each game scene
    Dictionary<PlayerSceneLocations, Vector3> playerLocations;

    #endregion

    #region Constructor

    /// <summary>
    /// constructor
    /// </summary>
    private MySceneManager()
    {
        //initialize the scene dictionary
        sceneDict = new Dictionary<Scenes, string>()
        {
            { Scenes.LevelEditor, Constants.SCENE_NAME_LEVELEDITOR },
            { Scenes.MainMenu, Constants.SCENE_NAME_MAINMENU },
            { Scenes.Tutorial, Constants.SCENE_NAME_TUTORIAL },
            { Scenes.PreLevel, Constants.SCENE_NAME_PRELEVEL },
            { Scenes.GameLevel, Constants.SCENE_NAME_GAMELEVEL },
            { Scenes.LevelComplete, Constants.SCENE_NAME_LEVELCOMPLETE },
            { Scenes.Defeat, Constants.SCENE_NAME_DEFEAT },
            { Scenes.Victory, Constants.SCENE_NAME_VICTORY},
        };

        //initialize the soundtrack dictionary
        soundtrackDict = new Dictionary<Scenes, MusicSoundEffect>()
        {
            //leave Scenes.None out
            { Scenes.MainMenu, MusicSoundEffect.MainMenu },
            { Scenes.GameLevel, MusicSoundEffect.GameLevel },
            { Scenes.PreLevel, MusicSoundEffect.PreLevel },
        };

        //initialize the player scene locations dictionary
        playerLocations = new Dictionary<PlayerSceneLocations, Vector3>()
        {
            //leave PlayerSceneLocations.None out

        };

        //register scene change delegate
        SceneManager.sceneLoaded += OnLevelLoaded;

        //register scene unloaded delegate
        SceneManager.sceneUnloaded += OnLevelUnloaded;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the scene manager
    /// </summary>
    public static MySceneManager Instance
    {
        get { return instance ?? (instance = new MySceneManager()); }
    }

    /// <summary>
    /// The current scene
    /// </summary>
    public Scenes CurrentScene
    { get; set; }

    /// <summary>
    /// The previous scene
    /// </summary>
    public Scenes PreviousScene
    { get; set; }


    //public bool EditorScene
    //{ get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Changes the scene
    /// </summary>
    /// <param name="name">the scene to change to</param>
    public void ChangeScene(Scenes name)
    {
        if (sceneDict.ContainsKey(name))
        {
            SceneManager.LoadScene(sceneDict[name]);
        }
    }

    /// <summary>
    /// Used for updating the scene
    /// </summary>
    /// <param name="name"></param>
    /// <param name="entity"></param>
    public void UpdateScene(Scenes name, GameObject entity)
    {

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Called when the scene changes and things need to update on scene change
    /// </summary>
    /// <param name="scene">the new scene</param>
    /// <param name="mode">loaded scene mode</param>
    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //update event system axis
        InputManager.Instance.UpdateEventSystemAxis();

        //get scene reference
        CurrentScene = sceneDict.Keys.First(t => sceneDict[t] == scene.name);

        //change soundtracks if needed
        if (soundtrackDict.ContainsKey(CurrentScene))
        {
            if (!(soundtrackDict[CurrentScene] == AudioManager.Instance.WhatMusicIsPlaying()))
            {
                AudioManager.Instance.PlayMusic(soundtrackDict[CurrentScene]);
            }
        }
        else
        {
            Debug.Log("MySceneManager sountrackDict does not contain key " + CurrentScene.ToString() + " for changing sountracks!");
            AudioManager.Instance.StopMusic();
        }

        //set the editor status if the scene is the level editor
        //if (CurrentScene == Scenes.LevelEditor)
        //{
        //    GameManager.Instance.IsLevelEditor = true;
        //    Debug.Log("level editor scene");
        //}

        //temporary score reset
        if (CurrentScene == Scenes.MainMenu)
        {
            GameManager.Instance.Score = 0;
        }
    }

    /// <summary>
    /// Called when the scene is unloaded and things need to update
    /// </summary>
    /// <param name="scene">the unloaded scene</param>
    void OnLevelUnloaded(Scene scene)
    {
        PreviousScene = sceneDict.Keys.First(x => sceneDict[x] == scene.name);

        //if (PreviousScene == Scenes.LevelEditor)
        //{
        //    GameManager.Instance.IsLevelEditor = false;
        //    Debug.Log("left the editor scene");
        //}
    }

    #endregion

    #region SceneData class

    class SceneData
    {
        //enemies, items, chests, environmental
    }

    #endregion
}