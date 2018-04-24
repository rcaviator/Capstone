using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldScript : PauseableObject
{
    //shield duration timer, counts up to max
    float lifeTimer = 0f;

    //shield remaining indication variables
    bool flashShield = false;
    bool changeColor = true;
    float maxFlashShieldTimer = 0.5f;
    float flashShieldTimer = 0f;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //go to player
        transform.position = GameManager.Instance.Player.transform.position;

        //play sound

    }

    // Update is called once per frame
    void Update ()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //follow the player
            transform.position = GameManager.Instance.Player.transform.position;

            //shield lifetime timer
            if (lifeTimer <= Constants.ENERGY_SHIELD_LIFETIME)
            {
                lifeTimer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }

            //test if lifetime timer is in range of flashing time
            if (lifeTimer >= Constants.ENERGY_SHIELD_LIFETIME - 2f)
            {
                flashShield = true;
            }

            //alternate between colors when little time remains
            if (flashShield)
            {
                if (changeColor)
                {
                    flashShieldTimer += Time.deltaTime;
                    GetComponent<SpriteRenderer>().color = Color.red;

                    if (flashShieldTimer >= maxFlashShieldTimer)
                    {
                        flashShieldTimer = 0f;
                        changeColor = false;
                    }
                }
                else
                {
                    flashShieldTimer += Time.deltaTime;
                    GetComponent<SpriteRenderer>().color = Color.white;

                    if (flashShieldTimer >= maxFlashShieldTimer)
                    {
                        flashShieldTimer = 0f;
                        changeColor = true;
                    }
                }
            }
        }
    }
}
