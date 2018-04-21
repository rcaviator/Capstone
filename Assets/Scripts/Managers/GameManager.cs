﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// GameManger is the main manager for the whole game.
/// </summary>
class GameManager
{
    #region Fields

    //singleton instance
    static GameManager instance;

    //bool for pausing
    bool isPaused;

    //the player's inventory
    Inventory playerInventory;

    #endregion

    #region Constructor

    /// <summary>
    /// Private constructor
    /// </summary>
    private GameManager()
    {
        // Creates the object that calls GM's Update method
        UnityEngine.Object.DontDestroyOnLoad(new GameObject("gmUpdater", typeof(Updater)));

        //create and initialize the tag dictionary
        GameObjectTags = new Dictionary<Constants.Tags, string>()
        {
            //default tags
            { Constants.Tags.Untagged, "Untagged" },
            { Constants.Tags.Respawn, "Respawn" },
            { Constants.Tags.Finish, "Finish" },
            { Constants.Tags.EditorOnly, "EditorOnly" },
            { Constants.Tags.MainCamera, "MainCamera" },
            { Constants.Tags.Player, "Player" },
            { Constants.Tags.GameController, "GameController" },

            //custom tags
            { Constants.Tags.Ground, "Ground" },
            { Constants.Tags.LevelStart, "LevelStart" },
            { Constants.Tags.LevelEnd, "LevelEnd" },
            { Constants.Tags.Enemy, "Enemy" },
            { Constants.Tags.PlayerBullet, "PlayerBullet" },
            { Constants.Tags.EnemyBullet, "EnemyBullet" },
            { Constants.Tags.WeatherHazard, "WeatherHazard" },
            { Constants.Tags.Environment, "Environment" },
        };

        //create the list of pausable objects
        PauseableObjects = new List<PauseableObject>();

        //load game data


        //load player inventory
        PlayerInventory.LoadInventory();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the game manager
    /// </summary>
    public static GameManager Instance
    {
        get { return instance ?? (instance = new GameManager()); }
    }

    /// <summary>
    /// The game object tags
    /// </summary>
    public Dictionary<Constants.Tags, string> GameObjectTags
    { get; private set; }

    /// <summary>
    /// Are the objects being spawned in the game or level editor scene
    /// </summary>
    public bool IsLevelEditor
    { get; set; }

    /// <summary>
    /// The camera the player stays within
    /// </summary>
    public CameraControllerScript PlayerCamera
    { get; set; }

    /// <summary>
    /// The accessor for the player
    /// </summary>
    public PlayerScript Player
    { get; set; }

    /// <summary>
    /// The target reticle of the player
    /// </summary>
    public TargetReticle Reticle
    { get; set; }

    /// <summary>
    /// The level number of the game
    /// </summary>
    public int Level
    { get; set; }
    /// <summary>
    /// The game score
    /// </summary>
    public int Score
    { get; set; }
    
    /// <summary>
    /// The list of all pauseable game objects
    /// </summary>
    public List<PauseableObject> PauseableObjects
    { get; set; }

    /// <summary>
    /// The distance slider at the top of the screen in the level
    /// </summary>
    public ProgressSliderScript ProgressSlider
    { get; set; }

    /// <summary>
    /// Reference to the starting trigger
    /// </summary>
    public StartLevelTriggerScript StartTrigger
    { get; set; }

    /// <summary>
    /// Reference to the ending trigger
    /// </summary>
    public EndLevelTriggerScript EndTrigger
    { get; set; }

    /// <summary>
    /// Reference to the level editor controller script
    /// </summary>
    public LevelEditorControllerScript EditorController
    { get; set; }

    /// <summary>
    /// Used for displaying an error message in the main menu
    /// </summary>
    public string ErrorMessage
    { get; set; }

    /// <summary>
    /// The player's inventory
    /// </summary>
    public Inventory PlayerInventory
    {
        get { return playerInventory ?? (playerInventory = new Inventory(Application.dataPath + "/GameData/" + "InventoryData.dat")); }
    }

    /// <summary>
    /// Is the game paused
    /// </summary>
    public bool Paused
    {
        get
        {
            return isPaused;
        }
        set
        {
            //if they're both true, do nothing
            if (value && isPaused)
            {
                return;
            }

            isPaused = value;

            //pause the objects
            if (isPaused)
            {
                AudioManager.Instance.PauseGamePlaySoundEffects();

                foreach (PauseableObject pauseObject in PauseableObjects)
                {
                    pauseObject.PauseObject();
                }
            }
            //unpause the objects
            else
            {
                AudioManager.Instance.UnpauseGamePlaySoundEffects();

                foreach (PauseableObject pauseObject in PauseableObjects)
                {
                    pauseObject.UnPauseObject();
                }
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds an object that needs to be pauseable to the list
    /// </summary>
    /// <param name="pauseObject">the object to pause</param>
    public void AddPauseableObject(PauseableObject pauseObject)
    {
        PauseableObjects.Add(pauseObject);
    }

    /// <summary>
    /// Removes an object from the pauseable list
    /// </summary>
    /// <param name="pauseObject"></param>
    public void RemovePauseableObject(PauseableObject pauseObject)
    {
        PauseableObjects.Remove(pauseObject);
    }

    /// <summary>
    /// Resets the game
    /// </summary>
    public void ResetGame()
    {
        //handle the player
        if (Player != null)
        {
            MonoBehaviour.Destroy(Player);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates the game manager (called from updater class)
    /// </summary>
    private void Update()
    {
        //call ui manager
        UIManager.Instance.Update();
    }

    #endregion

    #region Game Data Class
    
    [Serializable]
    class GameData
    {
        #region Data Fields

        int level;
        int score;


        #endregion

        #region Constructor

        //public GameData()
        //{

        //}

        #endregion

        #region Properties



        #endregion

        #region Methods

        //public void LoadData()
        //{

        //}

        #endregion
    }

    #endregion

    #region Internal Updater class

    #region Public methods



    #endregion

    /// <summary>
    /// Internal class that updates the game manager
    /// </summary>
    class Updater : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            Instance.Update();
        }
    }

    #endregion
}
