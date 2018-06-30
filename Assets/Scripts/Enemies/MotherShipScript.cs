using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipScript : PauseableObject
{
    //audio source
    AudioSource aSource;

    //boolean for if player is in range
    bool isInRange;

    //movement variables
    float currentHorizontalSpeed = 0f;
    float currentVerticalSpeed = 0f;

    //zepplin timer
    float maxZepplinTimer = Constants.ENEMY_MOTHERSHIP_SPAWN_ZEPPLIN_TIMER;
    float zepplinTimer = 0f;
    bool canSpawnZepplin = false;

    //fast missile timer
    float maxFastMissileTimer = Constants.ENEMY_MOTHERSHIP_FAST_MISSILE_COOLDOWN_TIMER;
    float fastMissileTimer = 0f;
    bool canFireFastMissile = false;

    //slow missile timer
    float maxSlowMissileTimer = Constants.ENEMY_MOTHERSHIP_SLOW_MISSILE_COOLDOWN_TIMER;
    float slowMissileTimer = 0f;
    bool canFireSlowMissile = false;

    //shell timer
    float maxShellTimer = Constants.ENEMY_MOTHERSHIP_HEAVY_SHELL_COOLDOWN_TIMER;
    float shellTimer = 0f;
    bool canFireShell = false;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.Boss = this;
        BossHealth = Constants.ENEMY_MOTHERSHIP_HEALTH;

        aSource = GetComponent<AudioSource>();
        aSource.clip = AudioManager.Instance.GetAudioClip(GameSoundEffect.EnemyBomberEngine);
        aSource.loop = true;
        aSource.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.Paused)
        {
            if (isInRange)
            {
                //health control
                if (BossHealth <= 0)
                {
                    MySceneManager.Instance.ChangeScene(Scenes.Victory);
                }

                //spawning control
                //zepplin
                if (zepplinTimer >= maxZepplinTimer)
                {
                    zepplinTimer = 0f;

                    //spawn object
                    Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Zepplin), transform.position, Quaternion.identity);
                }
                else
                {
                    zepplinTimer += Time.deltaTime;
                }

                //fast missile
                if (fastMissileTimer >= maxFastMissileTimer)
                {
                    fastMissileTimer = 0f;

                    //spawn object
                    Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.EnemyFastRocket), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                }
                else
                {
                    fastMissileTimer += Time.deltaTime;
                }

                //slow missile
                if (slowMissileTimer >= maxSlowMissileTimer)
                {
                    slowMissileTimer = 0f;

                    //spawn object
                    Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.EnemySlowRocket), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                }
                else
                {
                    slowMissileTimer += Time.deltaTime;
                }

                //heavy shell
                if (shellTimer >= maxShellTimer)
                {
                    shellTimer = 0f;

                    //spawn object
                    GameObject attack = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.HeavyProjectileShell), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    Vector2 vel = new Vector2((GameManager.Instance.Player.transform.position.x + (GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity.x)) - transform.position.x, GameManager.Instance.Player.transform.position.y - transform.position.y);
                    attack.GetComponent<HeavyProjectileShellScript>().InitializeProjectile(vel);
                }
                else
                {
                    shellTimer += Time.deltaTime;
                }
            }
        }
        else
        {
            //set the volume during pause
            aSource.volume = AudioManager.Instance.SoundEffectsVolume;
        }
	}


    private void FixedUpdate()
    {
        if (!GameManager.Instance.Paused)
        {
            if (isInRange)
            {
                //follow the camera's relative velocity
                //rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);

                //currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed + Constants.ENEMY_MOTHERSHIP_HORIZONTAL_ACCERATION * Time.fixedDeltaTime, -Constants.ENEMY_MOTHERSHIP_MAX_HORIZONTAL_SPEED, Constants.ENEMY_MOTHERSHIP_MAX_HORIZONTAL_SPEED);
                rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    /// <summary>
    /// The boss health
    /// </summary>
    public float BossHealth
    { get; set; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            //Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerBullet]))
        {
            BossHealth -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerAdvancedBullet]))
        {
            BossHealth -= Constants.PLAYER_ADVANCED_BULLET_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.ClusterBomb]))
        {
            BossHealth -= Constants.CLUSTER_BOMB_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyBeam]))
        {
            BossHealth -= Constants.ENERGY_BEAM_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyShield]))
        {
            //Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            BossHealth -= Constants.SEEKER_MISSILES_DAMAGE;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //boss hit target before player could destroy it, lose game
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.LevelEnd]))
        {
            GameManager.Instance.DeathObjectName = "The empire's superweapon reaching the capital!";
            GameManager.Instance.DeathObjectSprite = GetComponent<SpriteRenderer>().sprite;
            MySceneManager.Instance.ChangeScene(Scenes.Defeat);
        }

        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            isInRange = true;
        }
    }
}
