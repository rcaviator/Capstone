using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    LineRenderer gridLines;


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

        //create the grid drawing points
        List<Vector3> gridDrawPoints = new List<Vector3>();

        #region X Axis
        
        //set out of bounds points
        List<Vector3> outBoundPointsX = new List<Vector3>();
        List<Vector3> outBoundPointsY = new List<Vector3>();
        Vector3 outBoundPointXY = new Vector3();

        //populate outboundpoints x
        for (int bx = GridPoints.Count - Constants.LEVEL_EDITOR_GRID_SIZE_Y; bx < GridPoints.Count; bx++)
        {
            outBoundPointsX.Add(new Vector3(GridPoints[bx].x + Constants.LEVEL_EDITOR_SPACING, GridPoints[bx].y, 0));
        }

        //populate outboundpoints y
        for (int by = Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1; by < Constants.LEVEL_EDITOR_GRID_SIZE_X * Constants.LEVEL_EDITOR_GRID_SIZE_Y; by += Constants.LEVEL_EDITOR_GRID_SIZE_Y)
        {
            outBoundPointsY.Add(new Vector3(GridPoints[by].x, GridPoints[by].y + Constants.LEVEL_EDITOR_SPACING, 0));
        }
        
        //get corner point
        outBoundPointXY = new Vector3(outBoundPointsX.Last().x, outBoundPointsY.Last().y, 0);

        //handle x axis populating
        bool xClimb = true;
        bool xUp = true;
        int xIndexer = 0;

        //start at and add the first vertix point to the list. then loop through until x has reached the end
        while (x < Constants.LEVEL_EDITOR_GRID_SIZE_X)
        {
            //add point
            gridDrawPoints.Add(new Vector3(GridPoints[xIndexer].x - Constants.LEVEL_EDITOR_SPACING / 2, GridPoints[xIndexer].y - Constants.LEVEL_EDITOR_SPACING / 2, 0));

            //calculate what the index of the next point is to be
            //do we need to climb?
            if (xClimb)
            {
                //do we climb up or down?
                if (xUp)
                {
                    xIndexer += Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1;
                    xUp = false;
                }
                else
                {
                    xIndexer -= Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1;
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
                if (x == Constants.LEVEL_EDITOR_GRID_SIZE_X)
                {
                    break;
                }

                xIndexer += Constants.LEVEL_EDITOR_GRID_SIZE_Y;
            }
        }

        #endregion

        #region Y Axis

        //handle y axis populating
        bool jumpOver = true;
        bool yLeft = true;
        int yIndexer = xIndexer;

        //are we on the top or bottom of the grid after x popluating?
        if (GridPoints[yIndexer].y == 0 - Constants.LEVEL_EDITOR_GRID_OFFSET_Y)
        {
            //moving from bottom to top variables
            int leftClimber = 0;
            int rightClimber = GridPoints.Count - Constants.LEVEL_EDITOR_GRID_SIZE_Y;

            //calculate the next point before adding it to the list. then loop through until the end of y
            while (y < Constants.LEVEL_EDITOR_GRID_SIZE_Y)
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
                if (y == Constants.LEVEL_EDITOR_GRID_SIZE_Y)
                {
                    break;
                }

                //add point
                gridDrawPoints.Add(new Vector3(GridPoints[yIndexer].x - Constants.LEVEL_EDITOR_SPACING / 2, GridPoints[yIndexer].y - Constants.LEVEL_EDITOR_SPACING / 2, 0));
            }
        }
        else
        {
            //moving from top to bottom
            y = Constants.LEVEL_EDITOR_GRID_SIZE_Y;
            int leftDecender = Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1;
            int rightDecender = GridPoints.Count - 1;

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
                gridDrawPoints.Add(new Vector3(GridPoints[yIndexer].x - Constants.LEVEL_EDITOR_SPACING / 2, GridPoints[yIndexer].y - Constants.LEVEL_EDITOR_SPACING / 2, 0));
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


    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / Constants.LEVEL_EDITOR_SPACING);
        int yCount = Mathf.RoundToInt(position.y / Constants.LEVEL_EDITOR_SPACING);
        int zCount = Mathf.RoundToInt(position.z / Constants.LEVEL_EDITOR_SPACING);

        Vector3 result = new Vector3(xCount * Constants.LEVEL_EDITOR_SPACING, yCount * Constants.LEVEL_EDITOR_SPACING, zCount * Constants.LEVEL_EDITOR_SPACING);

        result += transform.position;

        return result;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Vector3 point in GridPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
