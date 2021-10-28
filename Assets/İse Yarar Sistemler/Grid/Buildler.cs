using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Build", fileName = "Build-X")]
public class Buildler : ScriptableObject
{
    public enum Dir
    {
        up,
        right,
        down,
        left,
    }
    [Header("Object Atamaları")]
    public string buildName;
    public int width;
    public int height;
    public Transform prefab;
    public Transform goruntu;
    public Material orjMaterial;

    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            case Dir.up:
                return Dir.right;
            case Dir.right:
                return Dir.down;
            case Dir.down:
                return Dir.left;
            case Dir.left:
                return Dir.up;
        }
        return dir;
    }
    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            case Dir.up:
                return 180;
            case Dir.right:
                return 270;
            case Dir.down:
                return 0;
            case Dir.left:
                return 90;
        }
        return 0;
    }
    public Vector2Int GetRotationOffSet(Dir dir)
    {
        switch (dir)
        {
            case Dir.up:
                return new Vector2Int(width, height);
            case Dir.right:
                return new Vector2Int(height, 0);
            case Dir.down:
                return new Vector2Int(0, 0);
            case Dir.left:
                return new Vector2Int(0, width);
        }
        return Vector2Int.zero;
    }
    public List<Vector2Int> GetGridPositionList(Vector2Int offSet, Dir dir)
    {
        List<Vector2Int> gridPozisyons = new List<Vector2Int>();
        switch (dir)
        {
            default:
            case Dir.up:
            case Dir.down:
                for (int e = 0; e < width; e++)
                {
                    for (int h = 0; h < height; h++)
                    {
                        gridPozisyons.Add(offSet + new Vector2Int(e, h));
                    }
                }
                break;
            case Dir.right:
            case Dir.left:
                for (int e = 0; e < height; e++)
                {
                    for (int h = 0; h < width; h++)
                    {
                        gridPozisyons.Add(offSet + new Vector2Int(e, h));
                    }
                }
                break;
        }
        return gridPozisyons;
    }
}