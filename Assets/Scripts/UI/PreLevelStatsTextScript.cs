using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelStatsTextScript : MonoBehaviour
{
    //text reference
    Text statsText;

	// Use this for initialization
	void Awake()
    {
        //set references
        UIManager.Instance.PreLevelStats = this;
        statsText = GetComponent<Text>();
	}

    private void OnEnable()
    {
        //update GM with purchases
        GameManager.Instance.PlayerHealth = Constants.PLAYER_STARTING_HEALTH;
        for (int i = 0; i < GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.AircraftHull); i++)
        {
            GameManager.Instance.PlayerHealth += Constants.AIRCRAFT_HULL_BONUS;
        }

        //set text bools
        bool apBullets = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.APBullets) > 0;
        bool flightEngy = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.FlightEngineer) > 0;
        bool wingman = GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.WingMan) > 0;

        //health
        statsText.text = "Current Max Health: " + GameManager.Instance.PlayerHealth.ToString() + "\n";

        //advanced bullet upgrade
        if (apBullets)
        {
            statsText.text = statsText.text + "Purchased AP Bullets: Yes\n";
        }
        else
        {
            statsText.text = statsText.text + "Purchased AP Bullets: No\n";
        }

        //flight engy upgrade
        if (flightEngy)
        {
            statsText.text = statsText.text + "Purchased Flight Engineer: Yes\n";
        }
        else
        {
            statsText.text = statsText.text + "Purchased Flight Engineer: No\n";
        }

        //wingman upgrade
        if (wingman)
        {
            statsText.text = statsText.text + "Purchased Wingman: Yes\n";
        }
        else
        {
            statsText.text = statsText.text + "Purchased Wingman: No\n";
        }
    }


    //public void SetStatsText(string msg)
    //{

    //}
}
