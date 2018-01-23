//using System;
//using System.Collections;
using System.Collections.Generic;
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

    #endregion

    #region Constructor

    /// <summary>
    /// Private constructor
    /// </summary>
    private GameManager()
    {
        // Creates the object that calls GM's Update method
        Object.DontDestroyOnLoad(new GameObject("gmUpdater", typeof(Updater)));

        //create the list of pausable objects
        PauseableObjects = new List<PauseableObject>();
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
    /// The accessor for the player
    /// </summary>
    public GameObject Player
    { get; set; }
    
    /// <summary>
    /// The list of all pauseable game objects
    /// </summary>
    public List<PauseableObject> PauseableObjects
    { get; set; }

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
