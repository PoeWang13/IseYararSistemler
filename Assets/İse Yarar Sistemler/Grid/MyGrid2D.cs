using System;
using UnityEngine;

public class MyGrid2D<TMyGridObject2D>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }
    public MyGrid2D(int width, int height, int cellSize, Vector3 originPos, Func<MyGrid2D<TMyGridObject2D>, int, int, TMyGridObject2D> creatingObject, float debugWidth = 1, float debugHeight = 1)
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

        Debug.Log("Grid : " + width + "---" + height);
        for (int e = 0; e < gridArray.GetLength(0); e++)
        {
            for (int h = 0; h < gridArray.GetLength(1); h++)
            {
                StaticFonksiyon.CreateWorldText(gridArray[e, h].ToString(), null, GetWorldPosition(e, h) + new Vector3(cellSize, cellSize) * 0.5f, 20, debugWidth, debugHeight, Color.white);
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
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }
    private void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);
    }
    private bool InGridTable(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
    public void SetGridObject(int x, int y, TMyGridObject2D value)
    {
        if (InGridTable(x, y))
        {
            gridArray[x, y] = value;
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
        else
        {
            Debug.Log("Grid Dısındasın : " + x + "---" + y);
        }
    }
    public void TriggedGridObject(int x, int y)
    {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
    public void SetGridObject(Vector3 worldPos, TMyGridObject2D value)
    {
        int x = 0;
        int y = 0;
        GetXY(worldPos, out x, out y);
        SetGridObject(x, y, value);
    }
    public TMyGridObject2D GetGridObject(int x, int y)
    {
        if (InGridTable(x, y))
        {
            Debug.Log(x + "---" + y + "---" + gridArray[x, y]);
            return gridArray[x, y];
        }
        else
        {
            Debug.Log("Grid Dısındasın : " + x + "---" + y + "---" + default(TMyGridObject2D));
            return default(TMyGridObject2D);
        }
    }
    public TMyGridObject2D GetGridObject(Vector3 worldPos)
    {
        int x = 0;
        int y = 0;
        GetXY(worldPos, out x, out y);
        return GetGridObject(x, y);
    }
}