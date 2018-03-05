using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGridCell
{
    //the prefab
    GameObject cellGameObject;

    //the actual object
    GameObject referencedObject;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="location">the cell location in the grid</param>
    public CustomGridCell(Vector3 location, Vector2 indexLocation, GameObject cellObject = null)
    {
        GridLocation = location;
        IndexLocation = indexLocation;
        CellObject = cellObject;
        if (CellObject)
        {
            IsOccupied = true;
        }
    }

    /// <summary>
    /// Is the cell occupied with an object
    /// </summary>
    public bool IsOccupied
    { get; private set; }

    /// <summary>
    /// The game object in the cell
    /// </summary>
    public GameObject CellObject
    {
        get { return referencedObject; }
        set
        {
            //if there is a game object passed in
            if (value != null)
            {
                //if there already is an game object here, remove it
                if (referencedObject)
                {
                    MonoBehaviour.Destroy(referencedObject);
                }
                //new prefab
                cellGameObject = value;
                IsOccupied = true;
                //new object
                referencedObject = MonoBehaviour.Instantiate(cellGameObject, GridLocation, Quaternion.identity);
            }
            //else null is passed in
            else
            {
                //destroy any game object here
                if (referencedObject)
                {
                    MonoBehaviour.Destroy(referencedObject);
                }
                cellGameObject = null;
                IsOccupied = false;
            }
        }
    }

    /// <summary>
    /// The grid cell location
    /// </summary>
    public Vector3 GridLocation
    { get; set; }

    /// <summary>
    /// Index location
    /// </summary>
    public Vector2 IndexLocation
    { get; set; }
}
