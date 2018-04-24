using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarCanvas : MonoBehaviour
{
    //healthbar
    [SerializeField]
    Image healthBar;
    [SerializeField]
    Image backgroundHealthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //check for boss
    bool bossExists;

    //timer for enabling
    float enableTimer = 0f;
    float maxEnableTimer = 6f;

    // Use this for initialization
    void Start()
    {
        if (GameManager.Instance.Boss != null)
        {
            bossExists = true;
            healthBar.enabled = false;
            backgroundHealthBar.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bossExists)
        {
            if (enableTimer >= maxEnableTimer)
            {
                healthBar.enabled = true;
                backgroundHealthBar.enabled = true;

                //update health bar
                healthBar.fillAmount = GameManager.Instance.Boss.BossHealth / Constants.ENEMY_MOTHERSHIP_HEALTH;

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
            }
            else
            {
                enableTimer += Time.deltaTime;
            }
        }
	}
}
