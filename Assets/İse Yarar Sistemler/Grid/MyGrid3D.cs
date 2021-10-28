using System;
using UnityEngine;

public class MyGrid3D<TMyGridObject2D>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }
    public MyGrid3D(int width, int height, int cellSize, Vector3 originPos, Func<MyGrid3D<TMyGridObject2D>, int, int, TMyGridObject2D> creatingObject, float debugWidth = 1, float debugHeight = 1)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new TMyGridObject2D[width, height];
        for (int e = 0; e < gridArray.GetLength(0); e++)
        {
            for (int h = 0; h < gridArray.GetLength(1); h++)
            {
                gridArray[e, h] = creatingObject(this, e, h);
            }
        }
        for (int e = 0; e < gridArray.GetLength(0); e++)
        {
            for (int h = 0; h < gridArray.GetLength(1); h++)
            {
                StaticFonksiyon.CreateWorldText(gridArray[e, h].ToString(), null, GetWorldPosition(e, h) + new Vector3(cellSize, 2.4f, cellSize) * 0.5f, 12.5f, debugWidth, debugHeight, Color.white);
                Debug.DrawLine(GetWorldPosition(e, h), GetWorldPosition(e, h + 1), Color.white, 100);
                Debug.DrawLine(GetWorldPosition(e, h), GetWorldPosition(e + 1, h), Color.red, 100);
            }
        }
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100);
    }
    private int width;
    private int height;
    private int cellSize;
    private Vector3 originPos;
    private TMyGridObject2D[,] gridArray;

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public int GetCellSize()
    {
        return cellSize;
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + originPos;
    }
    public void GetXZ(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        z = Mathf.FloorToInt((worldPos - originPos).z / cellSize);
    }
    private bool InGridTable(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 &&z < height;
    }
    public void SetGridObject(int x, int z, TMyGridObject2D value)
    {
        if (InGridTable(x, z))
        {
            gridArray[x, z] = value;
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
        }
        else
        {
            Debug.Log("Grid Dısındasın : " + x + "---" + z);
        }
    }
    public void TriggedGridObject(int x, int z)
    {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }
    public void SetGridObject(Vector3 worldPos, TMyGridObject2D value)
    {
        int x = 0;
        int z = 0;
        GetXZ(worldPos, out x, out z);
        SetGridObject(x, z, value);
    }
    public TMyGridObject2D GetGridObject(int x, int z)
    {
        if (InGridTable(x, z))
        {
            return gridArray[x, z];
        }
        else
        {
            Debug.Log("Grid Dısındasın : " + x + "---" + z + "---" + default(TMyGridObject2D));
            return default(TMyGridObject2D);
        }
    }
    public TMyGridObject2D GetGridObject(Vector3 worldPos)
    {
        int x = 0;
        int z = 0;
        GetXZ(worldPos, out x, out z);
        return GetGridObject(x, z);
    }
}
public class GridObject3DBasic
{
    public GridObject3DBasic(MyGrid3D<GridObject3DBasic> myGrid, int x, int z)
    {
        this.myGrid = myGrid;
        this.x = x;
        this.z = z;
    }
    private int x;
    private int z;
    private MyGrid3D<GridObject3DBasic> myGrid;

    private PlacedObject placedObjectBuild;
    public override string ToString()
    {
        return (x + "-" + z + "   " + placedObjectBuild).ToString();
    }
    public bool CanPlacedObjectBuild()
    {
        return placedObjectBuild == null;
    }
    public void SetPlacedObjectBuild(PlacedObject placedObjectBuild)
    {
        this.placedObjectBuild = placedObjectBuild;
        myGrid.TriggedGridObject(x, z);
    }
    public void ClearPlacedObjectBuild()
    {
        this.placedObjectBuild = null;
        myGrid.TriggedGridObject(x, z);
    }
    public PlacedObject GetPlacedObject()
    {
        return placedObjectBuild;
    }
}
public class GridObject3DBool
{
    public GridObject3DBool(MyGrid3D<GridObject3DBool> myGrid, int x, int z, bool value)
    {
        this.myGrid = myGrid;
        this.x = x;
        this.z = z;
        this.value = value;
    }
    private bool value;
    private int x;
    private int z;
    private MyGrid3D<GridObject3DBool> myGrid;
    public void AddValue(bool addValue)
    {
        value = addValue;
        myGrid.TriggedGridObject(x, z);
    }
    public override string ToString()
    {
        return value.ToString();
    }
}
public class GridObjectHeat3DInt
{
    public GridObjectHeat3DInt(MyGrid3D<GridObjectHeat3DInt> myGrid, int x, int y, int value, int minValue, int maxValue)
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
    private MyGrid3D<GridObjectHeat3DInt> myGrid;
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