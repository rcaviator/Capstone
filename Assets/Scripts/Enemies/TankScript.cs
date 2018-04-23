﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankScript : PauseableObject
{
    //main health
    float health = Constants.ENEMY_TANK_HEALTH;

    //healthbar visuals and controll
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //projectile timer
    float heavyTimer = Constants.HEAVY_PROJECTILE_SHELL_COOLDOWN_TIMER;

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
    void Update()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //update health bar
            healthBar.fillAmount = health / Constants.ENEMY_TANK_HEALTH;

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
                GameManager.Instance.Score += Constants.ENEMY_TANK_SCORE;
                Destroy(gameObject);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //end game if player crashes into tank
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            //Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
            //Destroy(gameObject);
            AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast6);
            MySceneManager.Instance.ChangeScene(Scenes.Defeat);
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
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            health -= Constants.SEEKER_MISSILES_DAMAGE;
            flashHealthBar = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            if (heavyTimer >= Constants.HEAVY_PROJECTILE_SHELL_COOLDOWN_TIMER)
            {
                GameObject attack = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/HeavyProjectileShell"), new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                Vector2 vel = new Vector2((GameManager.Instance.Player.transform.position.x + (GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity.x)) - transform.position.x, GameManager.Instance.Player.transform.position.y - transform.position.y);/*(Vector2)GameManager.Instance.Player.transform.position - (Vector2)transform.position;*/
                attack.GetComponent<HeavyProjectileShellScript>().InitializeProjectile(vel);

                heavyTimer = 0f;
            }
            else
            {
                heavyTimer += Time.deltaTime;
            }
        }
    }
}