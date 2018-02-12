using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : PauseableObject
{
    public enum PlayerState
    {
        None, AutoPilotTakeOff, AutoPilotLanding, Manual, Mayday,
    };

    #region Fields

    //player fields
    float health = Constants.PLAYER_STARTING_HEALTH;
    float currentHorizontalSpeed = 0f;
    float currentVerticalSpeed = 0f;

    //healthbar
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //auto firing controll
    float maxAutoFireTimer = Constants.PLAYER_BASIC_BULLET_COOLDOWN_TIMER;
    float autoFireTimer = 0f;
    bool canShoot = false;

    //camera clamp fields
    float dist;
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

        //resets camera for canvas component
        transform.GetChild(0).GetComponent<Canvas>().worldCamera = Camera.main;

        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Universals/HealthBarDamagedSprite");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //only do stuff if the game is not paused
        if (!GameManager.Instance.Paused)
        {
            //set clamping fields
            dist = transform.position.z - GameManager.Instance.PlayerCamera.transform.position.z;
            leftLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 0f, dist)).x;
            rightLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1f, 0f, dist)).x;
            upLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 1f, dist)).y;
            downLimitation = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 0f, dist)).y;

            //follow the camera's relative position
            rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);

            //switch over the player states
            switch (State)
            {
                case PlayerState.None:
                    break;
                case PlayerState.AutoPilotTakeOff:
                    break;
                case PlayerState.AutoPilotLanding:
                    break;
                case PlayerState.Manual:

                    #region Movement Control

                    //move player up down left right
                    //right
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0f)
                    {
                        currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //left
                    else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0f)
                    {
                        currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed - Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }

                    //up
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) > 0f)
                    {
                        currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed + Constants.PLAYER_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_VERTICAL_SPEED, Constants.PLAYER_MAX_VERTICAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    //down
                    else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) < 0f)
                    {
                        currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed - Constants.PLAYER_VERTICAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_VERTICAL_SPEED, Constants.PLAYER_MAX_VERTICAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.x, currentVerticalSpeed + GameManager.Instance.PlayerCamera.GetComponent<Rigidbody2D>().velocity.y);
                    }

                    #endregion

                    #region Decceleration Control

                    //slow down if no controls, horizontal
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f)
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
                                    currentHorizontalSpeed += Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime;
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
                                    currentHorizontalSpeed -= Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime;
                                }
                            }
                        }
                    }

                    //slow down if no controls, vertical
                    if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) == 0f)
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
                                    currentVerticalSpeed += Constants.PLAYER_VERTICAL_ACCELERATION * Time.deltaTime;
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
                                    currentVerticalSpeed -= Constants.PLAYER_VERTICAL_ACCELERATION * Time.deltaTime;
                                }
                            }
                        }
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
            healthBar.fillAmount = health / Constants.PLAYER_STARTING_HEALTH;

            //flash health bar if damaged
            if (flashHealthBar)
            {
                healthBarFlash += Time.deltaTime;

                if (healthBarFlash <= maxHealthBarFlash)
                {
                    //healthBar.GetComponent<Image>().color = Color.red;
                    healthBar.sprite = damagedHealthBar;
                }
                else
                {
                    //healthBar.GetComponent<Image>().color = Color.white;
                    healthBar.sprite = normalHealthBar;
                    healthBarFlash = 0f;
                    flashHealthBar = false;
                }
            }

            //health control
            if (health <= 0f)
            {
                MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
            }

            
        }

        #region Player Clamp Control

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimitation, rightLimitation), Mathf.Clamp(transform.position.y, downLimitation, upLimitation));

        #endregion
    }

    /// <summary>
    /// The state of the player
    /// </summary>
    public PlayerState State
    { get; set; }


    public bool InputPlayerShoot
    { get; set; }


    private void BasicAttack()
    {
        GameObject attack = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/PlayerBullet"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Vector2 vel = (Vector2)GameManager.Instance.Reticle.transform.position - (Vector2)transform.position;
        attack.GetComponent<PlayerBasicBulletScript>().InitializePlayerBasicProjectile(vel);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 20f;
            flashHealthBar = true;
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            health -= 10f;
            flashHealthBar = true;
        }
    }
}
