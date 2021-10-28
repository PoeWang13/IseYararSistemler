using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YuvarlakButton : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Transform butonObject;
    [SerializeField] private Image dolumImage;
    [SerializeField] private TextMeshProUGUI valueText;
    private Vector3 mousePozisyon;
    public void OnDragButton()
    {
        mousePozisyon = Input.mousePosition;
        Vector2 dir = mousePozisyon - butonObject.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;
        if (angle <= 225 || angle >= 315)
        {
            Quaternion r = Quaternion.AngleAxis(angle + 135, Vector3.forward);
            butonObject.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            dolumImage.fillAmount = 0.75f - (angle / 360.0f);
            valueText.text = Mathf.Round((dolumImage.fillAmount * 100) / 0.75f).ToString();
        }
    }
    public void OnBeginDropButton()
    {
        dolumImage.fillAmount = 0;
        valueText.text = 0.ToString();
    }
}