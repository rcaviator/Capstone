using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBlimpEnemyScript : PauseableObject
{
    float health;

    //healthbar
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    float slowRocketTimer = Constants.ENEMY_SLOW_ROCKET_COOLDOWN_TIMER;


    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        health = Constants.ENEMY_TEMP_BLIMP_HEALTH;

        //resets camera for canvas component
        transform.GetChild(0).GetComponent<Canvas>().worldCamera = Camera.main;

        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Universals/HealthBarDamagedSprite");
    }
	
	// Update is called once per frame
	void Update ()
    {
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

        if (health <= 0f)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
            GameManager.Instance.Score += Constants.ENEMY_TEMP_BLIMP_SCORE;
            Destroy(gameObject);
        }
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
            //GameManager.Instance.Score += Constants.ENEMY_TEMP_BLIMP_SCORE;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
            flashHealthBar = true;
            //Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (slowRocketTimer >= Constants.ENEMY_SLOW_ROCKET_COOLDOWN_TIMER)
            {
                /*GameObject rocket =*/ Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/EnemySlowRocket"), new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                //rocket.GetComponent<EnemySlowRocketScript>().
                slowRocketTimer = 0f;
            }
            else
            {
                slowRocketTimer += Time.deltaTime;
            }
        }
    }
}
