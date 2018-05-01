using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelButtonScript : ButtonScript
{
    //prelevel menu to change to if applicable. leave on None if n/a
    [SerializeField]
    PreLevelMenus preLevelMenuToGoTo;

    //bools for what the shop buttons do
    [SerializeField]
    bool apBulletButton;
    [SerializeField]
    bool aircraftHullButton;
    [SerializeField]
    bool shieldButton;
    [SerializeField]
    bool clusterBombButton;
    [SerializeField]
    bool seekerMissilesButton;
    [SerializeField]
    bool flightEngineerButton;
    [SerializeField]
    bool repairPackButton;
    
    //button reference
    Button button;

    //text reference
    Text descriptionText;

    // Use this for initialization
    void Awake()
    {
        //set references
        button = GetComponent<Button>();
        descriptionText = transform.GetChild(0).GetComponent<Text>();

        //setup what the button is supposed to do
        if (apBulletButton)
        {
            //check if already purchased
            if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.APBullets) > 0)
            {
                button.interactable = false;
                descriptionText.text = "Advanced Bullets already purchased!";
            }
            else
            {
                descriptionText.text = "Advanced Bullets: More range, more damage!\nCost: " + Constants.PLAYER_ADVANCED_BULLET_UPGRADE_COST.ToString();
            }
        }
        else if (aircraftHullButton)
        {
            //disable button if at cap
            if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.AircraftHull) >= Constants.AIRCRAFT_HULL_BONUS_CAP)
            {
                button.interactable = false;
                descriptionText.text = "At Maximum Bonus Health Cap!";
            }
            else
            {
                descriptionText.text = "Hull Upgrade: Increases max Hull Points!\nCost: " + Constants.AIRCRAFT_HULL_BONUS_COST;
            }
        }
        else if (shieldButton)
        {
            descriptionText.text = "Energy Shield: Protects from enemy fire!\nCost: " + Constants.ENERGY_SHIELD_COST;
        }
        else if (clusterBombButton)
        {
            descriptionText.text = "Cluster Bombs: Shoots bombs in all directions!\nCost: " + Constants.CLUSTER_BOMB_COST;
        }
        else if (seekerMissilesButton)
        {
            descriptionText.text = "Seeker Missiles: Locks missiles onto enemies!\nCost: " + Constants.SEEKER_MISSILES_COST;
        }
        else if (flightEngineerButton)
        {
            //check if already purchased
            if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.FlightEngineer) > 0)
            {
                button.interactable = false;
                descriptionText.text = "Flight Engineer already purchased!";
            }
            else
            {
                descriptionText.text = "Flight Engineer: Repairs your plane automatically!\nCost: " + Constants.FLIGHT_ENGINEER_COST;
            }
        }
        else if (repairPackButton)
        {
            //disable button if at cap
            if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.RepairPack) >= Constants.REPAIR_PACK_CAP)
            {
                button.interactable = false;
                descriptionText.text = "At Maximum Repair Pack Cap!";
            }
            else
            {
                descriptionText.text = "Instantly repairs your plane at low health!\nCost: " + Constants.REPAIR_PACK_COST;
            }
        }
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();


	}


    public void OnPreLevelMenuChange()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        UIManager.Instance.PreLevelMenuControl.ChangeMenu(preLevelMenuToGoTo);
    }


    public void PurchaseItem()
    {
        if (apBulletButton)
        {
            if (GameManager.Instance.Score >= Constants.PLAYER_ADVANCED_BULLET_UPGRADE_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.APBullets, 1);
                GameManager.Instance.Score -= Constants.PLAYER_ADVANCED_BULLET_UPGRADE_COST;
                button.interactable = false;
                descriptionText.text = "Advanced Bullets already purchased!";
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();
            }
        }
        else if (aircraftHullButton)
        {
            if (GameManager.Instance.Score >= Constants.AIRCRAFT_HULL_BONUS_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.AircraftHull, 1);
                GameManager.Instance.Score -= Constants.AIRCRAFT_HULL_BONUS_COST;
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();

                //disable button if at cap
                if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.AircraftHull) >= Constants.AIRCRAFT_HULL_BONUS_CAP)
                {
                    button.interactable = false;
                    descriptionText.text = "At Maximum Bonus Health Cap!";
                }
            }
        }
        else if (shieldButton)
        {
            if (GameManager.Instance.Score >= Constants.ENERGY_SHIELD_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.EnergyShield, 1);
                GameManager.Instance.Score -= Constants.ENERGY_SHIELD_COST;
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();
            }
        }
        else if (clusterBombButton)
        {
            if (GameManager.Instance.Score >= Constants.CLUSTER_BOMB_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.ClusterBomb, 1);
                GameManager.Instance.Score -= Constants.CLUSTER_BOMB_COST;
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();
            }
        }
        else if (seekerMissilesButton)
        {
            if (GameManager.Instance.Score >= Constants.SEEKER_MISSILES_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.SeekerMissiles, 1);
                GameManager.Instance.Score -= Constants.SEEKER_MISSILES_COST;
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();
            }
        }
        else if (flightEngineerButton)
        {
            if (GameManager.Instance.Score >= Constants.FLIGHT_ENGINEER_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.FlightEngineer, 1);
                GameManager.Instance.Score -= Constants.FLIGHT_ENGINEER_COST;
                button.interactable = false;
                descriptionText.text = "Flight Engineer already purchased!";
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();
            }
        }
        else if (repairPackButton)
        {
            if (GameManager.Instance.Score >= Constants.REPAIR_PACK_COST)
            {
                GameManager.Instance.PlayerInventory.AddItem(ItemType.RepairPack, 1);
                GameManager.Instance.Score -= Constants.REPAIR_PACK_COST;
                UIManager.Instance.PreLevelMoneyText.SetMoneyText();
            }

            //disable button if at cap
            if (GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.RepairPack) >= Constants.REPAIR_PACK_CAP)
            {
                button.interactable = false;
                descriptionText.text = "At Maximum Repair Pack Cap!";
            }
        }

        AudioManager.Instance.PlayUISoundEffect(buttonSound);
    }


    public void TakeoffFinallizer()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        UIManager.Instance.PreLevelMenuControl.TakeoffFinalizer();
    }
}
