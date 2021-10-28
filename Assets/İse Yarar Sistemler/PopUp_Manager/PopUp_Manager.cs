using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class PopUp
{
    public string title = "My Title";
    public string message = "My Message";
    public float fadeInDuration = 0.25f;
    public Color pozitifButtonColor = Color.white;
    public string pozitifButtonTextString = "Yes";
    public UnityAction pozitifUnityAction = null;
    public Color negatifButtonColor = Color.white;
    public string negatifButtonTextString = "No";
    public UnityAction negatifUnityAction = null;
}
public class PopUp_Manager : MonoSingletion<PopUp_Manager>
{
    [Header("Genel Script Atamaları")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject genelPopUpPanel;

    private bool isActive = false;
    private PopUp popUp = new PopUp();
    private PopUp myPopUp = new PopUp();
    private Queue<PopUp> popUps = new Queue<PopUp>();

    [Header("Pozitif Button Atamaları")]
    [SerializeField] private Button pozitifButton;
    [SerializeField] private Image pozitifButtonImage;
    [SerializeField] private TextMeshProUGUI pozitifButtonText;

    [Header("Negatif Button Atamaları")]
    [SerializeField] private Button negatifButton;
    [SerializeField] private Image negatifButtonImage;
    [SerializeField] private TextMeshProUGUI negatifButtonText;

    [Header("Negatif Button Atamaları")]
    [SerializeField] private GameObject slotGameObject;
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI slotAmountText;

    private IEnumerator FadeTimer;
    private void Start()
    {
        FadeTimer = FadeTime(myPopUp.fadeInDuration);
        pozitifButton.onClick.AddListener(PopUpPozitifAnswer);
        negatifButton.onClick.AddListener(PopUpNegatifAnswer);
    }
    public PopUp_Manager SetTitle(string title)
    {
        myPopUp.title = title;
        return Instance;
    }
    public PopUp_Manager SetMessage(string message)
    {
        myPopUp.message = message;
        return Instance;
    }
    public PopUp_Manager SetFadeInDuration(float duration)
    {
        myPopUp.fadeInDuration = duration;
        return Instance;
    }
    private IEnumerator FadeTime(float duration)
    {
        float startingTime = Time.time;
        float alphaTime = 0.0f;
        while (alphaTime < 1)
        {
            alphaTime = Mathf.Lerp(0.0f, 1.0f, (Time.time - startingTime) / duration);
            canvasGroup.alpha = alphaTime;
            yield return null;
        }
    }
    public PopUp_Manager ItemSlot(Sprite item, string slotAmount)
    {
        slotGameObject.SetActive(true);
        slotImage.sprite = item;
        slotAmountText.text = slotAmount;
        return Instance;
    }
    public PopUp_Manager SetPozitifButtonText(string pozitifText)
    {
        myPopUp.pozitifButtonTextString = pozitifText;
        return Instance;
    }
    public PopUp_Manager SetNegatifButtonText(string negatifText)
    {
        myPopUp.negatifButtonTextString = negatifText;
        return Instance;
    }
    public PopUp_Manager SetPozitifButtonActiver(bool isActive)
    {
        pozitifButton.gameObject.SetActive(isActive);
        return Instance;
    }
    public PopUp_Manager SetNegatifButtonActiver(bool isActive)
    {
        negatifButton.gameObject.SetActive(isActive);
        return Instance;
    }
    public PopUp_Manager SetPozitifButtonColor(Color pozitifColor)
    {
        myPopUp.pozitifButtonColor = pozitifColor;
        return Instance;
    }
    public PopUp_Manager SetNegatifButtonColor(Color negatifColor)
    {
        myPopUp.negatifButtonColor = negatifColor;
        return Instance;
    }
    public PopUp_Manager SetPozitifAction(UnityAction pozitifAction)
    {
        myPopUp.pozitifUnityAction = pozitifAction;
        return Instance;
    }
    public PopUp_Manager SetNegatifAction(UnityAction negatifAction)
    {
        myPopUp.negatifUnityAction = negatifAction;
        return Instance;
    }
    private void PopUpPozitifAnswer()
    {
        popUp.pozitifUnityAction?.Invoke();

        PopUpPanelSakla();
    }
    private void PopUpNegatifAnswer()
    {
        popUp.negatifUnityAction?.Invoke();

        PopUpPanelSakla();
    }
    public void PopUpPanelGoster()
    {
        popUps.Enqueue(myPopUp);
        // Temizle Herşeyi
        myPopUp = new PopUp();
        if (!isActive)
        {
            SiradakiPopUpGoster();
        }
    }
    private void SiradakiPopUpGoster()
    {
        popUp = popUps.Dequeue();

        titleText.text = popUp.title;
        messageText.text = popUp.message;
        pozitifButtonImage.color = popUp.pozitifButtonColor;
        negatifButtonImage.color = popUp.negatifButtonColor;
        pozitifButtonText.text = popUp.pozitifButtonTextString;
        negatifButtonText.text = popUp.negatifButtonTextString;

        genelPopUpPanel.SetActive(true);
        isActive = true;
        StartCoroutine(FadeTime(popUp.fadeInDuration));
    }
    private void PopUpPanelSakla()
    {
        isActive = false;
        StopCoroutine(FadeTimer);
        genelPopUpPanel.SetActive(false);
        slotGameObject.SetActive(false);
        if (popUps.Count > 0)
        {
            SiradakiPopUpGoster();
        }
    }
}