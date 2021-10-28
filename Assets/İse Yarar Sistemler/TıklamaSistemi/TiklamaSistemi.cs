using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TiklamaSistemi : MonoSingletion<TiklamaSistemi>
{
    [Header("Script Atamaları")]
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI mesajText;
    [SerializeField] private Image keyImage;
    [SerializeField] private TextMeshProUGUI keySpriteText;
    [SerializeField] private Image clickDurumImage;
    [SerializeField] private KeyCode clickKey;
    [SerializeField] private int click_Adet = 1;
    [SerializeField] private float clickDelete_Time = 1;
    private float click_AdetNext;
    private float clickDelete_TimeNext;
    private UnityAction pozitifUnityAction = null;
    private UnityAction negaticUnityAction = null;
    private void Update()
    {
        clickDelete_TimeNext += Time.deltaTime;
        if (clickDelete_TimeNext >= clickDelete_Time)
        {
            clickDelete_TimeNext = 0;
            click_Adet--;
            if (click_Adet <= 0)
            {
                click_Adet = 0;
                NegatifDurum();
            }
            clickDurumImage.fillAmount = 1.0f * click_AdetNext / click_Adet;
        }
        if (Input.GetKeyDown(clickKey))
        {
            click_AdetNext++;
            clickDurumImage.fillAmount = 1.0f * click_AdetNext / click_Adet;
            if (click_AdetNext >= click_Adet)
            {
                // Basari
                PozitifDurum();
            }
        }
    }
    public TiklamaSistemi SetTitle(string title)
    {
        clickPanel.SetActive(true);
        titleText.text = title;
        return Instance;
    }
    public TiklamaSistemi SetMessage(string message)
    {
        mesajText.text = message;
        return Instance;
    }
    public TiklamaSistemi SetKeySprite(Sprite keySprite)
    {
        keyImage.sprite = keySprite;
        return Instance;
    }
    public TiklamaSistemi SetKeySpriteMesaj(string keySpriteMesaj)
    {
        keySpriteText.text = keySpriteMesaj;
        return Instance;
    }
    public TiklamaSistemi SetKeyTiklamaStart_Finish(int clickAdetStart, int clickAdetFinish, float clickDeleteTime)
    {
        click_AdetNext = clickAdetStart;
        click_Adet = clickAdetFinish;
        clickDelete_Time = clickDeleteTime;
        clickDurumImage.fillAmount = 1.0f * click_AdetNext / click_Adet;
        clickDelete_TimeNext = 0;
        return Instance;
    }
    public TiklamaSistemi SetKeyTiklamaPozitif_Negatif(UnityAction pozitifAction, UnityAction negatifAction)
    {
        pozitifUnityAction = pozitifAction;
        negaticUnityAction = negatifAction;
        return Instance;
    }
    private void PozitifDurum()
    {
        pozitifUnityAction?.Invoke();
        ClickSystemClose();
    }
    private void NegatifDurum()
    {
        negaticUnityAction?.Invoke();
        ClickSystemClose();
    }
    private void ClickSystemClose()
    {
        pozitifUnityAction = null;
        negaticUnityAction = null;
        click_AdetNext = 0;
        clickDelete_TimeNext = 0;
        clickDurumImage.fillAmount = 0;
        clickPanel.SetActive(false);
    }
}