using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridTester3D : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform maskObject;
    private MyGrid3D<GridObjectHeat3DInt> gridInt;
    private MyGrid3D<GridObject3DBool> gridBool;
    private void Start()
    {
        gridInt = new MyGrid3D<GridObjectHeat3DInt>(4, 2, 5, new Vector3(-10, 0, -3), (MyGrid3D<GridObjectHeat3DInt> g, int x, int y) => new GridObjectHeat3DInt(g, x, y, 0, 0, 100));
        gridBool = new MyGrid3D<GridObject3DBool>(4, 2, 5, new Vector3(-10, 0, 10), (MyGrid3D<GridObject3DBool> g, int x, int y) => new GridObject3DBool(g, x, y, false));
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999, mask))
        {
            maskObject.position = raycastHit.point;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GridObjectHeat3DInt gridObject2DInt = gridInt.GetGridObject(maskObject.position);
            if (gridObject2DInt != null)
            {
                gridObject2DInt.AddValue(5);
            }
            gridInt.GetGridObject(maskObject.position);
            GridObject3DBool gridObject2DBool = gridBool.GetGridObject(maskObject.position);
            if (gridObject2DBool != null)
            {
                gridObject2DBool.AddValue(true);
            }
            gridBool.GetGridObject(maskObject.position);
        }
    }
}