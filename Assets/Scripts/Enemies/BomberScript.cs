using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomberScript : PauseableObject
{
    //main health
    float health = Constants.ENEMY_BOMBER_HEALTH;

    //healthbar visuals and controll
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //audio source
    AudioSource aSource;

    //sprite renderer
    SpriteRenderer sRenderer;

    //healthbar canvas
    Canvas healthBarCanvas;

    //box collider
    BoxCollider2D box;

    //fast rocket cool down timer
    float fastRocketTimer = Constants.ENEMY_FAST_ROCKET_COOLDOWN_TIMER;

    //bomber chase trigger
    bool chasePlayer = false;

    //movement variables
    float currentHorizontalSpeed;
    float currentVerticalSpeed;

    //pitching variables
    float pitchAngle = 0f;
    GameObject childCanvas;
    Quaternion childQuaternion;

    //boolean for mayday after defeated
    bool maydayState = false;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //set canvas, camera, and disable it
        healthBarCanvas = transform.GetChild(0).GetComponent<Canvas>();
        healthBarCanvas.worldCamera = Camera.main;
        healthBarCanvas.enabled = false;

        //set audio source
        aSource = GetComponent<AudioSource>();
        aSource.clip = AudioManager.Instance.GetAudioClip(GameSoundEffect.PlayerAirplaneEngine);
        aSource.pitch = 0.9f;
        aSource.loop = true;

        //set sprite renderer
        sRenderer = GetComponent<SpriteRenderer>();

        //disable box collider to prevent invisable crashing
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;

        //set health bar sprites
        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Universals/HealthBarDamagedSprite");

        //get child reference
        childCanvas = transform.GetChild(0).gameObject;

        //set child quaterion
        childQuaternion = transform.rotation;
    }


    private void Start()
    {
        //change oriantation
        if (transform.rotation.eulerAngles.y == 180)
        {
            transform.rotation = Quaternion.identity;
        }

        //set sprite renderer enabled/disabled based on scene
        if (MySceneManager.Instance.CurrentScene == Scenes.GameLevel)
        {
            sRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //check if the bomber is alive
            if (!maydayState)
            {
                //update health bar
                healthBar.fillAmount = health / Constants.ENEMY_BOMBER_HEALTH;

                //flash health bar if damaged
                if (flashHealthBar)
                {
                    healthBarFlash += Time.deltaTime;

                    if (healthBarFlash <= maxHealthBarFlash)
                    {
                        healthBar.sprite = damagedHealthBar;
                    }
                    else
                    {
                        healthBar.sprite = normalHealthBar;
                        healthBarFlash = 0f;
                        flashHealthBar = false;
                    }
                }

                //death from 0 health
                if (health <= 0f)
                {
                    GameManager.Instance.Score += Constants.ENEMY_BOMBER_SCORE;
                    maydayState = true;
                }
            }
            else
            {
                anim.Play("Bomber_Flip");
            }

            //pitch control
            pitchAngle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
            pitchAngle = Mathf.Clamp(rBody.velocity.y, Constants.PLAYER_PITCH_DOWN_MAX, Constants.PLAYER_PITCH_UP_MAX);
            transform.rotation = Quaternion.AngleAxis(pitchAngle, Vector3.forward);
            childCanvas.transform.rotation = childQuaternion;
        }
        else
        {
            //set the volume during pause
            aSource.volume = AudioManager.Instance.SoundEffectsVolume;
        }
    }


    private void FixedUpdate()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            if (chasePlayer)
            {
                //check if the bomber is alive
                if (!maydayState)
                {
                    //follow the camera's relative velocity
                    if (GameManager.Instance.Player.State == PlayerScript.PlayerState.Manual)
                    {
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else
                    {
                        rBody.velocity = new Vector2(currentHorizontalSpeed + Constants.CAMERA_SPEED, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }

                    //if the player is out of range horizontally, accelerate to catch up. player must be in manual mode to avoid enemy landings at finish line
                    if ((GameManager.Instance.Player.transform.position.x - transform.position.x > Constants.ENEMY_BOMBER_PLAYER_DISTANCE_THRESHOLD) && GameManager.Instance.Player.State == PlayerScript.PlayerState.Manual)
                    {
                        //horizontal
                        currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime, -Constants.ENEMY_BOMBER_MAX_HORIZONTAL_SPEED, Constants.ENEMY_BOMBER_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //else stabilize horizontal and follow the player
                    else
                    {
                        //set horizontal down to 0
                        //check if its not 0
                        if (currentHorizontalSpeed != 0f)
                        {
                            //check if its less than 0
                            if (currentHorizontalSpeed < 0f)
                            {
                                //check if we just need to set it to 0
                                if (currentHorizontalSpeed > -0.1f)
                                {
                                    currentHorizontalSpeed = 0f;
                                }
                                //add decceleration
                                else
                                {
                                    currentHorizontalSpeed += Constants.ENEMY_BOMBER_MAX_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime;
                                }
                            }
                            //check if its greater
                            else if (currentHorizontalSpeed > 0f)
                            {
                                //check if we just need to set it to 0
                                if (currentHorizontalSpeed < 0.1f)
                                {
                                    currentHorizontalSpeed = 0f;
                                }
                                //add decceleration
                                else
                                {
                                    currentHorizontalSpeed -= Constants.ENEMY_BOMBER_MAX_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime;
                                }
                            }
                        }
                    }

                    //get vertical spacing closed
                    //above and decend
                    if ((transform.position.y > GameManager.Instance.Player.transform.position.y && (transform.position.y - GameManager.Instance.Player.transform.position.y > Constants.ENEMY_BOMBER_VERTICAL_SPACING)) && GameManager.Instance.Player.State == PlayerScript.PlayerState.Manual)
                    {
                        currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed - Constants.ENEMY_BOMBER_MAX_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.ENEMY_BOMBER_MAX_VERTICAL_SPEED, Constants.ENEMY_BOMBER_MAX_VERTICAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //below and climb
                    else if ((transform.position.y < GameManager.Instance.Player.transform.position.y && (GameManager.Instance.Player.transform.position.y - transform.position.y > 3)) && GameManager.Instance.Player.State == PlayerScript.PlayerState.Manual)
                    {
                        currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed + Constants.ENEMY_BOMBER_MAX_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.ENEMY_BOMBER_MAX_VERTICAL_SPEED, Constants.ENEMY_BOMBER_MAX_VERTICAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //else stabilize vertical
                    else
                    {
                        //set vertical down to 0
                        //check if its not 0
                        if (currentVerticalSpeed != 0f)
                        {
                            //check if its less than 0
                            if (currentVerticalSpeed < 0f)
                            {
                                //check if we just need to set it to 0
                                if (currentVerticalSpeed > -0.1f)
                                {
                                    currentVerticalSpeed = 0f;
                                }
                                //add decceleration
                                else
                                {
                                    currentVerticalSpeed += Constants.ENEMY_BOMBER_MAX_VERTICAL_ACCELERATION * Time.fixedDeltaTime;
                                }
                            }
                            //check if its greater
                            else if (currentVerticalSpeed > 0f)
                            {
                                //check if we just need to set it to 0
                                if (currentVerticalSpeed < 0.1f)
                                {
                                    currentVerticalSpeed = 0f;
                                }
                                //add decceleration
                                else
                                {
                                    currentVerticalSpeed -= Constants.ENEMY_BOMBER_MAX_VERTICAL_ACCELERATION * Time.fixedDeltaTime;
                                }
                            }
                        }
                    }

                    //downdraft effect
                    if (Downdraft)
                    {
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x + Physics2D.gravity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y + (Physics2D.gravity.y / 2));
                    }
                }
                else
                {
                    //make the bomber fall out of the sky
                    currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed - Constants.ENEMY_BOMBER_MAX_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.ENEMY_BOMBER_MAX_VERTICAL_SPEED, Constants.ENEMY_BOMBER_MAX_VERTICAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }
    }

    /// <summary>
    /// Used for toggling the downdraft effect
    /// </summary>
    public bool Downdraft
    { get; set; }

    public void ModifyHealth(float amount)
    {
        health -= amount;
        //flashHealthBar = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kill self and damage player if player crashed into zeplin
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //else take damage from player bullet
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerBullet]))
        {
            health -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
            flashHealthBar = true;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerAdvancedBullet]))
        {
            health -= Constants.PLAYER_ADVANCED_BULLET_DAMAGE;
            flashHealthBar = true;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.ClusterBomb]))
        {
            health -= Constants.CLUSTER_BOMB_DAMAGE;
            flashHealthBar = true;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyBeam]))
        {
            health -= Constants.ENERGY_BEAM_DAMAGE;
            flashHealthBar = true;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyShield]))
        {
            Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
            GameManager.Instance.Score += Constants.ENEMY_BOMBER_SCORE;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            health -= Constants.SEEKER_MISSILES_DAMAGE;
            flashHealthBar = true;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bird]))
        {
            health -= Constants.BIRD_COLLISION_DAMAGE;
            flashHealthBar = true;
        }
        //destroy on ground
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Ground]))
        {
            Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.LightningBolt]))
        {
            health -= Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE;
            flashHealthBar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            chasePlayer = true;
            sRenderer.enabled = true;
            box.enabled = true;
            healthBarCanvas.enabled = true;
            aSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            if (chasePlayer && !maydayState)
            {
                if (GameManager.Instance.Player.transform.position.x - transform.position.x <= Constants.ENEMY_BOMBER_PLAYER_DISTANCE_THRESHOLD)
                {
                    //fire fast rocket if ready
                    if (fastRocketTimer >= Constants.ENEMY_FAST_ROCKET_COOLDOWN_TIMER)
                    {
                        Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.EnemyFastRocket), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        fastRocketTimer = 0f;
                    }
                    else
                    {
                        fastRocketTimer += Time.deltaTime;
                    }
                }
            }
        }
    }
}
