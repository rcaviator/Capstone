using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Item type enum for inventory
/// </summary>
[Serializable]
public enum ItemType
{
    //default
    None,

    //weapons
    APBullets, ClusterBomb, SeekerMissiles, EnergyBeam,

    //defensive
    EnergyShield, Decoy,

    //other consumables
    RepairPack,

    //upgrades
    AircraftArmor, FlightEngineer, WingMan,
}

[Serializable]
public class Inventory
{
    #region Fields

    //the file path
    string file;

    #endregion

    #region Constructor

    /// <summary>
    /// Main Constructor. Takes in a string as the file path name to be used
    /// for saving and loading.
    /// </summary>
    /// <param name="inventoryFile">The full path name of the file</param>
    public Inventory(string inventoryFile)
    {
        //set file path
        file = inventoryFile;
        
        //initialize the dictionary
        MainInventory = new Dictionary<ItemType, int>();
    }

    #endregion

    #region Properties

    /// <summary>
    /// The main inventory dictionary
    /// </summary>
    Dictionary<ItemType, int> MainInventory
    { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a quantity onto a selected item into the inventory dictionary.
    /// </summary>
    /// <param name="item">The item to add more to</param>
    /// <param name="amount">The amount to add onto that item</param>
    public void AddItem(ItemType item, int amount)
    {
        if (MainInventory.ContainsKey(item))
        {
            MainInventory[item] += amount;
        }
        else
        {
            Debug.Log("Add Item: Item is not in inventory dictionary");
        }
    }

    /// <summary>
    /// Removes a quantity from a selected item in the inventory dictionary.
    /// Items do not go below 0 amount.
    /// </summary>
    /// <param name="item">The item to remove amount from</param>
    /// <param name="amount">The amount to subtract in the inventory</param>
    public void RemoveItem(ItemType item, int amount)
    {
        if (MainInventory.ContainsKey(item))
        {
            if (MainInventory[item] > 0 && (MainInventory[item] - amount >= 0))
            {
                MainInventory[item] -= amount;
            }
            else
            {
                Debug.Log("Item goes below 0 amount");
            }
        }
        else
        {
            Debug.Log("Remove Item: Item is not in the inventory dictionary");
        }
    }

    /// <summary>
    /// Views the count of a selected item.
    /// </summary>
    /// <param name="item">The item to view the count for</param>
    /// <returns>The amount of the selected item remaining in the inventory</returns>
    public int ViewItemCount(ItemType item)
    {
        if (MainInventory.ContainsKey(item))
        {
            return MainInventory[item];
        }
        else
        {
            Debug.Log("View Item Count: Item is not in the inventory dictionary");
            return 0;
        }
    }

    /// <summary>
    /// Loads the inventory from the file specified at object creation and populates
    /// the inventory dictionary.
    /// </summary>
    public void LoadInventory()
    {
        //load the inventory if the file exists
        if (File.Exists(file))
        {
            //open stream
            using (Stream fs = File.OpenRead(file))
            {
                //create reader
                BinaryFormatter bf = new BinaryFormatter();

                //create deserializing list and get data
                List<int> listToDeserialize = (List<int>)bf.Deserialize(fs);

                //setup the inventory dictonary
                for (int i = 0; i < listToDeserialize.Count; i++)
                {
                    MainInventory.Add((ItemType)i, listToDeserialize[i]);
                }
            }

            //print out contents
            if (Constants.IS_DEVELOPER_BUILD)
            {
                foreach (KeyValuePair<ItemType, int> item in MainInventory)
                {
                    Debug.Log(item.Key + " " + item.Value);
                }
            }
        }
        else
        {
            Debug.Log("Inventory: File does not exist. Creating file...");

            //setup the inventory dictonary with default 0 values
            for (int i = 0; i < Enum.GetNames(typeof(ItemType)).Length; i++)
            {
                MainInventory.Add((ItemType)i, 0);
            }

            //create the file and save the data
            SaveInventory();

            if (File.Exists(file))
            {
                Debug.Log("File now exists");
            }
        }
    }

    /// <summary>
    /// Saves the inventory to the file specified at object creation.
    /// </summary>
    public void SaveInventory()
    {
        //create the folder if it does not exist
        if (!Directory.Exists(Application.dataPath + "/GameData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/GameData");
        }

        //override the file
        if (File.Exists(file))
        {
            File.Delete(file);
        }

        //create stream
        using (Stream fs = File.OpenWrite(file))
        {
            //create writer
            BinaryFormatter bf = new BinaryFormatter();

            //create serializable list
            List<int> listToSerialize = new List<int>();

            //initialize list
            for (int i = 0; i < MainInventory.Count; i++)
            {
                listToSerialize.Add(MainInventory[(ItemType)i]);
            }

            //serialize list
            bf.Serialize(fs, listToSerialize);
        }
    }

    #endregion
}

