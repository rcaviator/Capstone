﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZepplinScript : PauseableObject
{
    //main health
    float health = Constants.ENEMY_ZEPPLIN_HEALTH;

    //healthbar visuals and controll
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //slow rocket cool down timer
    float slowRocketTimer = Constants.ENEMY_SLOW_ROCKET_COOLDOWN_TIMER;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //resets camera for canvas component
        transform.GetChild(4).GetComponent<Canvas>().worldCamera = Camera.main;

        //set health bar sprites
        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Universals/HealthBarDamagedSprite");
    }

    // Update is called once per frame
    void Update()
    {
        //process if game is not paused
        if (!GameManager.Instance.Paused)
        {
            //update health bar
            healthBar.fillAmount = health / Constants.ENEMY_ZEPPLIN_HEALTH;

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
                Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
                GameManager.Instance.Score += Constants.ENEMY_ZEPPLIN_SCORE;
                Destroy(gameObject);
            }
        }
    }


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
            GameManager.Instance.Score += Constants.ENEMY_ZEPPLIN_SCORE;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            health -= Constants.SEEKER_MISSILES_DAMAGE;
            flashHealthBar = true;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.LightningBolt]))
        {
            health -= Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE;
            flashHealthBar = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            //fire slow rocket if ready
            if (slowRocketTimer >= Constants.ENEMY_SLOW_ROCKET_COOLDOWN_TIMER)
            {
                Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.EnemySlowRocket), new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                slowRocketTimer = 0f;
            }
            else
            {
                slowRocketTimer += Time.deltaTime;
            }
        }
    }
}
