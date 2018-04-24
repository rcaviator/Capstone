using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelInventoryTextScript : MonoBehaviour
{
    //text reference
    Text inventoryText;

    // Use this for initialization
    void Awake()
    {
        //set references
        UIManager.Instance.PreLevelInventoryText = this;
        inventoryText = GetComponent<Text>();
    }


    private void OnEnable()
    {
        //set text
        inventoryText.text = "Energy Shields: " + GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.EnergyShield).ToString() + "\n"
            + "Cluster Bombs: " + GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.ClusterBomb).ToString() + "\n"
            + "Seeker Missiles: " + GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.SeekerMissiles).ToString() + "\n"
            + "Repair Packs: " + GameManager.Instance.PlayerInventory.ViewItemCount(ItemType.RepairPack).ToString();
    }
}
