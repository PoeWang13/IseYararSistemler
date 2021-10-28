using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class GameDatas
{
    public string GameName;
    public string GameUrl;
    public List<string> GameSprites = new List<string>();
}
[System.Serializable]
public class GameDataContainer
{
    public List<GameDatas> GameDatas;
}
public class GamesData : MonoBehaviour
{
    /// Nasıl yapılacak
    /// 1-Driveda oyun için yeni klasör aç ve ismini oyunun ismi yap
    /// 2-Oyunun resimlerini klasöre yükle
    /// 3-Resimlere sağ tıkla Bağlantıyı Al seçeneğini seç
    /// 4-Kısıtlanmış tikine tıkla ve seçeneği Bağlantıya Sahip Olan Herkes olarak değiştir.
    /// 5-Bağlantı linkini kopyala. Şöyle bir şey olması lazım
    /// https://drive.google.com/file/d/0B6kWpoVPKv9DVkFudENneU9QX2s/view?usp=sharing&resourcekey=0-Oley_7RdN9VXlu3DpM9CmA
    /// 6-file/d/Kod No/view? kısmındaki Kod No kısmını seç ve kopyala
    /// 7-Drivedaki More Game Data klasörü içindeki jsonu aç ve oyun için başka bir { } li kısım oluştur
    /// 8-Kısım içindekileri yeni oyuna göre doldur.
    /// 9-Resim için gerekli url kısmına 6. adımda kopyaladığın link kısımlarını yapıştır.

    [Header("Data Atamaları")]
    private string driveStartLink = "https://drive.google.com/uc?export=download&id=";
    private string driveJsonLink = "10yRQvUjHGANTPjJUEZNYcdhbTf33k6Vd";
    [Header("Script Atamaları")]
    [SerializeField] private GameDataButton gameDataButton;
    [SerializeField] private GameDataContainer gameDataStruct = new GameDataContainer();

    public void CloseCanvas(GameObject games)
    {
        games.SetActive(!games.activeSelf);
    }
    private void Awake()
    {
        StartCoroutine(GetGamesData(driveStartLink + driveJsonLink));
    }
    IEnumerator GetGamesData(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        Debug.Log("İstek yollandi.");

        yield return unityWebRequest.SendWebRequest();
        UnityWebRequest.Result result = unityWebRequest.result;
        if (result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Bilgi gelmedi." + unityWebRequest.error);
        }
        else
        {
            gameDataStruct = JsonUtility.FromJson<GameDataContainer>(unityWebRequest.downloadHandler.text);
        }
        unityWebRequest.Dispose();
        for (int e = 1; e < gameDataStruct.GameDatas.Count; e++)
        {
            GameDataButton gDB = Instantiate(gameDataButton, gameDataButton.transform.parent);
            gDB.SetGameButton(gameDataStruct.GameDatas[e].GameName, gameDataStruct.GameDatas[e].GameUrl);

            for (int h = 0; h < gameDataStruct.GameDatas[e].GameSprites.Count; h++)
            {
                StartCoroutine(GetSpriteData(driveStartLink + gameDataStruct.GameDatas[e].GameSprites[h], e));
            }
        }
        gameDataButton.SetGameButton(gameDataStruct.GameDatas[0].GameName, gameDataStruct.GameDatas[0].GameUrl);
        for (int h = 0; h < gameDataStruct.GameDatas[0].GameSprites.Count; h++)
        {
            StartCoroutine(GetSpriteData(driveStartLink + gameDataStruct.GameDatas[0].GameSprites[h], 0));
        }
    }
    IEnumerator GetSpriteData(string url, int order)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        unityWebRequest.downloadHandler = new DownloadHandlerTexture();

        yield return unityWebRequest.SendWebRequest();
        UnityWebRequest.Result result = unityWebRequest.result;
        if (result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Bilgi gelmedi." + unityWebRequest.error);
        }
        else
        {
            gameDataButton.transform.parent.GetChild(order).GetComponent<GameDataButton>()
                .AddGameSprite(DownloadHandlerTexture.GetContent(unityWebRequest));
        }
        unityWebRequest.Dispose();
    }
}