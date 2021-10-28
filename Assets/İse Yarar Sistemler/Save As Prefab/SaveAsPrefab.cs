using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SaveAsPrefab : MonoBehaviour
{
    /// <summary>
    /// Bu fonksiyonu Editor zamanında prefab yapmak için kullanabilirsin.
    /// yollar => Prefabın konulmasını istediğiniz klasöre kadarki klasör isimleri.
    /// obj => Prefab yapılacak obje.
    /// </summary>
    public void SaveLikePrefab(List<string> yollar, GameObject obj)
    {
        string path = "Assets/";
        for (int e = 0; e < yollar.Count; e++)
        {
            path += yollar[e] + "/";
        }
        path += obj.name + ".prefab";
        path = AssetDatabase.GenerateUniqueAssetPath(path);

        PrefabUtility.SaveAsPrefabAssetAndConnect(obj, path, InteractionMode.UserAction);
    }
}