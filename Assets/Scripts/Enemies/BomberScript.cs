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

    //fast rocket cool down timer
    float fastRocketTimer = Constants.ENEMY_FAST_ROCKET_COOLDOWN_TIMER;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //resets camera for canvas component
        transform.GetChild(0).GetComponent<Canvas>().worldCamera = Camera.main;

        //set health bar sprites
        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Universals/HealthBarDamagedSprite");
    }

    // Update is called once per frame
    void Update ()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
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
                Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
                GameManager.Instance.Score += Constants.ENEMY_BOMBER_SCORE;
                Destroy(gameObject);
            }
        }
    }


    private void FixedUpdate()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kill self and damage player if player crashed into bomber
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //else take damage from player bullet
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerBullet]))
        {
            health -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
            flashHealthBar = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            //fire fast rocket if ready
            if (fastRocketTimer >= Constants.ENEMY_FAST_ROCKET_COOLDOWN_TIMER)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/EnemyFastRocket"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                fastRocketTimer = 0f;
            }
            else
            {
                fastRocketTimer += Time.deltaTime;
            }
        }
    }
}
