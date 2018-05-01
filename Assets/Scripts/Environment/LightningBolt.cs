using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : PauseableObject
{
    float lifeTimer = 0f;
    float flickerTimer = 0f;
    bool flickerOn = true;

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();
	}

    // Update is called once per frame
    void Update()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //lightning life timer
            if (lifeTimer <= Constants.LIGHTNING_LIFETIME)
            {
                lifeTimer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }

            //flicker timer
            if (flickerTimer <= Constants.LIGHTNING_FLICKER_TIMER && !flickerOn)
            {
                //flicker off
                GetComponent<SpriteRenderer>().color = Color.white;

                flickerTimer += Time.deltaTime;

                if (flickerTimer >= Constants.LIGHTNING_FLICKER_TIMER)
                {
                    flickerOn = true;
                }
            }
            else
            {
                //flicker on
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

                flickerTimer -= Time.deltaTime;

                if (flickerTimer <= 0)
                {
                    flickerOn = false;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            if (!GameManager.Instance.Shield)
            {
                collision.GetComponent<PlayerScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
            }
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber]))
        {
            collision.GetComponent<BomberScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Jeep]))
        {
            collision.GetComponent<JeepScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Soldier]))
        {
            collision.GetComponent<SoldierScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Tank]))
        {
            collision.GetComponent<TankScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Zepplin]))
        {
            collision.GetComponent<ZepplinScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bird]))
        {
            collision.GetComponent<BirdScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemyFastRocket]))
        {
            collision.GetComponent<EnemyFastRocketScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemySlowRocket]))
        {
            collision.GetComponent<EnemySlowRocketScript>().ModifyHealth(Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE);
        }
    }
}
