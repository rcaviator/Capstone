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

    ////set out of bounds points
    //List<Vector3> leftSideYPoints = new List<Vector3>();
    //List<Vector3> rightSideYPoints = new List<Vector3>();
    //List<Vector3> topSideXPoints = new List<Vector3>();
    //List<Vector3> bottomSideXPoints = new List<Vector3>();
    //Vector3 topRightPoint = new Vector3();

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

        //poulate left side y points
        //for (int ly = 0; ly < Constants.LEVEL_EDITOR_GRID_SIZE_Y; ly++)
        //{
        //    leftSideYPoints.Add(new Vector3(GridPoints[ly].x, GridPoints[ly].y, 0));
        //}

        ////populate right side y points
        //for (int ry = GridPoints.Count - Constants.LEVEL_EDITOR_GRID_SIZE_Y; ry < GridPoints.Count; ry++)
        //{
        //    rightSideYPoints.Add(new Vector3(GridPoints[ry].x + Constants.LEVEL_EDITOR_SPACING, GridPoints[ry].y, 0));
        //}

        ////populate bottom side x points
        //for (int bx = 0; bx < Constants.LEVEL_EDITOR_GRID_SIZE_Y; bx += Constants.LEVEL_EDITOR_GRID_SIZE_Y)
        //{
        //    bottomSideXPoints.Add(new Vector3(GridPoints[bx].x, GridPoints[bx].y, 0));
        //}

        ////populate top side x points
        //for (int tx = Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1; tx < Constants.LEVEL_EDITOR_GRID_SIZE_X * Constants.LEVEL_EDITOR_GRID_SIZE_Y; tx += Constants.LEVEL_EDITOR_GRID_SIZE_Y)
        //{
        //    topSideXPoints.Add(new Vector3(GridPoints[tx].x, GridPoints[tx].y + Constants.LEVEL_EDITOR_SPACING, 0));
        //}

        ////get corner point
        //topRightPoint = new Vector3(rightSideYPoints.Last().x, topSideXPoints.Last().y, 0);

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
        //main grid
        Gizmos.color = Color.yellow;
        foreach (Vector3 point in GridPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }

        //foreach (Vector3 point in tempGrid)
        //{
        //    Gizmos.DrawSphere(point, 0.1f);
        //}

        //top row extra
        //Gizmos.color = Color.red;
        //foreach (Vector3 point in topSideXPoints)
        //{
        //    Gizmos.DrawSphere(point, 0.1f);
        //}

        ////right row extra
        //Gizmos.color = Color.green;
        //foreach (Vector3 point in rightSideYPoints)
        //{
        //    Gizmos.DrawSphere(point, 0.1f);
        //}

        ////corner point
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(topRightPoint, 0.1f);
    }
}
