using System;

/// <summary>
/// All constants for the whole game
/// </summary>
public static class Constants
{
    #region Object IDs

    /// <summary>
    /// All object IDs used for saving, loading, and instantiation
    /// </summary>
    public enum ObjectIDs
    {
        //default
        None,

        //utilities
        LevelStartPoint, LevelEndPoint,
        UnusedUtility1, UnusedUtility2,

        //environment - blocks
        DirtBlock, DirtBlockGrass, DirtBlockSloped, DirtBlockSlopedGrass,
        StoneBlock, StoneBlockSloped, StoneBlockConcreteTop, StoneBlockSlopedConcreteTop,
        UnusedBlock1, UnusedBlock2, UnusedBlock3, UnusedBlock4, UnusedBlock5, UnusedBlock6,

        //environment - buildings, weather, ambience
        HangarClose, HangarMiddle, HangarFar, Tower, WeatherHazard1, WeatherHazard2, WeatherHazard3,
        Bird, UnusedOther2, UnusedOther3, UnusedOther4, UnusedOther5, UnusedOther6,

        //allies
        AlliedTurret1, AlliedTurret2, WingMan1, WingMan2, UnusedAlly1, UnusedAlly2,

        //enemies
        MotherShip, Zepplin, Tank, Soldier, Jeep, Bomber, UnusedEnemey1, UnusedEnemy2,
        UnusedEnemy3, UnusedEnemy4,

        //new additions

    };

    /// <summary>
    /// enum for the type of tag an object is associated with.
    /// </summary>
    public enum Tags
    {
        //default tags
        Untagged,
        Respawn,
        Finish,
        EditorOnly,
        MainCamera,
        Player,
        GameController,

        //custom tags
        Ground,
        LevelStart,
        LevelEnd,
        Enemy,
        PlayerBullet,
        EnemyBullet,
        WeatherHazard,
        Environment,
    };

    #endregion

    #region Game Constants

    //global
    public const bool IS_DEVELOPER_BUILD = true;

    //scenes
    public const string SCENE_NAME_LEVELEDITOR = "LevelEditor";
    public const string SCENE_NAME_MAINMENU = "MainMenu";
    public const string SCENE_NAME_TUTORIAL = "Tutorial";
    public const string SCENE_NAME_PRELEVEL = "PreLevel";
    public const string SCENE_NAME_GAMELEVEL = "GameLevel";
    public const string SCENE_NAME_LEVELCOMPLETE = "LevelComplete";
    public const string SCENE_NAME_DEFEAT = "Defeat";
    public const string SCENE_NAME_VICTORY = "Victory";

    //game data constants
    public const string GAME_DATA_FILE_HEADER = "GAME";
    public const int GAME_DEFAULT_LEVEL = 1;
    public const int GAME_DEFAULT_SCORE = 0;
    public const bool GAME_DEFAULT_FINISHED_GAME = false;

    //settings constants
    public const string AUDIO_SETTINGS_FILE_HEADER = "AUDI";
    public const float AUDIO_DEFAULT_MUSIC_VOLUME = 1f;
    public const float AUDIO_DEFAULT_SOUNDEFFECTS_VOLUME = 1f;
    public const float AUDIO_DEFAULT_UI_VOLUME = 1f;

    //level editor constants
    public const int NUMBER_OF_MODULES = 9;
    public const string MODULE_FILE_HEADER = "MODU";
    public const int LEVEL_EDITOR_GRID_SIZE_X = 50;
    public const int LEVEL_EDITOR_GRID_SIZE_Y = 25;
    public const int LEVEL_EDITOR_GRID_OFFSET_X = 10;//10 finish deleting
    public const int LEVEL_EDITOR_GRID_OFFSET_Y = 5;//5 finish deleting
    public const float LEVEL_EDITOR_GRID_DRAW_OFFSET_X = 0.5f;
    public const float LEVEL_EDITOR_GRID_DRAW_OFFSET_Y = 0.5f;
    public const float LEVEL_EDITOR_GRID_DRAW_OFFSET_Z = 0f;
    public const int LEVEL_EDITOR_OBJECT_CAP = 15;

    //game level constants
    public const int MODULE_LENGTH = LEVEL_EDITOR_GRID_SIZE_X;
    public const float CAMERA_SPEED = 10f;
    public const float BULLET_RICOCHET_SPARKS_LIFETIME = 0.1f;
    public const float GROUND_DAMAGE = 5f;//delete this
    public const string STATUS_TAKEOFF_MESSAGE = "TAKING OFF!...";
    public const string STATUS_GO_MESSAGE = "GO!";
    public const string STATUS_LEVEL_FINISHED_MESSAGE = "LEVEL FINISHED!!";
    public const float STATUS_PANEL_TIMER = 2f;

    //airport text
    public const string AIRPORT_1_NAME = "Ira Sapping";
    public const string AIRPORT_1_MISSION_BRIEFING = "Briefing 1";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_1_WEATHER_BRIEFING = "Clear skies, very few clouds at high altitude. Visibility: 10+ miles.";

    public const string AIRPORT_2_NAME = "Dominick Walker";
    public const string AIRPORT_2_MISSION_BRIEFING = "Briefing 2";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_2_WEATHER_BRIEFING = "Partly cloudy with some thunderstorms in the area. Visibility 7 miles.";

    public const string AIRPORT_3_NAME = "Amos Raycraft";
    public const string AIRPORT_3_MISSION_BRIEFING = "Briefing 3";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_3_WEATHER_BRIEFING = "Mostly cloudy with thunderstorms and down drafts in the area. Visibility 5 miles.";

    public const string AIRPORT_4_NAME = "Morgan Remmington";
    public const string AIRPORT_4_MISSION_BRIEFING = "Briefing 4";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_4_WEATHER_BRIEFING = "Mixed weather with overcast and scattered thunderstorms and down drafts. Visibility mixed.";

    #endregion

    #region Player Constants

    //player
    public const float PLAYER_STARTING_HEALTH = 100f;
    public const float PLAYER_SCREEN_MOVEMENT_SPEED = 50f;
    public const float PLAYER_HORIZONTAL_ACCELERATION = 20f;
    public const float PLAYER_MAX_HORIZONTAL_SPEED = 100f;
    public const float PLAYER_VERTICAL_ACCELERATION = 20f;
    public const float PLAYER_MAX_VERTICAL_SPEED = 25f;
    public const float PLAYER_TAKEOFF_RATE = 5f;
    public const float PLAYER_TAKEOFF_GROUND_ROLL_TIMER = 2f;
    public const float PLAYER_PITCH_UP_MAX = 60f;
    public const float PLAYER_PITCH_DOWN_MAX = -60f;
    
    //basic attack bullet
    public const float PLAYER_BASIC_BULLET_DAMAGE = 10f;
    public const float PLAYER_BASIC_BULLET_ATTACK_LIFETIME = 0.15f;
    public const float PLAYER_BASIC_BULLET_SPEED = 75f;
    public const float PLAYER_BASIC_BULLET_COOLDOWN_TIMER = 0.05f;

    #endregion

    #region Environmental Constants

    //bird
    public const float BIRD_HEALTH = 1f;
    public const float BIRD_SPEED = 4f;
    public const float BIRD_LIFETIME = 20f;
    public const float BIRD_DAMAGE = 5f;

    //weather hazard 1


    //weather hazard 2


    //weather hazard 3


    #endregion

    #region Allied Constants



    #endregion

    #region Enemy Constants

    //enemy collision damage
    public const float ENEMY_COLLISION_DAMAGE = 20f;

    //enemy rockets
    public const float ENEMY_SLOW_ROCKET_HEALTH = 30f;
    public const float ENEMY_FAST_ROCKET_HEALTH = 10f;
    public const float ENEMY_SLOW_ROCKET_COOLDOWN_TIMER = 4f;
    public const float ENEMY_FAST_ROCKET_COOLDOWN_TIMER = 2f;
    public const float ENEMY_SLOW_ROCKET_DAMAGE = 30f;
    public const float ENEMY_FAST_ROCKET_DAMAGE = 10f;
    public const float ENEMY_SLOW_ROCKET_LIFETIME = 5f;
    public const float ENEMY_FAST_ROCKET_LIEFTIME = 2f;
    public const float ENEMY_SLOW_ROCKET_SPEED = 3f;
    public const float ENEMY_FAST_ROCKET_SPEED = 6f;
    public const int ENEMY_SLOW_ROCKET_SCORE = 5;
    public const int ENEMY_FAST_ROCKET_SCORE = 10;

    //zepplin
    public const float ENEMY_ZEPPLIN_HEALTH = 100f;
    public const int ENEMY_ZEPPLIN_SCORE = 20;

    public const float ENEMY_TEMP_BLIMP_HEALTH = 100f;//delete
    public const int ENEMY_TEMP_BLIMP_SCORE = 20;//delete

    //mothership
    public const float ENEMY_MOTHERSHIP_HEALTH = 20000f;

    //bomber
    public const float ENEMY_BOMBER_HEALTH = 70f;
    public const int ENEMY_BOMBER_SCORE = 30;

    //soldier
    public const float ENEMY_SOLDIER_HEALTH = 1f;
    public const int ENEMY_SOLDIER_SCORE = 1;

    //tank
    public const float ENEMY_TANK_HEALTH = 150f;
    public const int ENEMY_TANK_SCORE = 30;

    //jeep
    public const float ENEMY_JEEP_HEALTH = 30f;
    public const int ENEMY_JEEP_SCORE = 10;

    #endregion

    #region Item Constants



    #endregion
}
