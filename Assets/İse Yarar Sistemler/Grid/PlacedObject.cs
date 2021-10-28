using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlacedObject : MonoBehaviour
{
    [Header("Script Atamaları")]
    private Buildler build;
    private Vector2Int origin;
    private Buildler.Dir dir;
    public static PlacedObject CreateBuild(Vector3 worldPos, Vector2Int origin, Buildler.Dir dir, Buildler buildler)
    {
        Transform build = Instantiate(buildler.prefab, worldPos,
                    Quaternion.Euler(0, buildler.GetRotationAngle(dir), 0));
        PlacedObject placedObject = build.GetComponent<PlacedObject>();
        placedObject.build = buildler;
        placedObject.origin = origin;
        placedObject.dir = dir;
        return placedObject;
    }
    public static Transform CreateHayaliBuild(Vector3 worldPos, Buildler.Dir dir, Buildler buildler)
    {
        Transform build = Instantiate(buildler.goruntu, worldPos,
                    Quaternion.Euler(0, buildler.GetRotationAngle(dir), 0));
        return build;
    }
    public List<Vector2Int> GetGridPositionList()
    {
        return build.GetGridPositionList(origin, dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}