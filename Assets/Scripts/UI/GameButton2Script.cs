using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton2Script : MonoBehaviour
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
        //GameManager.Instance.PlayerInventory.AddItem(ItemType.ClusterBomb, 10);

        //set info
        if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.ClusterBomb) > 0)
        {
            cooldownImage.fillAmount = 0f;
            numberText.text = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.ClusterBomb).ToString();
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
                if (InputManager.Instance.GetButtonDown(PlayerAction.Button2))
                {
                    //process item click
                    OnButtonClick();
                }

                //update timer and fill amount if inventory has contents
                if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.ClusterBomb) > 0)
                {
                    //update timer
                    if (cooldownTimer > 0)
                    {
                        cooldownTimer -= Time.deltaTime;
                        cooldownImage.fillAmount = cooldownTimer / Constants.CLUSTER_BOMB_COOLDOWN;
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
                if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.ClusterBomb) > 0)
                {
                    //if the timer is ready
                    if (cooldownTimer <= 0)
                    {
                        //spawn object
                        float angle = 0f;
                        for (int i = 0; i < Constants.CLUSTER_BOMB_COUNT; i++)
                        {
                            GameObject bomb = Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles and Powerups/ClusterBomb"), GameManager.Instance.Player.transform.position, Quaternion.identity);
                            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
                            bomb.GetComponent<ClusterBombScript>().InitializeProjectile(dir);
                            angle += 360 / Constants.CLUSTER_BOMB_COUNT;
                        }

                        //subtract inventory
                        GameManager.Instance.PlayerInventory.RemoveItem(ItemType.ClusterBomb, 1);

                        //update text
                        numberText.text = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.ClusterBomb).ToString();

                        //reset timer and fill amount
                        cooldownTimer = Constants.CLUSTER_BOMB_COOLDOWN;
                        cooldownImage.fillAmount = cooldownTimer / Constants.CLUSTER_BOMB_COOLDOWN;
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
