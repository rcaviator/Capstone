using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton3Script : MonoBehaviour
{
    //image reference
    Image itemImage;
    Image cooldownImage;
    Sprite referenceSprite;

    //text reference
    Text numberText;

    //timer
    float cooldownTimer = 0f;

    //flash button when item is ready again
    bool flashButton = false;
    float flashButtonTimer = 0f;
    float maxFlashButtonTime = 0.25f;

    // Use this for initialization
    void Awake()
    {
        //set references
        itemImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        cooldownImage = transform.GetChild(1).gameObject.GetComponent<Image>();
        numberText = transform.GetChild(2).gameObject.GetComponent<Text>();

        referenceSprite = itemImage.sprite;

        //-----testing hack--------
        //GameManager.Instance.PlayerInventory.AddItem(ItemType.SeekerMissiles, 10);

        //set info
        if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.SeekerMissiles) > 0)
        {
            cooldownImage.fillAmount = 0f;
            numberText.text = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.SeekerMissiles).ToString();
        }
        else
        {
            cooldownImage.fillAmount = 1f;
            numberText.text = "0";
        }
    }

    private void Update()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //player must be in manual
            if (GameManager.Instance.Player.State == PlayerScript.PlayerState.Manual)
            {
                //button press number
                if (InputManager.Instance.GetButtonDown(PlayerAction.Button3))
                {
                    //process item click
                    OnButtonClick();
                }

                //update timer and fill amount if inventory has contents
                if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.SeekerMissiles) > 0)
                {
                    //update timer
                    if (cooldownTimer > 0)
                    {
                        cooldownTimer -= Time.deltaTime;
                        cooldownImage.fillAmount = cooldownTimer / Constants.SEEKER_MISSILES_COOLDDOWN;
                        flashButton = true;
                    }
                    else
                    {
                        if (flashButton)
                        {
                            if (flashButtonTimer <= maxFlashButtonTime)
                            {
                                //change button
                                //change these to particles later
                                //itemImage.color = Color.yellow;
                                itemImage.sprite = null;

                                //update timer
                                flashButtonTimer += Time.deltaTime;
                            }
                            else
                            {
                                //change button
                                //change these to particles later
                                //itemImage.color = Color.white;
                                itemImage.sprite = referenceSprite;

                                //change boolean
                                flashButton = false;

                                //update timer
                                flashButtonTimer = 0f;
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnButtonClick()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //player must be in manual state
            if (GameManager.Instance.Player.State == PlayerScript.PlayerState.Manual)
            {
                //if the inventory is greater than 0
                if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.SeekerMissiles) > 0)
                {
                    //if the timer is ready
                    if (cooldownTimer <= 0)
                    {
                        //get targets
                        int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
                        Collider2D[] targets = Physics2D.OverlapCircleAll(GameManager.Instance.Player.transform.position, 5f, enemyLayer);

                        //if there is at least 1 enemy target
                        if (targets.Length > 0)
                        {
                            //spawn seeker missiles on targets
                            int targetIndex = 0;
                            for (int i = 0; i < Constants.SEEKER_MISSILES_COUNT; i++)
                            {
                                //test if enemy is visable
                                if (targets[targetIndex].GetComponent<SpriteRenderer>())
                                {
                                    if (targets[targetIndex].GetComponent<SpriteRenderer>().enabled)
                                    {
                                        //spawn object and lock on
                                        GameObject missile = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/SeekerMissile"), GameManager.Instance.Player.transform.position, Quaternion.identity);
                                        missile.GetComponent<SeekerMissileScript>().Initialize(targets[targetIndex].gameObject, targets[targetIndex].gameObject.transform.position - GameManager.Instance.Player.transform.position, Constants.SEEKER_MISSILES_LIFETIME);

                                        //increment targets list if possible
                                        try
                                        {
                                            if (targets[targetIndex + 1])
                                            {
                                                targetIndex++;
                                            }
                                        }
                                        catch (System.IndexOutOfRangeException e)
                                        {

                                            //Debug.Log("out of range");
                                        }
                                    }
                                    else
                                    {
                                        //skip object
                                        //increment targets list if possible
                                        try
                                        {
                                            if (targets[targetIndex + 1])
                                            {
                                                targetIndex++;
                                            }
                                        }
                                        catch (System.IndexOutOfRangeException e)
                                        {

                                            //Debug.Log("out of range");
                                        }
                                    }
                                }
                            }

                            //subtract inventory
                            GameManager.Instance.PlayerInventory.RemoveItem(ItemType.SeekerMissiles, 1);

                            //update text
                            numberText.text = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.SeekerMissiles).ToString();

                            //reset timer and fill amount
                            cooldownTimer = Constants.SEEKER_MISSILES_COOLDDOWN;
                            cooldownImage.fillAmount = cooldownTimer / Constants.SEEKER_MISSILES_COOLDDOWN;
                        }
                    }
                }
                else
                {
                    //play denied sound

                }
            }
        }
    }
}
