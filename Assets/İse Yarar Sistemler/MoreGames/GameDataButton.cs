using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameDataButton : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private Button gameNameButton;
    [SerializeField] private RawImage gameNameImage;
    private List<Texture> gameSprites = new List<Texture>();
    private float waitSpriteTime = 1;
    private float waitSpriteTimeNext;
    private int waitNextSprite;
    private bool canChangeSprite = false;

    public void SetGameButton(string gameName, string gameUrl)
    {
        gameNameText.text = gameName;
        gameNameButton.onClick.AddListener(() => GoGameLink(gameUrl));
    }
    public void AddGameSprite(Texture gameTexture)
    {
        gameSprites.Add(gameTexture);
        if (gameSprites.Count == 1)
        {
            canChangeSprite = true;
        }
    }
    private void Update()
    {
        if (!canChangeSprite)
        {
            return;
        }
        waitSpriteTimeNext += Time.deltaTime;
        if (waitSpriteTimeNext >= waitSpriteTime)
        {
            waitSpriteTimeNext = 0;
            waitSpriteTime = Random.Range(0, 3);
            waitNextSprite++;
            waitNextSprite = waitNextSprite == gameSprites.Count ? 0 : waitNextSprite;
            gameNameImage.texture = gameSprites[waitNextSprite];
        }
    }
    private void GoGameLink(string url)
    {
        Application.OpenURL(url);
    }
}