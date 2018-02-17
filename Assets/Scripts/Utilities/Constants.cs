using System;
/// <summary>
/// All constants for the whole game
/// </summary>
public static class Constants
{
    #region Game Constants

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
    public const float LEVEL_EDITOR_SPACING = 1f;
    public const int LEVEL_EDITOR_GRID_SIZE_X = 10;//20
    public const int LEVEL_EDITOR_GRID_SIZE_Y = 5;//10
    public const int LEVEL_EDITOR_GRID_OFFSET_X = 0;//10
    public const int LEVEL_EDITOR_GRID_OFFSET_Y = 0;//5

    //settings constants


    //game play constants
    public const float CAMERA_SPEED = 10f;

    #endregion

    #region Player Constants

    public const float PLAYER_STARTING_HEALTH = 100f;
    public const float PLAYER_SCREEN_MOVEMENT_SPEED = 50f;
    public const float PLAYER_HORIZONTAL_ACCELERATION = 10f;
    public const float PLAYER_MAX_HORIZONTAL_SPEED = 100f;
    public const float PLAYER_VERTICAL_ACCELERATION = 10f;
    public const float PLAYER_MAX_VERTICAL_SPEED = 25f;
    public const float PLAYER_BASIC_BULLET_DAMAGE = 10f;
    public const float PLAYER_BASIC_BULLET_ATTACK_LIFETIME = 0.15f;
    public const float PLAYER_BASIC_BULLET_SPEED = 75f;
    public const float PLAYER_BASIC_BULLET_COOLDOWN_TIMER = 0.05f;

    #endregion

    #region Enemy Constants

    public const float ENEMY_TEMP_BLIMP_HEALTH = 100f;

    #endregion

    #region Item Constants



    #endregion
}
