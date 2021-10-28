using UnityEngine;

public class GridTester2D : MonoBehaviour
{
    private MyGrid2D<GridObject2DInt> gridInt;
    private MyGrid2D<GridObject2DBool> gridBool;
    private void Start()
    {
        gridInt = new MyGrid2D<GridObject2DInt>(4, 2, 5, new Vector3(-10, 5, 0), (MyGrid2D<GridObject2DInt> g, int x, int y) => new GridObject2DInt(g, x, y, 0, 0, 100));
        gridBool = new MyGrid2D<GridObject2DBool>(4, 2, 5, new Vector3(-10, -15, 0), (MyGrid2D<GridObject2DBool> g, int x, int y) => new GridObject2DBool(g, x, y, false));
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = StaticFonksiyon.MousePozisyon();
            Debug.Log(worldPos);

            GridObject2DInt gridObject2DInt = gridInt.GetGridObject(worldPos);
            if (gridObject2DInt != null)
            {
                gridObject2DInt.AddValue(5);
            }
            gridInt.GetGridObject(worldPos);
            GridObject2DBool gridObject2DBool = gridBool.GetGridObject(worldPos);
            if (gridObject2DBool != null)
            {
                gridObject2DBool.AddValue(true);
            }
            gridBool.GetGridObject(worldPos);
        }
    }

}
public class GridObject2DBool
{
    public GridObject2DBool(MyGrid2D<GridObject2DBool> myGrid, int x, int y, bool value)
    {
        this.myGrid = myGrid;
        this.x = x;
        this.y = y;
        this.value = value;
    }
    private bool value;
    private int x;
    private int y;
    private MyGrid2D<GridObject2DBool> myGrid;
    public void AddValue(bool addValue)
    {
        value = addValue;
        myGrid.TriggedGridObject(x, y);
    }
    public override string ToString()
    {
        return value.ToString();
    }
}
public class GridObject2DInt
{
    public GridObject2DInt(MyGrid2D<GridObject2DInt> myGrid, int x, int y, int value, int minValue, int maxValue)
    {
        this.myGrid = myGrid;
        this.x = x;
        this.y = y;
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
    private int value;
    private int minValue;
    private int maxValue;
    private int x;
    private int y;
    private MyGrid2D<GridObject2DInt> myGrid;
    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, minValue, maxValue);
        myGrid.TriggedGridObject(x, y);
    }
    public float GetValueNormalized()
    {
        return 1.0f * value / maxValue;
    }
    public override string ToString()
    {
        return value.ToString();
    }
}