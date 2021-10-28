using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridBuildSystem3D : MonoBehaviour
{
    private MyGrid3D<GridObject3DBasic> gridBasic;
    private Buildler.Dir buildlerDir = Buildler.Dir.down;
    [Header("Script Atamaları")]
    [SerializeField] private int gridWeidgt = 10;
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private int gridCellSize = 1;
    [SerializeField] private int speed = 50;
    [SerializeField] private List<Buildler> testBuildler;
    [SerializeField] private Buildler testBuild;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform maskObject;
    [SerializeField] private Transform hayaliObject;
    private List<Vector2Int> gridPozisyons = new List<Vector2Int>();
    private Vector2Int rotationOffSet;
    private Vector3 buildWorldPosition;
    private Vector3Int targetPointInt = Vector3Int.zero;
    private bool canBuild = true;
    private MeshRenderer[] mR;
    private Vector2Int buildOffSet = Vector2Int.zero;
    private void Start()
    {
        testBuild = testBuildler[1];
        gridBasic = new MyGrid3D<GridObject3DBasic>(gridWeidgt, gridHeight, gridCellSize, Vector3.zero, (MyGrid3D<GridObject3DBasic> g, int x, int y) => new GridObject3DBasic(g, x, y), 2.9f, 2.9f);
    }
    private void Update()
    {
        if (hayaliObject != null)
        {
            // Mouse pozisyonunu öğren
            Vector3 targetPoint = StaticFonksiyon.CanHitSomeThing3D(mask).point;

            // Mouse pozisyonunun Grid karşılığını öğren
            // Bina konacak yerin ilk noktasını öğren
            gridBasic.GetXZ(targetPoint, out int x, out int z);
            gridPozisyons = testBuild.GetGridPositionList(new Vector2Int(x, z), buildlerDir);
            rotationOffSet = testBuild.GetRotationOffSet(buildlerDir);
            buildWorldPosition = gridBasic.GetWorldPosition(x, z) +
                new Vector3(rotationOffSet.x, 0, rotationOffSet.y) * gridBasic.GetCellSize();

            // Mouse pozisyonunun grid karşılığı değiştimi
            if (targetPointInt.x != buildWorldPosition.x)
            {
                targetPointInt.x = (int)buildWorldPosition.x;
                buildOffSet.x = x;
            }
            else if (targetPointInt.z != buildWorldPosition.z)
            {
                targetPointInt.z = (int)buildWorldPosition.z;
                buildOffSet.y = z;
            }
            hayaliObject.position = Vector3.MoveTowards(hayaliObject.position, targetPointInt, Time.deltaTime * speed);

            canBuild = true;
            for (int e = 0; e < gridPozisyons.Count && canBuild; e++)
            {
                if (gridPozisyons[e].x >= gridWeidgt || gridPozisyons[e].y >= gridHeight || !gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).CanPlacedObjectBuild())
                {
                    // You can't build here
                    canBuild = false;
                    Debug.Log(canBuild);
                }
            }
            for (int e = 0; e < mR.Length; e++)
            {
                Color color = Color.white;
                if (!canBuild)
                {
                    // Materyallerini uyari yap
                    color = Color.red;
                    color.a = 0.5f;
                }
                else
                {
                    // Materyallerini uyari yap
                    color.a = 1f;
                }
                mR[e].material.color = color;
            }
        }
        else
        {
            maskObject.position = StaticFonksiyon.CanHitSomeThing3D(mask).point;
        }
        // Hayali bina
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateHayaliBuild();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (hayaliObject == null)
            {
                return;
            }

            // Can Build
            if (canBuild)
            {
                PlacedObject placedObject = PlacedObject.CreateBuild(buildWorldPosition, buildOffSet, buildlerDir, testBuild);

                Debug.Log(buildOffSet.x + "---" + buildOffSet.y);
                for (int e = 0; e < gridPozisyons.Count; e++)
                {
                    gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).SetPlacedObjectBuild(placedObject);
                }
                if (hayaliObject != null)
                {
                    Destroy(hayaliObject.gameObject);
                }
            }
            else
            {
                Debug.Log("You can't build here.");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            GridObject3DBasic gridObject3DBasic = gridBasic.GetGridObject(StaticFonksiyon.CanHitSomeThing3D(mask).point);
            PlacedObject placedObject = gridObject3DBasic.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPozisyons = placedObject.GetGridPositionList();
                for (int e = 0; e < gridPozisyons.Count; e++)
                {
                    gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).ClearPlacedObjectBuild();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (hayaliObject != null)
            {
                Destroy(hayaliObject.gameObject);
                buildlerDir = Buildler.GetNextDir(buildlerDir);
                CreateHayaliBuild();
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (hayaliObject != null)
            {
                Destroy(hayaliObject.gameObject);
            }
        }
    }
    private void Update2()
    {
        if (hayaliObject != null)
        {
            // Mouse pozisyonunu öğren
            Vector3 targetPoint = StaticFonksiyon.CanHitSomeThing3D(mask).point;

            // Mouse pozisyonunun Grid karşılığını öğren
            // Bina konacak yerin ilk noktasını öğren
            gridBasic.GetXZ(targetPoint, out int x, out int z);
            List<Vector2Int> gridPozisyons = testBuild.GetGridPositionList(new Vector2Int(x, z), buildlerDir);
            rotationOffSet = testBuild.GetRotationOffSet(buildlerDir);
            buildWorldPosition = gridBasic.GetWorldPosition(x, z) +
                new Vector3(rotationOffSet.x, 0, rotationOffSet.y) * gridBasic.GetCellSize();

            // Mouse pozisyonunun grid karşılığı değiştimi
            if (targetPointInt.x != buildWorldPosition.x)
            {
                targetPointInt.x = (int)buildWorldPosition.x;
            }
            else if (targetPointInt.z != buildWorldPosition.z)
            {
                targetPointInt.z = (int)buildWorldPosition.z;
            }
            hayaliObject.position = Vector3.MoveTowards(hayaliObject.position, targetPointInt, Time.deltaTime * speed);

            canBuild = true;
            for (int e = 0; e < gridPozisyons.Count && canBuild; e++)
            {
                if (gridPozisyons[e].x >= gridWeidgt || gridPozisyons[e].y >= gridHeight || !gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).CanPlacedObjectBuild())
                {
                    // You can't build here
                    canBuild = false;
                }
            }
            for (int e = 0; e < mR.Length; e++)
            {
                Color color = Color.white;
                if (!canBuild)
                {
                    // Materyallerini uyari yap
                    color = Color.red;
                    color.a = 0.5f;
                }
                else
                {
                    // Materyallerini uyari yap
                    color.a = 1f;
                }
                mR[e].material.color = color;
            }
        }
        else
        {
            maskObject.position = StaticFonksiyon.CanHitSomeThing3D(mask).point;
        }
        // Hayali bina
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateHayaliBuild();
        }
        if (Input.GetMouseButtonDown(0))
        {
            gridBasic.GetXZ(StaticFonksiyon.CanSomeThingHitPoint(mask), out int x, out int z);
            List<Vector2Int> gridPozisyons = testBuild.GetGridPositionList(new Vector2Int(x, z), buildlerDir);

            // Can Build
            canBuild = true;
            for (int e = 0; e < gridPozisyons.Count && canBuild; e++)
            {
                if (gridPozisyons[e].x >= gridWeidgt || gridPozisyons[e].y >= gridHeight || !gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).CanPlacedObjectBuild())
                {
                    // You can't build here
                    canBuild = false;
                }
            }
            if (canBuild)
            {
                rotationOffSet = testBuild.GetRotationOffSet(buildlerDir);
                buildWorldPosition = gridBasic.GetWorldPosition(x, z) +
                    new Vector3(rotationOffSet.x, 0, rotationOffSet.y) * gridBasic.GetCellSize();
                PlacedObject placedObject = PlacedObject.CreateBuild(buildWorldPosition, new Vector2Int(x, z), buildlerDir, testBuild);

                Debug.Log(x + "---" + z);
                for (int e = 0; e < gridPozisyons.Count; e++)
                {
                    gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).SetPlacedObjectBuild(placedObject);
                }
                if (hayaliObject != null)
                {
                    Destroy(hayaliObject.gameObject);
                }
            }
            else
            {
                Debug.Log("You can't build here.");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            GridObject3DBasic gridObject3DBasic = gridBasic.GetGridObject(StaticFonksiyon.CanHitSomeThing3D(mask).point);
            PlacedObject placedObject = gridObject3DBasic.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPozisyons = placedObject.GetGridPositionList();
                for (int e = 0; e < gridPozisyons.Count; e++)
                {
                    gridBasic.GetGridObject(gridPozisyons[e].x, gridPozisyons[e].y).ClearPlacedObjectBuild();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (hayaliObject != null)
            {
                Destroy(hayaliObject.gameObject);
                buildlerDir = Buildler.GetNextDir(buildlerDir);
                CreateHayaliBuild();
            }
        }
    }
    private void CreateHayaliBuild()
    {
        if (hayaliObject != null)
        {
            Destroy(hayaliObject.gameObject);
        }
        // Bina konacak yerin ilk noktasını öğren
        gridBasic.GetXZ(StaticFonksiyon.CanSomeThingHitPoint(mask), out int x, out int z);
        // Hayali binayı kurduk. Bu yüzden açısını ve alanını belirle ve hayali binayı kur.
        rotationOffSet = testBuild.GetRotationOffSet(buildlerDir);
        buildWorldPosition = gridBasic.GetWorldPosition(x, z) +
            new Vector3(rotationOffSet.x, 0, rotationOffSet.y) * gridBasic.GetCellSize();

        hayaliObject = PlacedObject.CreateBuild(buildWorldPosition, new Vector2Int(x, z), buildlerDir, testBuild).transform;
        mR = hayaliObject.GetComponentsInChildren<MeshRenderer>();
    }
}