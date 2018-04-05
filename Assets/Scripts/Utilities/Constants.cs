using System;
/// <summary>
/// All constants for the whole game
/// </summary>
public static class Constants
{
    #region Object IDs

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

        //environment - buildings and weather
        HangarClose, HangarMiddle, HangarFar, Tower, WeatherHazard1, WeatherHazard2, WeatherHazard3,
        UnusedOther1, UnusedOther2, UnusedOther3, UnusedOther4, UnusedOther5, UnusedOther6,

        //allies
        AlliedTurret1, AlliedTurret2, WingMan1, WingMan2, UnusedAlly1, UnusedAlly2,

        //enemies
        MainBoss, TempBlimpEnemy,

        //new additions
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

    //level editor constants
    public const int NUMBER_OF_MODULES = 9;
    public const string MODULE_FILE_HEADER = "MODU";
    public const int LEVEL_EDITOR_GRID_SIZE_X = 50;//20
    public const int LEVEL_EDITOR_GRID_SIZE_Y = 25;//10
    public const int LEVEL_EDITOR_GRID_OFFSET_X = 10;//10 finish deleting
    public const int LEVEL_EDITOR_GRID_OFFSET_Y = 5;//5 finish deleting
    public const float LEVEL_EDITOR_GRID_DRAW_OFFSET_X = 0.5f;
    public const float LEVEL_EDITOR_GRID_DRAW_OFFSET_Y = 0.5f;
    public const float LEVEL_EDITOR_GRID_DRAW_OFFSET_Z = 0f;//10f;

    //settings constants


    //game level constants
    public const int MODULE_LENGTH = LEVEL_EDITOR_GRID_SIZE_X;
    public const float CAMERA_SPEED = 10f;
    public const float BULLET_RICOCHET_SPARKS_LIFETIME = 0.1f;
    public const float GROUND_DAMAGE = 5f;//delete this

    #endregion

    #region Player Constants

    public const float PLAYER_STARTING_HEALTH = 100f;
    public const float PLAYER_SCREEN_MOVEMENT_SPEED = 50f;
    public const float PLAYER_HORIZONTAL_ACCELERATION = 20f;
    public const float PLAYER_MAX_HORIZONTAL_SPEED = 100f;
    public const float PLAYER_VERTICAL_ACCELERATION = 20f;
    public const float PLAYER_MAX_VERTICAL_SPEED = 25f;
    public const float PLAYER_TAKEOFF_RATE = 5f;
    public const float PLAYER_TAKEOFF_GROUND_ROLL_TIMER = 2f;
    public const float PLAYER_BASIC_BULLET_DAMAGE = 10f;
    public const float PLAYER_BASIC_BULLET_ATTACK_LIFETIME = 0.15f;
    public const float PLAYER_BASIC_BULLET_SPEED = 75f;
    public const float PLAYER_BASIC_BULLET_COOLDOWN_TIMER = 0.05f;

    #endregion

    #region Enemy Constants

    public const float ENEMY_TEMP_BLIMP_HEALTH = 100f;
    public const int ENEMY_TEMP_BLIMP_SCORE = 20;
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

    #endregion

    #region Item Constants



    #endregion
}
