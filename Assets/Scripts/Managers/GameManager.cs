using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    //the player's health
    float playerHealth;

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
            { Constants.Tags.Bomber, "Bomber" },
            { Constants.Tags.Jeep, "Jeep" },
            { Constants.Tags.MotherShip, "MotherShip" },
            { Constants.Tags.Soldier, "Soldier" },
            { Constants.Tags.Tank, "Tank" },
            { Constants.Tags.Zepplin, "Zepplin" },
            { Constants.Tags.EnemyFastRocket, "EnemyFastRocket" },
            { Constants.Tags.EnemySlowRocket, "EnemySlowRocket" },
            { Constants.Tags.Bird, "Bird" },
            { Constants.Tags.HeavyProjectileShell, "HeavyProjectileShell" },
            { Constants.Tags.WeatherHazard2, "WeatherHazard2" },
            { Constants.Tags.WeatherHazard3, "WeatherHazard3" },
            { Constants.Tags.ClusterBomb, "ClusterBomb" },
            { Constants.Tags.EnergyBeam, "EnergyBeam" },
            { Constants.Tags.EnergyShield, "EnergyShield" },
            { Constants.Tags.SeekerMissile, "SeekerMissile" },
            { Constants.Tags.Decoy, "Decoy" },
            { Constants.Tags.PlayerAdvancedBullet, "PlayerAdvancedBullet" },
            { Constants.Tags.LightningBolt, "LightningBolt" },
        };

        //create and initialize the dictionary of airports
        Airports = new Dictionary<int, Airport>()
        {
            { 1, new Airport(Constants.AIRPORT_1_NAME, Constants.AIRPORT_2_NAME, Constants.AIRPORT_1_MISSION_BRIEFING, Resources.Load<Sprite>("Graphics/UI/Maps/Level1Map"), Constants.AIRPORT_1_WEATHER_BRIEFING) },
            { 2, new Airport(Constants.AIRPORT_2_NAME, Constants.AIRPORT_3_NAME, Constants.AIRPORT_2_MISSION_BRIEFING, Resources.Load<Sprite>("Graphics/UI/Maps/Level2Map"), Constants.AIRPORT_2_WEATHER_BRIEFING) },
            { 3, new Airport(Constants.AIRPORT_3_NAME, Constants.AIRPORT_4_NAME, Constants.AIRPORT_3_MISSION_BRIEFING, Resources.Load<Sprite>("Graphics/UI/Maps/Level3Map"), Constants.AIRPORT_3_WEATHER_BRIEFING) },
            { 4, new Airport(Constants.AIRPORT_4_NAME, "Capital City", Constants.AIRPORT_4_MISSION_BRIEFING, Resources.Load<Sprite>("Graphics/UI/Maps/Level4Map"), Constants.AIRPORT_4_WEATHER_BRIEFING) },
        };

        //create the list of pausable objects
        PauseableObjects = new List<PauseableObject>();

        //set game data file path and load game data
        GameDataFile = Application.dataPath + "/GameData/" + "GameData.dat";
        LoadGameData();

        //load player inventory
        PlayerInventory.LoadInventory();

        //testing items, currency, and level editor
        if (Constants.IS_DEVELOPER_BUILD)
        {
            PlayerInventory.AddItem(ItemType.AircraftHull, 1);
            PlayerInventory.AddItem(ItemType.RepairPack, 1);
            PlayerInventory.AddItem(ItemType.FlightEngineer, 1);

            Level = 4;

            Score = 1000;

            FinishedGame = true;
        }
        //Level = 4;
        //set player health
        PlayerHealth = Constants.PLAYER_STARTING_HEALTH;
        for (int i = 0; i < PlayerInventory.ViewItemCount(ItemType.AircraftHull); i++)
        {
            PlayerHealth += Constants.AIRCRAFT_HULL_BONUS;
        }
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
    /// The main game data file
    /// </summary>
    public string GameDataFile
    { get; private set; }

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
    /// Used as the name for what object killed the player
    /// </summary>
    public string DeathObjectName
    { get; set; }

    /// <summary>
    /// Used as the image for what object killed the player
    /// </summary>
    public Sprite DeathObjectSprite
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
    /// Used for saving the score before starting a level
    /// or restarting a level
    /// </summary>
    public int PreScore
    { get; set; }

    /// <summary>
    /// The game score
    /// </summary>
    public int Score
    { get; set; }

    /// <summary>
    /// If the player has beaten the game
    /// </summary>
    public bool FinishedGame
    { get; set; }
    
    /// <summary>
    /// The list of all pauseable game objects
    /// </summary>
    public List<PauseableObject> PauseableObjects
    { get; set; }

    /// <summary>
    /// The dictionary of airports for pre level scenes
    /// </summary>
    public Dictionary<int, Airport> Airports
    { get; private set; }

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

    ///// <summary>
    ///// Reference to the pre level controller
    ///// </summary>
    //public PreLevelMenuControllerScript PreLevelMenuController
    //{ get; set; }

    /// <summary>
    /// Used for displaying an error message in the main menu
    /// </summary>
    public string ErrorMessage
    { get; set; }

    /// <summary>
    /// Used for setting the player's health including hull bonuses
    /// </summary>
    public float PlayerHealth
    { get; set; }

    /// <summary>
    /// Used for referencing the energy shield of the player
    /// </summary>
    public EnergyShieldScript Shield
    { get; set; }

    /// <summary>
    /// Used for getting the boss
    /// </summary>
    public MotherShipScript Boss
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
    /// Loads the game data to set where the player left off.
    /// </summary>
    public void LoadGameData()
    {
        //load the file if it exists
        if (File.Exists(GameDataFile))
        {
            //open stream
            using (Stream fs = File.OpenRead(GameDataFile))
            {
                //create reader
                using (BinaryReader br = new BinaryReader(fs))
                {
                    //verify the file is the correct format
                    string head = new string(br.ReadChars(4));
                    if (!head.Equals(Constants.GAME_DATA_FILE_HEADER))
                    {
                        Debug.Log("File not of correct format");
                        return;
                    }

                    //set level, must be 1 or higher
                    Level = br.ReadInt32();
                    if (Level < Constants.GAME_DEFAULT_LEVEL)
                    {
                        Level = Constants.GAME_DEFAULT_LEVEL;
                    }

                    //set score, must be 0 or higher
                    Score = br.ReadInt32();
                    if (Score < Constants.GAME_DEFAULT_SCORE)
                    {
                        Score = Constants.GAME_DEFAULT_SCORE;
                    }

                    //set if finished
                    FinishedGame = br.ReadBoolean();
                }
            }
        }
        else
        {
            //set default values and save data
            Level = Constants.GAME_DEFAULT_LEVEL;
            Score = Constants.GAME_DEFAULT_SCORE;
            FinishedGame = Constants.GAME_DEFAULT_FINISHED_GAME;

            SaveGameData();
        }
    }

    /// <summary>
    /// Saves the player's progress and score
    /// </summary>
    public void SaveGameData()
    {
        //create the folder if it does not exist
        if (!Directory.Exists(Application.dataPath + "/GameData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/GameData");
        }

        //override the file
        if (File.Exists(GameDataFile))
        {
            //Debug.Log("Deleting " + file);
            File.Delete(GameDataFile);
        }

        //open stream
        using (Stream fs = File.OpenWrite(GameDataFile))
        {
            //create writer
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                //write header
                bw.Write(Constants.GAME_DATA_FILE_HEADER.ToCharArray());

                //write the level
                bw.Write(Level);

                //write the score
                bw.Write(Score);

                //write if the player has beaten the game
                bw.Write(FinishedGame);
            }
        }
    }

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
