using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorControllerScript : MonoBehaviour
{
    Text titleText;
    LineRenderer gridLines;

    CustomGrid grid;

	// Use this for initialization
	void Awake ()
    {
        //load UI elements

        //draw grid
        grid = GetComponent<CustomGrid>();
        grid.Initialize();
        

        //set other references

    }

    

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit hitInfo;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(ray, out hitInfo))
            //{
            //    PlaceCubeNear(hitInfo.point);
            //}
            PlaceCubeNear(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public List<Vector3> GridPoints
    { get; set; }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        clickPoint.z = 0f;
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
}
