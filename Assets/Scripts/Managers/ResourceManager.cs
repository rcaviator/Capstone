using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum Prefabs
{
    //default
    None,

    //effects
    BulletRicochetSparks1, BulletRicochetSparks2, LightnightBolt, ModerateExplosion,

    //enemies
    Bomber, Jeep, Mothership, Soldier, Tank, Zepplin,

    //environment
    Bird, DirtBlock, DirtBlockGrass, DirtBlockSloped, DirtBlockSlopedGrass,
    HangarClose, HangarFar, HangarMiddle,
    StoneBlock, StoneBlockConcreteTop, StoneBlockSloped, StoneBlockSlopedConcreteTop,
    Tower, WeatherHazard1, WeatherHazard2, WeatherHazard3,

    //player
    Player, TargetReticle,

    //projectiles and powerups
    ClusterBomb, EnemyFastRocket, EnemySlowRocket, EnergyShield,
    HeavyProjectileShell, PlayerAdvancedBullet, PlayerBullet,
    SeekerMissile, Wrench,

    //ui
    ModuleFileButton, SpawnableObjectButton, CreditsCanvas, ErrorMsgCanvas,
    MainMenuCanvas, NewGameCanvas, SettingsCanvas, PauseMenuCanvas,
    APUpgradeButton, CluserBombButton, FlightEngineerButton, HullUpgradeButton,
    RepairPackButton, SeekerMissileButton, ShieldButton,
    MenuNavigationCanvas, OverviewCanvas, PreLevelMenuTitleCanvas, ShopCanvas, WeatherAndMapCanvas,

    //utility
    BGParallax, LevelEndPoint, LevelStartPoint,
}

/// <summary>
/// ResourceManager handles loading in the resources and prefabs.
/// </summary>
class ResourceManager
{
    #region Fields

    //instance
    static ResourceManager instance;

    #endregion

    #region Constructor

    /// <summary>
    /// Private Constructor
    /// </summary>
    private ResourceManager()
    {
        //initialize the prefab dictionary
        PrefabDictionary = new Dictionary<Prefabs, GameObject>()
        {
            //leave none out
            //effects
            { Prefabs.BulletRicochetSparks1, Resources.Load<GameObject>("Prefabs/Effects/Bullet_Ricochet_Sparks_1") },
            { Prefabs.BulletRicochetSparks2, Resources.Load<GameObject>("Prefabs/Effects/Bullet_Ricochet_Sparks_2") },
            { Prefabs.LightnightBolt, Resources.Load<GameObject>("Prefabs/Effects/LightningBolt") },
            { Prefabs.ModerateExplosion, Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion") },
            //enemies
            { Prefabs.Bomber, Resources.Load<GameObject>("Prefabs/Enemies/Bomber") },
            { Prefabs.Jeep, Resources.Load<GameObject>("Prefabs/Enemies/Jeep") },
            { Prefabs.Mothership, Resources.Load<GameObject>("Prefabs/Enemies/Mothership") },
            { Prefabs.Soldier, Resources.Load<GameObject>("Prefabs/Enemies/Soldier") },
            { Prefabs.Tank, Resources.Load<GameObject>("Prefabs/Enemies/Tank") },
            { Prefabs.Zepplin, Resources.Load<GameObject>("Prefabs/Enemies/Zepplin") },
            //environment
            { Prefabs.Bird, Resources.Load<GameObject>("Prefabs/Environment/Bird") },
            { Prefabs.DirtBlock, Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block") },
            { Prefabs.DirtBlockGrass, Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block_Grass") },
            { Prefabs.DirtBlockSloped, Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block_Sloped") },
            { Prefabs.DirtBlockSlopedGrass, Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block_Sloped_Grass") },
            { Prefabs.HangarClose, Resources.Load<GameObject>("Prefabs/Environment/HangarClose") },
            { Prefabs.HangarFar, Resources.Load<GameObject>("Prefabs/Environment/HangarFar") },
            { Prefabs.HangarMiddle, Resources.Load<GameObject>("Prefabs/Environment/HangarMiddle") },
            { Prefabs.StoneBlock, Resources.Load<GameObject>("Prefabs/Environment/Stone_Block") },
            { Prefabs.StoneBlockConcreteTop, Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Concrete_Top") },
            { Prefabs.StoneBlockSloped, Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Sloped") },
            { Prefabs.StoneBlockSlopedConcreteTop, Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Sloped_Concrete_Top") },
            { Prefabs.Tower, Resources.Load<GameObject>("Prefabs/Environment/Tower") },
            { Prefabs.WeatherHazard1, Resources.Load<GameObject>("Prefabs/Environment/WeatherHazard1") },
            { Prefabs.WeatherHazard2, Resources.Load<GameObject>("Prefabs/Environment/WeatherHazard2") },
            { Prefabs.WeatherHazard3, Resources.Load<GameObject>("Prefabs/Environment/WeatherHazard3") },
            //player
            { Prefabs.Player, Resources.Load<GameObject>("Prefabs/Player/Player") },
            { Prefabs.TargetReticle, Resources.Load<GameObject>("Prefabs/Player/TargetReticle") },
            //projectiles and powerups
            { Prefabs.ClusterBomb, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/CluserBomb") },
            { Prefabs.EnemyFastRocket, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/EnemyFastRocket") },
            { Prefabs.EnemySlowRocket, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/EnemySlowRocket") },
            { Prefabs.EnergyShield, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/EnergyShield") },
            { Prefabs.HeavyProjectileShell, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/HeavyProjectileShell") },
            { Prefabs.PlayerAdvancedBullet, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/PlayerAdvancedBullet") },
            { Prefabs.PlayerBullet, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/PlayerBullet") },
            { Prefabs.SeekerMissile, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/SeekerMissile") },
            { Prefabs.Wrench, Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/Wrench") },
            //ui
            { Prefabs.ModuleFileButton, Resources.Load<GameObject>("Prefabs/UI/Level editor/ModuleFileButton") },
            { Prefabs.SpawnableObjectButton, Resources.Load<GameObject>("Prefabs/UI/Level editor/SpawnableObjectButton") },
            { Prefabs.CreditsCanvas, Resources.Load<GameObject>("Prefabs/UI/Main menu/CreditsCanvas") },
            { Prefabs.ErrorMsgCanvas, Resources.Load<GameObject>("Prefabs/UI/Main menu/ErrorMsgCanvas") },
            { Prefabs.MainMenuCanvas, Resources.Load<GameObject>("Prefabs/UI/Main menu/MainMenuCanvas") },
            { Prefabs.NewGameCanvas, Resources.Load<GameObject>("Prefabs/UI/Main menu/NewGameCanvas") },
            { Prefabs.SettingsCanvas, Resources.Load<GameObject>("Prefabs/UI/Main menu/SettingsCanvas") },
            { Prefabs.PauseMenuCanvas, Resources.Load<GameObject>("Prefabs/UI/Pause menu/PauseMenuCanvas") },
            { Prefabs.APUpgradeButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/APUpgradeButton") },
            { Prefabs.CluserBombButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/CluserBombButton") },
            { Prefabs.FlightEngineerButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/FlightEngineerButton") },
            { Prefabs.HullUpgradeButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/HullUpgradeButton") },
            { Prefabs.RepairPackButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/RepairPackButton") },
            { Prefabs.SeekerMissileButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/SeekerMissileButton") },
            { Prefabs.ShieldButton, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/ShieldButton") },
            { Prefabs.MenuNavigationCanvas, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/MenuNavigationCanvas") },
            { Prefabs.OverviewCanvas, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/OverviewCanvas") },
            { Prefabs.PreLevelMenuTitleCanvas, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/PreLevelMenuTitleCanvas") },
            { Prefabs.ShopCanvas, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/ShopCanvas") },
            { Prefabs.WeatherAndMapCanvas, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/WeatherAndMapCanvas") },
            //utilties
            { Prefabs.BGParallax, Resources.Load<GameObject>("Prefabs/Utility/BGParallax") },
            { Prefabs.LevelEndPoint, Resources.Load<GameObject>("Prefabs/Utility/LevelEndPoint") },
            { Prefabs.LevelStartPoint, Resources.Load<GameObject>("Prefabs/Utility/LevelStartPoint") },
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the game manager
    /// </summary>
    public static ResourceManager Instance
    {
        get { return instance ?? (instance = new ResourceManager()); }
    }

    /// <summary>
    /// The dictionary to hold all the loaded prefabs
    /// </summary>
    Dictionary<Prefabs, GameObject> PrefabDictionary
    { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the prefab from the prefab dictionary
    /// </summary>
    /// <param name="getPrefab">the name of the prefab to get</param>
    /// <returns>the requested prefab, else null</returns>
    public GameObject GetPrefab(Prefabs getPrefab)
    {
        if (PrefabDictionary.ContainsKey(getPrefab))
        {
            return PrefabDictionary[getPrefab];
        }
        else
        {
            return null;
        }
    }

    #endregion
}