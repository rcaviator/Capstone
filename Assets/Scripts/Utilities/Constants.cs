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
        Bomber,
        Jeep,
        MotherShip,
        Soldier,
        Tank,
        Zepplin,
        EnemyFastRocket,
        EnemySlowRocket,
        Bird,
        HeavyProjectileShell,
        WeatherHazard2,
        WeatherHazard3,
        ClusterBomb,
        EnergyBeam,
        EnergyShield,
        SeekerMissile,
        Decoy,
        PlayerAdvancedBullet,
        LightningBolt,
    };

    #endregion

    #region Game Constants

    //global
    public const bool IS_DEVELOPER_BUILD = false;
    public const bool USING_RELEASE_ASSETS = true;

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
    public const string AIRPORT_1_MISSION_BRIEFING = "Alright listen up lieutenant! " +
        "Only three scouts from the patrol survived to bring us disturbing news. " + 
        "The Empire has developed a new flying superweapon capable of destroying entier cities! " + 
        "Now normally we would contact HQ up in the north and start planning a counter attack, " + 
        "but the Empire knows we have intel on their weapon and has destroyed the communications network " + 
        "in this region, so we need you to fly this intel to HQ personally. Be warned, the Empire " + 
        "will try to stop you, so expect resistance. Dismissed pilot!";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_1_WEATHER_BRIEFING = "Clear skies, very few clouds at high altitude.\nVisibility: 10+ miles.";

    public const string AIRPORT_2_NAME = "Dominick Walker";
    public const string AIRPORT_2_MISSION_BRIEFING = "We have intel on enemy ground activity building up on the border. " + 
        "It would seem they are preparing for an offensive on our millitary bases in that region. " + 
        "With the intel you have provided, this would explain the enemy movements. " +
        "They may be planning a diversion. " +
        "We have been trying to restore communications to HQ, lieutenant, but the repairs will take " + 
        "longer than we have time for, so you get out there and deliver this intel!";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_2_WEATHER_BRIEFING = "Partly cloudy with some thunderstorms in the area.\nVisibility 7 miles.";

    public const string AIRPORT_3_NAME = "Amos Raycraft";
    public const string AIRPORT_3_MISSION_BRIEFING = "There are several reports coming to us of enemy engagements " + 
        "at the border and our forces are tempting to hold them off. What you bring to us " + 
        "shows this is a diversion to pull our units away from the capital while this superweapon " + 
        "moves against the capital. We detected an EMP burst several minutes ago that knocked out " + 
        "our communication array and we have been on high alert since. There will be enemy forces " + 
        "headed our way soon, you best get this intel over to HQ as soon as possible. Dissmissed!";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_3_WEATHER_BRIEFING = "Mostly cloudy with thunderstorms and down drafts in\nthe area.\nVisibility 5 miles.";

    public const string AIRPORT_4_NAME = "Morgan Remmington";
    public const string AIRPORT_4_MISSION_BRIEFING = "That EMP burst you said Amos Raycraft experienced was also " + 
        "experienced here. That was one hell of an EMP blast, and this intel matches up with that ship being where the EMP originated. " + 
        "As such, we also have no communications to HQ, but - What was that Private? Detecting a massive ship on radar? - " + 
        "Change in plan Lieutenant, you are not going to HQ, we need you to stop that ship before it gets to the capital!" +
        "we have very little forces defending the capital, so we need you up in the air immediately! God speed!";
    //public const Image AIRPORT_1_MAP
    public const string AIRPORT_4_WEATHER_BRIEFING = "Mixed weather with overcast and scattered thunderstorms\nand down drafts.\nVisibility mixed.";

    #endregion

    #region Player Constants

    //player
    public const float PLAYER_STARTING_HEALTH = 200f;//200f;
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
    public const float PLAYER_BASIC_BULLET_LIFETIME = 0.15f;
    public const float PLAYER_BASIC_BULLET_SPEED = 75f;
    public const float PLAYER_BASIC_BULLET_COOLDOWN_TIMER = 0.05f;

    #endregion

    #region Environmental Constants

    //bird
    public const float BIRD_HEALTH = 1f;
    public const float BIRD_SPEED = 4f;
    public const float BIRD_LIFETIME = 20f;
    public const float BIRD_COLLISION_DAMAGE = 5f;

    //weather hazard 1


    //weather hazard 2
    public const float WEATHER_HAZARD_2_LIGHTNING_TIMER = 2f;
    public const float WEATHER_HAZARD_2_LIGHTING_DAMAGE = 10f;
    public const float LIGHTNING_LIFETIME = 0.5f;
    public const float LIGHTNING_FLICKER_TIMER = 0.1f;

    //weather hazard 3
    public const float WEATHER_HAZARD_3_DOWNDRAFT_FORCE = 1f;

    #endregion

    #region Allied Constants



    #endregion

    #region Enemy Constants

    //enemy collision damage
    //public const float ENEMY_COLLISION_DAMAGE = 20f;

    //enemy rockets
    public const float ENEMY_SLOW_ROCKET_HEALTH = 30f;
    public const float ENEMY_FAST_ROCKET_HEALTH = 10f;
    public const float ENEMY_SLOW_ROCKET_COOLDOWN_TIMER = 4f;
    public const float ENEMY_FAST_ROCKET_COOLDOWN_TIMER = 2f;
    public const float ENEMY_SLOW_ROCKET_DAMAGE = 30f;
    public const float ENEMY_FAST_ROCKET_DAMAGE = 10f;
    public const float ENEMY_SLOW_ROCKET_LIFETIME = 5f;
    public const float ENEMY_FAST_ROCKET_LIEFTIME = 5f;
    public const float ENEMY_SLOW_ROCKET_SPEED = 3f;
    public const float ENEMY_FAST_ROCKET_SPEED = 15f;
    public const int ENEMY_SLOW_ROCKET_SCORE = 5;
    public const int ENEMY_FAST_ROCKET_SCORE = 10;

    //zepplin
    public const float ENEMY_ZEPPLIN_HEALTH = 100f;
    public const int ENEMY_ZEPPLIN_SCORE = 20;
    public const float ENEMY_ZEPPLIN_COLLISION_DAMAGE = 20f;

    //mothership
    public const float ENEMY_MOTHERSHIP_HEALTH = 3500f;
    public const float ENEMY_MOTHERSHIP_HORIZONTAL_ACCERATION = 5f;
    public const float ENEMY_MOTHERSHIP_MAX_HORIZONTAL_SPEED = CAMERA_SPEED;
    public const float ENEMY_MOTHERSHIP_SPAWN_ZEPPLIN_TIMER = 5f;
    public const float ENEMY_MOTHERSHIP_SLOW_MISSILE_COOLDOWN_TIMER = 3f;
    public const float ENEMY_MOTHERSHIP_FAST_MISSILE_COOLDOWN_TIMER = 2f;
    public const float ENEMY_MOTHERSHIP_HEAVY_SHELL_COOLDOWN_TIMER = 2f;

    //bomber
    public const float ENEMY_BOMBER_HEALTH = 70f;
    public const int ENEMY_BOMBER_SCORE = 30;
    public const float ENEMY_BOMBER_COLLISION_DAMAGE = 20f;
    public const float ENEMY_BOMBER_MAX_HORIZONTAL_SPEED = 7f;
    public const float ENEMY_BOMBER_MAX_VERTICAL_SPEED = 10f;
    public const float ENEMY_BOMBER_MAX_HORIZONTAL_ACCELERATION = 10f;
    public const float ENEMY_BOMBER_MAX_VERTICAL_ACCELERATION = 10f;
    public const float ENEMY_BOMBER_PLAYER_DISTANCE_THRESHOLD = 15f;
    public const float ENEMY_BOMBER_VERTICAL_SPACING = 3f;

    //soldier
    public const float ENEMY_SOLDIER_HEALTH = 1f;
    public const int ENEMY_SOLDIER_SCORE = 1;
    public const float ENEMY_SOLDIER_COLLISION_DAMAGE = 5f;

    //tank
    public const float ENEMY_TANK_HEALTH = 150f;
    public const int ENEMY_TANK_SCORE = 30;

    //jeep
    public const float ENEMY_JEEP_HEALTH = 30f;
    public const int ENEMY_JEEP_SCORE = 10;
    public const float ENEMY_JEEP_COLLSION_DAMAGE = 20f;

    #endregion

    #region Projectile Constants

    //heavy projectile shell
    public const float HEAVY_PROJECTILE_SHELL_DAMAGE = 30f;
    public const float HEAVY_PROJECTILE_SHELL_COOLDOWN_TIMER = 1f;
    public const float HEAVY_PROJECTILE_SHELL_LIFETIME = 1f;
    public const float HEAVY_PROJECTILE_SHELL_SPEED = 10f;

    #endregion

    #region Item Constants

    //basic attack upgrade
    public const int PLAYER_ADVANCED_BULLET_UPGRADE_COST = 50;
    public const float PLAYER_ADVANCED_BULLET_DAMAGE = 15f;
    public const float PLAYER_ADVANCED_BULLET_LIFETIME = 0.33f;
    public const float PLAYER_ADVANCED_BULLET_SPEED = 75f;
    public const float PLAYER_ADVANCED_BULLET_COOLDOWN = 0.05f;

    //cluster bomb
    public const int CLUSTER_BOMB_COST = 100;
    public const int CLUSTER_BOMB_COUNT = 8;
    public const float CLUSTER_BOMB_LIFETIME = 2f;
    public const float CLUSTER_BOMB_DAMAGE = 75f;
    public const float CLUSTER_BOMB_COOLDOWN = 7.5f;
    public const float CLUSTER_BOMB_SPEED = 10f;

    //seeker missiles
    public const int SEEKER_MISSILES_COST = 100;
    public const int SEEKER_MISSILES_COUNT = 5;
    public const float SEEKER_MISSILES_DAMAGE = 75f;
    public const float SEEKER_MISSILES_SPEED = 10f;
    public const float SEEKER_MISSILES_LIFETIME = 1.5f;
    public const float SEEKER_MISSILES_COOLDDOWN = 7.5f;

    //energy beam
    public const int ENERGY_BEAM_COST = 200;
    public const int ENERGY_BEAM_DAMAGE = 100;
    public const float ENERGY_BEAM_LIFETIME = 3f;
    public const float ENERGY_BEAM_COOLDOWN = 10f;

    //energy shield
    public const int ENERGY_SHIELD_COST = 200;
    public const float ENERGY_SHIELD_LIFETIME = 5f;
    public const float ENERGY_SHIELD_COOLDOWN = 7.5f;

    //decoy


    //repair pack
    public const int REPAIR_PACK_COST = 50;
    public const float REPAIR_PACK_REPAIR_AMOUNT = 75f;
    public const int REPAIR_PACK_CAP = 3;

    //aircraft hull
    public const int AIRCRAFT_HULL_BONUS_COST = 50;
    public const float AIRCRAFT_HULL_BONUS = 50f;
    public const int AIRCRAFT_HULL_BONUS_CAP = 4;

    //flight engineer
    public const int FLIGHT_ENGINEER_COST = 100;
    public const float FLIGHT_ENGINEER_REPAIR_RATE = 5f;
    public const float FLIGHT_ENGINEER_WRENCH_ROTATION_RATE = 240f;

    //wingman
    public const int WINGMAN_COST = 100;

    #endregion
}
