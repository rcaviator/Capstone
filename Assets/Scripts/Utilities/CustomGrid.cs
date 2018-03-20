using System;
using System.Collections.Generic;
using System.IO;
//using System.IO.Directory;
using System.Linq;
using System.Text;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    //line renderer component
    LineRenderer gridLines;

    //dictionary of game object names and ids
    Dictionary<string, Constants.ObjectIDs> objectNames;

    //list for game objects
    List<GameObject> gameObjects = new List<GameObject>();

    private void Awake()
    {
        //get line reneder reference
        gridLines = GetComponent<LineRenderer>();

        //initialize dictionary
        objectNames = new Dictionary<string, Constants.ObjectIDs>()
        {
            //environment - blocks
            { "Dirt_Block(Clone)", Constants.ObjectIDs.DirtBlock },
            { "Dirt_Block_Grass(Clone)", Constants.ObjectIDs.DirtBlockGrass },
            { "Dirt_Block_Sloped(Clone)", Constants.ObjectIDs.DirtBlockSloped },
            { "Stone_Block(Clone)", Constants.ObjectIDs.StoneBlock },
            { "Stone_Block_Sloped(Clone)", Constants.ObjectIDs.StoneBlockSloped },
            { "Stone_Block_Concrete_Top(Clone)", Constants.ObjectIDs.StoneBlockConcreteTop },
            { "Stone_Block_Sloped_Concrete_Top(Clone)", Constants.ObjectIDs.StoneBlockSlopedConcreteTop },
            //environment - other


            //enemies
            { "TempBlimpEnemy(Clone)", Constants.ObjectIDs.TempBlimpEnemy },

            //utilities


        };
    }


    public void Initialize()
    {
        //if line renderer is null for whatever reason
        if (!gridLines)
        {
            gridLines = GetComponent<LineRenderer>();
        }

        #region Grid Initialization

        //declare the grid and grid draw points arrays
        GridPoints = new CustomGridCell[Constants.LEVEL_EDITOR_GRID_SIZE_X, Constants.LEVEL_EDITOR_GRID_SIZE_Y];
        DrawGridPoints2DArray = new Vector3[Constants.LEVEL_EDITOR_GRID_SIZE_X + 1, Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1];
        DrawGridPointsPositionList = new List<Vector3>();

        //populate the point array
        int x = 0;
        int y = 0;
        for (y = 0; y < Constants.LEVEL_EDITOR_GRID_SIZE_Y; y++)
        {
            for (x = 0; x < Constants.LEVEL_EDITOR_GRID_SIZE_X; x++)
            {
                GridPoints[x, y] = new CustomGridCell(new Vector3(x, y, 0), new Vector2(x, y));
            }
        }

        #endregion

        #region Draw Grid

        //populate the draw grid array only on the sides
        //bottom row
        for (int xStep = 0; xStep < DrawGridPoints2DArray.GetLength(0); xStep++)
        {
            DrawGridPoints2DArray[xStep, 0] = new Vector3(xStep - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, 0 - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0);
        }
        //top row
        for (int xStep = 0; xStep < DrawGridPoints2DArray.GetLength(0); xStep++)
        {
            DrawGridPoints2DArray[xStep, DrawGridPoints2DArray.GetLength(1) - 1] = new Vector3(xStep - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, DrawGridPoints2DArray.GetLength(1) - 1 - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0);
        }
        //left column
        for (int yStep = 0; yStep < DrawGridPoints2DArray.GetLength(1); yStep++)
        {
            DrawGridPoints2DArray[0, yStep] = new Vector3(0 - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, yStep - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0);
        }
        //right column
        for (int yStep = 0; yStep < DrawGridPoints2DArray.GetLength(1); yStep++)
        {
            DrawGridPoints2DArray[DrawGridPoints2DArray.GetLength(0) - 1, yStep] = new Vector3(DrawGridPoints2DArray.GetLength(0) - 1 /*- Constants.LEVEL_EDITOR_GRID_OFFSET_X */- Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, yStep /*- Constants.LEVEL_EDITOR_GRID_OFFSET_Y */- Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0);
        }

        //reset x and y for drawing
        x = 0;
        y = 0;

        #region X Axis

        //handle x axis populating
        bool xClimb = true;
        bool xUp = true;

        //start and add the first grid point to the list. then loop through until x has reached the end
        while (x < DrawGridPoints2DArray.GetLength(0))
        {
            //add point
            DrawGridPointsPositionList.Add(DrawGridPoints2DArray[x, y]);

            //determine what the coordinate of the next point is to be
            //do we need to climb?
            if (xClimb)
            {
                //do we climb up or down?
                if (xUp)
                {
                    y += DrawGridPoints2DArray.GetLength(1) - 1;
                    xUp = false;
                }
                else
                {
                    y -= DrawGridPoints2DArray.GetLength(1) - 1;
                    xUp = true;
                }

                //we're no longer climbing
                xClimb = false;
            }
            //else advnace over
            else
            {
                //next jump will be climbing
                xClimb = true;

                //we have shifted over on x, increment
                x++;
            }
        }

        #endregion

        #region Y Axis

        bool drawY = true;
        //handle y axis populating
        bool jumpOver = true;
        bool yLeft = true;

        //are we on the top or bottom of the grid after x popluating?
        //moving from bottom to top
        if (y == 0 && drawY)
        {
            Debug.Log("bottom");
            //calculate the next point before adding it to the list. then loop through until the end of y
            while (y < DrawGridPoints2DArray.GetLength(1))
            {
                //do we jump over?
                if (jumpOver)
                {
                    //do we jump over left or right?
                    if (yLeft)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = DrawGridPoints2DArray.GetLength(0) - 1;
                    }

                    //prepare for climb up
                    jumpOver = false;
                }
                //else climb up
                else
                {
                    //which side are we climbing?
                    if (yLeft)
                    {
                        //prepare for next jump
                        yLeft = false;
                    }
                    else
                    {
                        //prepare for next jump
                        yLeft = true;
                    }

                    //prepare for jump
                    jumpOver = true;

                    //increment y since this was a climb jump
                    y++;
                }

                //index protection
                if (y == DrawGridPoints2DArray.GetLength(1))
                {
                    break;
                }

                //add point
                DrawGridPointsPositionList.Add(DrawGridPoints2DArray[x, y]);
            }
        }
        //moving from top to bottom
        else if (drawY)
        {
            //calculate the next point before adding it to the list. then loop through until the end of y
            while (y >= 0)
            {
                //do we jump over?
                if (jumpOver)
                {
                    //do we jump over left or right?
                    if (yLeft)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = DrawGridPoints2DArray.GetLength(0) - 1;
                    }

                    //prepare for climb down
                    jumpOver = false;
                }
                //else climb down
                else
                {
                    //which side are we climbing down?
                    if (yLeft)
                    {
                        yLeft = false;
                    }
                    else
                    {
                        yLeft = true;
                    }

                    //prepare for jump
                    jumpOver = true;

                    //decrement y since this was a decending jump
                    y--;
                }

                //bounds check
                if (y == -1)
                {
                    break;
                }

                //add point
                DrawGridPointsPositionList.Add(DrawGridPoints2DArray[x, y]);
            }

        }

        #endregion

        //send draw position vector list to line renderer
        gridLines.loop = false;
        gridLines.positionCount = DrawGridPointsPositionList.Count;
        gridLines.SetPositions(DrawGridPointsPositionList.ToArray());

        #endregion
    }

    /// <summary>
    /// The List of grid points as grid cells
    /// </summary>
    public CustomGridCell[,] GridPoints
    { get; set; }

    /// <summary>
    /// The list of vectors for drawing the grid line with Line Renderer
    /// </summary>
    private List<Vector3> DrawGridPointsPositionList
    { get; set; }

    /// <summary>
    /// The vector array for storing points to be used in DrawGridPointsPositionList
    /// </summary>
    private Vector3[,] DrawGridPoints2DArray
    { get; set; }

    /// <summary>
    /// Gets the nearest point in the gird as a Vector3.
    /// Returns null if passed position is too far out of the grid.
    /// </summary>
    /// <param name="position">The position to get the nearest grid cell</param>
    /// <returns>The nearest grid cell if not outside the grid</returns>
    public CustomGridCell GetGridCellInGrid(Vector3 location)
    {
        //round the x and y of the position to test
        int xPosition = Mathf.RoundToInt(location.x);
        int yPosition = Mathf.RoundToInt(location.y);
        //int zPosition = Mathf.RoundToInt(location.z / Constants.LEVEL_EDITOR_SPACING);

        //bounds check
        if (xPosition < 0 || xPosition > GridPoints.GetLength(0) - 1)
        {
            return null;
        }
        //y bounds
        else if (yPosition < 0 || yPosition > GridPoints.GetLength(1) - 1)
        {
            return null;
        }

        //in bounds
        return GridPoints[xPosition, yPosition];
    }

    /// <summary>
    /// Sets an object in the grid if the cell is not occupied
    /// </summary>
    /// <param name="cellObject">the object to place in the grid</param>
    public void SetGameObjectInGrid(CustomGridCell cell, GameObject cellObject, bool flipped)
    {
        //get coordinates
        int x = (int)cell.IndexLocation.x;
        int y = (int)cell.IndexLocation.y;

        //check if the cell is occupied
        if (!GridPoints[x, y].IsOccupied)
        {
            //set game object in grid and add to list
            if (flipped)
            {
                GridPoints[x, y].IsFlipped = true;
            }
            GridPoints[x, y].CellObject = cellObject;
            gameObjects.Add(GridPoints[x, y].CellObject);
        }
    }

    /// <summary>
    /// Removes a game object from the grid if the cell is occupied
    /// </summary>
    /// <param name="cell">the cell to remove a game object from</param>
    public void RemoveGameObjectInGrid(CustomGridCell cell)
    {
        //get coordinates
        int x = (int)cell.IndexLocation.x;
        int y = (int)cell.IndexLocation.y;

        //check if there is an object in the cell
        if (GridPoints[x, y].IsOccupied)
        {
            //remove game object from cell and remove from list
            //Debug.Log("RemoveGameObjectInGrid: Removing game object in cell at " + GridPoints[x, y].GridLocation);
            gameObjects.Remove(GridPoints[x, y].CellObject);
            GridPoints[x, y].CellObject = null;
        }
        //else
        //{
        //    Debug.Log("RemoveGameObjectInGrid: Target cell is already empty at " + GridPoints[index].GridLocation);
        //}
    }

    /// <summary>
    /// Fills all the columns under the first found dirt/grass/sloped block with dirt blocks.
    /// Will not replace existing blocks.
    /// </summary>
    public void FillDirt()
    {
        bool foundTriggerBlockType = false;
        for (int x = 0; x < Constants.LEVEL_EDITOR_GRID_SIZE_X; x++)
        {
            //reset trigger bool
            foundTriggerBlockType = false;

            for (int y = Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1; y >= 0; y--)
            {
                //locate the first instance of a valid block
                if (GridPoints[x, y].IsOccupied && !foundTriggerBlockType)
                {
                    //check for applicable trigger block
                    if (GridPoints[x, y].CellObject.name == "Dirt_Block(Clone)" ||
                        GridPoints[x, y].CellObject.name == "Dirt_Block_Grass(Clone)" ||
                        GridPoints[x, y].CellObject.name == "Dirt_Block_Sloped(Clone)" ||
                        GridPoints[x, y].CellObject.name == "Stone_Block_Sloped(Clone)" ||
                        GridPoints[x, y].CellObject.name == "Stone_Block_Concrete_Top(Clone)" ||
                        GridPoints[x, y].CellObject.name == "Stone_Block_Sloped_Concrete_Top(Clone)")
                    {
                        foundTriggerBlockType = true;
                        continue;
                    }
                }

                //check if any cell under the found trigger block can be filled in
                if (foundTriggerBlockType && !GridPoints[x, y].IsOccupied)
                {
                    GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block");
                    gameObjects.Add(GridPoints[x, y].CellObject);
                }
            }
        }
    }

    public void SaveModule(int level, int number)
    {
        //create the folder if it does not exist
        if (!Directory.Exists(Application.dataPath + "/Modules"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Modules");
        }

        //set file path
        string file = Application.dataPath + "/Modules" + "/Level_" + level.ToString() + "_Module_" + number.ToString() + ".mod";

        //override the file
        if (File.Exists(file))
        {
            Debug.Log("Deleting " + file);
            File.Delete(file);
        }

        //open stream
        using (Stream s = File.OpenWrite(file))
        {
            //create writer
            using (BinaryWriter w = new BinaryWriter(s))
            {
                //write header
                w.Write(Constants.MODULE_FILE_HEADER.ToCharArray());
                w.Write(gameObjects.Count);

                //loop through game object list and save object type and location
                foreach (GameObject item in gameObjects)
                {
                    //determine game object type
                    w.Write((int)objectNames[item.name]);

                    //write if the object is flipped
                    if (item.transform.rotation.eulerAngles.y == 180)
                    {
                        w.Write(true);
                    }
                    else
                    {
                        w.Write(false);
                    }
                    
                    //write game object's location
                    w.Write((int)item.transform.position.x);
                    w.Write((int)item.transform.position.y);
                }
            }
        }
    }


    public void LoadModule(string moduleName)
    {
        //set file path
        Debug.Log(moduleName);
        string file = Application.dataPath + "/Modules/" + moduleName;

        //load the module if the file exists
        if (File.Exists(file))
        {
            //prep grid for new module
            ClearGrid();

            //open stream
            using (Stream s = File.OpenRead(file))
            {
                //create reader
                using (BinaryReader r = new BinaryReader(s))
                {
                    //verify file is of correct format
                    string head = new string(r.ReadChars(4));
                    if (!head.Equals(Constants.MODULE_FILE_HEADER))
                    {
                        Debug.Log("File not of correct format");
                        return;
                    }

                    //get number of game objects
                    int numberOfGameObjects = r.ReadInt32();

                    //loop through the file and create each game object, add to grid, and move it
                    for (int i = 0; i < numberOfGameObjects; i++)
                    {
                        //get type
                        int typeInt = r.ReadInt32();
                        Constants.ObjectIDs type = (Constants.ObjectIDs)typeInt;

                        //check if flipped
                        bool flipped = r.ReadBoolean();

                        //get position
                        int x = r.ReadInt32();
                        int y = r.ReadInt32();

                        //prep cell
                        GridPoints[x, y].IsFlipped = flipped;

                        //create object
                        switch (type)
                        {
                            case Constants.ObjectIDs.None:
                                break;

                            //environment - blocks
                            case Constants.ObjectIDs.DirtBlock:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block");
                                break;
                            case Constants.ObjectIDs.DirtBlockGrass:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block_Grass");
                                break;
                            case Constants.ObjectIDs.DirtBlockSloped:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block_Sloped");
                                break;
                            case Constants.ObjectIDs.StoneBlock:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Stone_Block");
                                break;
                            case Constants.ObjectIDs.StoneBlockSloped:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Sloped");
                                break;
                            case Constants.ObjectIDs.StoneBlockConcreteTop:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Concrete_Top");
                                break;
                            case Constants.ObjectIDs.StoneBlockSlopedConcreteTop:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Sloped_Concrete_Top");
                                break;

                            //environment - other


                            //enemies
                            case Constants.ObjectIDs.TempBlimpEnemy:
                                GridPoints[x, y].CellObject = Resources.Load<GameObject>("Prefabs/Enemies/TempBlimpEnemy");
                                break;
                            //utilities


                            default:
                                break;
                        }

                        //add game object to list
                        gameObjects.Add(GridPoints[x, y].CellObject);
                    }
                }
            }
            
        }
        else
        {
            Debug.Log("LoadModule: File does not exist");
        }
    }

    /// <summary>
    /// Clears the grid
    /// </summary>
    public void ClearGrid()
    {
        for (int y = 0; y < GridPoints.GetLength(1); y++)
        {
            for (int x = 0; x < GridPoints.GetLength(0); x++)
            {
                //check if there is an object
                if (GridPoints[x, y].IsOccupied)
                {
                    gameObjects.Remove(GridPoints[x, y].CellObject);
                    GridPoints[x, y].CellObject = null;
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    //main grid
    //    Gizmos.color = Color.yellow;
    //    for (int y = 0; y < GridPoints.GetLength(1); y++)
    //    {
    //        for (int x = 0; x < GridPoints.GetLength(0); x++)
    //        {
    //            Gizmos.DrawSphere(GridPoints[x, y].GridLocation, 0.1f);
    //        }
    //    }

    //    //draw points grid
    //    Gizmos.color = Color.red;
    //    for (int y = 0; y < DrawGridPoints2DArray.GetLength(1); y++)
    //    {
    //        for (int x = 0; x < DrawGridPoints2DArray.GetLength(0); x++)
    //        {
    //            Gizmos.DrawSphere(DrawGridPoints2DArray[x, y], 0.1f);
    //        }
    //    }
    //}
}
