﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : PauseableObject
{
    /// <summary>
    /// The state of the player
    /// </summary>
    public enum PlayerState
    {
        None, AutoPilotTakeOff, AutoPilotLanding, Manual, Mayday,
    };

    #region Fields

    //healthbar
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //audio source
    AudioSource aSource;

    //player direction booleans, used for better input between update and fixed update
    bool up;
    bool down;
    bool left;
    bool right;

    //player speed fields
    float currentHorizontalSpeed = 0f;
    float currentVerticalSpeed = 0f;

    //auto firing controll
    float maxAutoFireTimer = Constants.PLAYER_BASIC_BULLET_COOLDOWN_TIMER;
    float autoFireTimer = 0f;
    bool canShoot = false;

    //take off and landing timing controls
    float takeOffGroundRollTime = Constants.PLAYER_TAKEOFF_GROUND_ROLL_TIMER;
    float takeOffGroundRollTimer = 0f;
    float takeOffClimbRate = Constants.PLAYER_TAKEOFF_RATE;
    float landingHeight = 0f;
    float landingDecentRate = 0f;
    //float landingGroundRollTime = 2f;
    //float landingGroundRollTimer = 0f;
    float timeUntilLanding = 3f;

    //pitching variables
    float pitchAngle = 0f;
    GameObject childCanvas;
    Quaternion childQuaternion;

    //camera clamp fields
    float dist;
    float width;
    float height;
    float leftLimitation;
    float rightLimitation;
    float upLimitation;
    float downLimitation;

    #endregion


    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        //set reference in game manager
        GameManager.Instance.Player = this;

        //set state
        State = PlayerState.AutoPilotTakeOff;

        //get child reference
        childCanvas = transform.GetChild(0).gameObject;

        //resets camera for canvas component
        childCanvas.GetComponent<Canvas>().worldCamera = Camera.main;

        //set child quaterion
        childQuaternion = transform.rotation;

        //set health
        Health = Constants.PLAYER_STARTING_HEALTH;

        //set healthbars
        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Universals/HealthBarDamagedSprite");

        //set audio source
        aSource = GetComponent<AudioSource>();
        aSource.clip = AudioManager.Instance.GetAudioClip(GameSoundEffect.PlayerAirplaneEngine);
        aSource.Play();
        aSource.loop = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //only do stuff if the game is not paused
        if (!GameManager.Instance.Paused)
        {
            //set clamping fields
            width = GetComponent<Collider2D>().bounds.size.x;
            height = GetComponent<Collider2D>().bounds.size.y;
            dist = transform.position.z - GameManager.Instance.PlayerCamera.transform.position.z;
            leftLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 0f, dist)).x + (width / 2);
            rightLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1f, 0f, dist)).x - (width / 2);
            upLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 1f, dist)).y - (height / 2);
            downLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 0f, dist)).y + (height / 2);

            
            //switch over the player states
            switch (State)
            {
                case PlayerState.None:
                    break;

                case PlayerState.AutoPilotTakeOff:
                    //handled in fixed update
                    break;
                case PlayerState.AutoPilotLanding:
                    //handled in fixed update
                    break;
                case PlayerState.Manual:

                    #region Movement Control

                    //move player up down left right
                    //right
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0f)
                    {
                        right = true;
                        left = false;
                    }
                    //left
                    else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0f)
                    {
                        left = true;
                        right = false;
                    }

                    //up
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) > 0f)
                    {
                        up = true;
                        down = false;
                    }
                    //down
                    else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) < 0f)
                    {
                        down = true;
                        up = false;
                    }

                    #endregion

                    #region Decceleration Control

                    //slow down if no controls, horizontal
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f)
                    {
                        left = false;
                        right = false;
                    }

                    //slow down if no controls, vertical
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) == 0f)
                    {
                        up = false;
                        down = false;
                    }

                    #endregion

                    #region Fire Control

                    if (autoFireTimer < maxAutoFireTimer)
                    {
                        autoFireTimer += Time.deltaTime;
                    }
                    else
                    {
                        canShoot = true;
                    }

                    //attack
                    //InputManager.Instance.GetButton(PlayerAction.FirePrimary)
                    if (InputPlayerShoot && canShoot)
                    {
                        BasicAttack();
                        canShoot = false;
                        autoFireTimer = 0f;
                    }

                    #endregion

                    break;

                case PlayerState.Mayday:

                    break;
                default:
                    break;
            }

            //update health bar
            healthBar.fillAmount = Health / Constants.PLAYER_STARTING_HEALTH;

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

            //health control
            if (Health <= 0f)
            {
                MySceneManager.Instance.ChangeScene(Scenes.Defeat);
            }

            //engine audio control
            //aSource.pitch = Mathf.Clamp(, 0f, 2f);
        }

        #region Player Pitch Control

        pitchAngle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
        pitchAngle = Mathf.Clamp(rBody.velocity.y, Constants.PLAYER_PITCH_DOWN_MAX, Constants.PLAYER_PITCH_UP_MAX);
        transform.rotation = Quaternion.AngleAxis(pitchAngle, Vector3.forward);
        childCanvas.transform.rotation = childQuaternion;

        #endregion

        #region Player Clamp Control

        //test for out of bounds and reseting the velocity
        if (transform.position.x < leftLimitation || transform.position.x > rightLimitation)
        {
            currentHorizontalSpeed = 0f;
        }
        if (transform.position.y < downLimitation || transform.position.y > upLimitation)
        {
            currentVerticalSpeed = 0f;
        }

        //clamp player to screen
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimitation, rightLimitation), Mathf.Clamp(transform.position.y, downLimitation, upLimitation));

        #endregion
    }


    private void FixedUpdate()
    {
        //only do stuff when the game is not paused
        if (!GameManager.Instance.Paused)
        {
            //follow the camera's relative velocity
            rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);

            switch (State)
            {
                case PlayerState.None:
                    break;

                case PlayerState.AutoPilotTakeOff:
                    //ground roll takeoff
                    if (takeOffGroundRollTimer < takeOffGroundRollTime)
                    {
                        takeOffGroundRollTimer += Time.fixedDeltaTime;
                    }
                    //climb
                    else
                    {
                        rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y + takeOffClimbRate);
                    }
                    break;

                case PlayerState.AutoPilotLanding:
                    //landing
                    if (transform.position.y > 4f)
                    {
                        rBody.velocity = new Vector2(GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, -landingDecentRate);
                    }
                    else
                    {
                        rBody.velocity = new Vector2(GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, 0f);
                        GameManager.Instance.PlayerCamera.Landed = true;
                    }
                    break;

                case PlayerState.Manual:
                    //right
                    if (right)
                    {
                        currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //left
                    else if (left)
                    {
                        currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed - Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }

                    //up
                    if (up)
                    {
                        currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed + Constants.PLAYER_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_VERTICAL_SPEED, Constants.PLAYER_MAX_VERTICAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //down
                    else if (down)
                    {
                        currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed - Constants.PLAYER_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_VERTICAL_SPEED, Constants.PLAYER_MAX_VERTICAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }

                    //slow down horizontal if no controls are pressed
                    if (!left && !right)
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
                                    currentHorizontalSpeed += Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime;
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
                                    currentHorizontalSpeed -= Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.fixedDeltaTime;
                                }
                            }
                        }
                    }

                    //slow down vertical if no controls are pressed
                    if (!up && !down)
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
                                    currentVerticalSpeed += Constants.PLAYER_VERTICAL_ACCELERATION * Time.fixedDeltaTime;
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
                                    currentVerticalSpeed -= Constants.PLAYER_VERTICAL_ACCELERATION * Time.fixedDeltaTime;
                                }
                            }
                        }
                    }

                    break;

                case PlayerState.Mayday:

                    break;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// The state of the player
    /// </summary>
    public PlayerState State
    { get; set; }

    /// <summary>
    /// The player health
    /// </summary>
    public float Health
    { get; set; }


    public bool InputPlayerShoot
    { get; set; }


    public void PrepareForLanding()
    {
        landingHeight = transform.position.y + Constants.LEVEL_EDITOR_GRID_OFFSET_Y - 1;
        landingHeight = Mathf.Abs(landingHeight);

        landingDecentRate = landingHeight / timeUntilLanding;
        //reset velocity for next update
        rBody.velocity = new Vector2(0, 0);
        State = PlayerState.AutoPilotLanding;
    }

    private void BasicAttack()
    {
        GameObject attack = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/PlayerBullet"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Vector2 vel = (Vector2)GameManager.Instance.Reticle.transform.position - (Vector2)transform.position;
        attack.GetComponent<PlayerBasicBulletScript>().InitializePlayerBasicProjectile(vel);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player will only take damage in manual state
        if (State == PlayerState.Manual)
        {
            if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Enemy]))
            {
                Health -= Constants.ENEMY_COLLISION_DAMAGE;
                flashHealthBar = true;
            }
            else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemyBullet]))
            {
                Health -= 10f;//change this in constants when enemy bullets are in
                flashHealthBar = true;
            }
            else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Environment]))
            {
                Health -= Constants.BIRD_DAMAGE;
                flashHealthBar = true;
            }

            if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Ground]))
            {
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast6);
                MySceneManager.Instance.ChangeScene(Scenes.Defeat);
            }
        }
    }
}
