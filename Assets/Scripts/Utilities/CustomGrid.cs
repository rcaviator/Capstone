using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    LineRenderer gridLines;

    //create the grid drawing points
    List<Vector3> tempGrid = new List<Vector3>();
    List<Vector3> gridDrawPoints = new List<Vector3>();

    //private void Awake ()
    //{
    //    if (gridLines)
    //    {
    //        gridLines = GetComponent<LineRenderer>();
    //    }
    //}


    public void Initialize()
    {
        //if line renderer is null for whatever reason
        if (!gridLines)
        {
            gridLines = GetComponent<LineRenderer>();
        }

        #region Grid Initialization

        GridPoints = new List<Vector3>();

        //populate the point array
        float x = 0;
        float y = 0;
        for (int p = 0; p < (Constants.LEVEL_EDITOR_GRID_SIZE_X * Constants.LEVEL_EDITOR_GRID_SIZE_Y); p++)
        {
            GridPoints.Add(new Vector3(x, y, 0));

            y += Constants.LEVEL_EDITOR_SPACING;

            if (y >= (Constants.LEVEL_EDITOR_GRID_SIZE_Y * Constants.LEVEL_EDITOR_SPACING))
            {
                y = 0;
                x += Constants.LEVEL_EDITOR_SPACING;
            }
        }

        //reuse x and y for drawing grid points axis steping. reset them to 0
        x = 0;
        y = 0;

        //shift the vertex array over
        for (int p = 0; p < GridPoints.Count; p++)
        {
            Vector3 temp = GridPoints[p];
            temp.x -= Constants.LEVEL_EDITOR_GRID_OFFSET_X;
            temp.y -= Constants.LEVEL_EDITOR_GRID_OFFSET_Y;
            GridPoints[p] = temp;
        }

        #endregion

        #region Draw Grid

        float drawX = 0;
        float drawY = 0;
        for (int dp = 0; dp < ((Constants.LEVEL_EDITOR_GRID_SIZE_X + 1) * (Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1)); dp++)
        {
            tempGrid.Add(new Vector3(drawX, drawY, 0));

            drawY += Constants.LEVEL_EDITOR_SPACING;

            if (drawY >= ((Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1) * Constants.LEVEL_EDITOR_SPACING))
            {
                drawY = 0;
                drawX += Constants.LEVEL_EDITOR_SPACING;
            }
        }

        //set offset
        for (int p = 0; p < tempGrid.Count; p++)
        {
            Vector3 temp = tempGrid[p];
            temp.x -= Constants.LEVEL_EDITOR_GRID_OFFSET_X;
            temp.y -= Constants.LEVEL_EDITOR_GRID_OFFSET_Y;
            tempGrid[p] = temp;
        }

        #region X Axis

        //handle x axis populating
        bool xClimb = true;
        bool xUp = true;
        int xIndexer = 0;

        //start at and add the first vertix point to the list. then loop through until x has reached the end
        while (x <= Constants.LEVEL_EDITOR_GRID_SIZE_X)
        {
            //add point
            gridDrawPoints.Add(new Vector3(tempGrid[xIndexer].x - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, tempGrid[xIndexer].y - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0));

            //calculate what the index of the next point is to be
            //do we need to climb?
            if (xClimb)
            {
                //do we climb up or down?
                if (xUp)
                {
                    xIndexer += Constants.LEVEL_EDITOR_GRID_SIZE_Y;//-1
                    xUp = false;
                }
                else
                {
                    xIndexer -= Constants.LEVEL_EDITOR_GRID_SIZE_Y;//-1
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

                //if this was the last point, break. xIndexer is used as the starting index for y populating
                if (x == Constants.LEVEL_EDITOR_GRID_SIZE_X + 1)
                {
                    break;
                }

                xIndexer += Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1;
            }
        }

        #endregion

        #region Y Axis

        //handle y axis populating
        bool jumpOver = true;
        bool yLeft = true;
        int yIndexer = xIndexer;

        //are we on the top or bottom of the grid after x popluating?
        if (tempGrid[yIndexer].y == 0 - Constants.LEVEL_EDITOR_GRID_OFFSET_Y)
        {
            //moving from bottom to top variables
            int leftClimber = 0;
            int rightClimber = tempGrid.Count - (Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1);

            //calculate the next point before adding it to the list. then loop through until the end of y
            while (y <= Constants.LEVEL_EDITOR_GRID_SIZE_Y)
            {
                //do we jump over?
                if (jumpOver)
                {
                    //do we jump over left or right?
                    if (yLeft)
                    {
                        //set yIndexer
                        yIndexer = leftClimber;
                    }
                    else
                    {
                        //set yIndexer
                        yIndexer = rightClimber;
                    }

                    //prepare for next jump
                    leftClimber++;
                    rightClimber++;
                    jumpOver = false;
                }
                //else climb up
                else
                {
                    //which side are we climbing?
                    if (yLeft)
                    {
                        //set yIndexer
                        yIndexer = leftClimber;

                        //prepare for next jump
                        yLeft = false;
                    }
                    else
                    {
                        //set yIndexer
                        yIndexer = rightClimber;

                        //prepare for next jump
                        yLeft = true;
                    }

                    jumpOver = true;

                    //increment y since this was a climb jump
                    y++;
                }

                //index protection
                if (y == Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1)
                {
                    break;
                }

                //add point
                gridDrawPoints.Add(new Vector3(tempGrid[yIndexer].x - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, tempGrid[yIndexer].y - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0));
            }
        }
        else
        {
            //moving from top to bottom
            y = Constants.LEVEL_EDITOR_GRID_SIZE_Y + 1;
            int leftDecender = Constants.LEVEL_EDITOR_GRID_SIZE_Y;
            int rightDecender = tempGrid.Count - 1;

            //calculate the next point before adding it to the list. then loop through until the end of y
            while (y > 0)
            {
                //do we jump over?
                if (jumpOver)
                {
                    //do we jump over left or right?
                    if (yLeft)
                    {
                        //set yIndexer
                        yIndexer = leftDecender;
                    }
                    else
                    {
                        //set yIndexer
                        yIndexer = rightDecender;
                    }

                    //prepare for next jump
                    leftDecender--;
                    rightDecender--;
                    jumpOver = false;
                }
                //else climb up
                else
                {
                    //which side are we climbing?
                    if (yLeft)
                    {
                        //set yIndexer
                        yIndexer = leftDecender;

                        //prepare for next jump
                        yLeft = false;
                    }
                    else
                    {
                        //set yIndexer
                        yIndexer = rightDecender;

                        //prepare for next jump
                        yLeft = true;
                    }

                    jumpOver = true;

                    //decrement y since this was a decending jump
                    y--;
                }

                //index protection
                if (y == 0)
                {
                    break;
                }

                //add point
                gridDrawPoints.Add(new Vector3(tempGrid[yIndexer].x - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_X, tempGrid[yIndexer].y - Constants.LEVEL_EDITOR_GRID_DRAW_OFFSET_Y, 0));
            }

        }

        #endregion

        gridLines.loop = false;
        gridLines.positionCount = gridDrawPoints.Count;
        gridLines.SetPositions(gridDrawPoints.ToArray());

        #endregion
    }

    /// <summary>
    /// The List of grid points as Vector3's
    /// </summary>
    public List<Vector3> GridPoints
    { get; set; }

    /// <summary>
    /// Gets the nearest point in the gird as a Vector3.
    /// Returns Vector3.forward (0,0,1) if passed position is too far out of the grid.
    /// </summary>
    /// <param name="position">The position to get nearest grid point</param>
    /// <returns>The position of the nearest grid point</returns>
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        //round the x and y of the position to test
        int xCount = Mathf.RoundToInt(position.x / Constants.LEVEL_EDITOR_SPACING);
        int yCount = Mathf.RoundToInt(position.y / Constants.LEVEL_EDITOR_SPACING);
        //int zCount = Mathf.RoundToInt(position.z / Constants.LEVEL_EDITOR_SPACING);

        //get the grid point matching the rounded point and return that value
        Vector3 testLocation = new Vector3(xCount * Constants.LEVEL_EDITOR_SPACING, yCount * Constants.LEVEL_EDITOR_SPACING, 0);

        //set index
        int index = 0;

        //is the test location vector in the grid
        if (GridPoints.Contains(testLocation))
        {
            //set index
            index = GridPoints.FindIndex(a => a == testLocation);
        }
        else
        {
            return Vector3.forward;
        }

        //return the vector in the grid
        return GridPoints[index];
    }


    private void OnDrawGizmos()
    {
        //main grid
        Gizmos.color = Color.yellow;
        foreach (Vector3 point in GridPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
