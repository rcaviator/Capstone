using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JeepScript : PauseableObject
{
    //main health
    float health = Constants.ENEMY_JEEP_HEALTH;

    //healthbar visuals and controll
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //check scene and stop gravity
        if (MySceneManager.Instance.CurrentScene == Scenes.LevelEditor)
        {
            rBody.gravityScale = 0f;
        }

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
            healthBar.fillAmount = health / Constants.ENEMY_JEEP_HEALTH;

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
                GameManager.Instance.Score += Constants.ENEMY_JEEP_SCORE;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kill self and damage player if player crashed into jeep
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
}
