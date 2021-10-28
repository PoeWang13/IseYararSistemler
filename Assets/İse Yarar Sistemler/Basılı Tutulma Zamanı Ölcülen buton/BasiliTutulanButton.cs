using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BasiliTutulanButton : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int count;
    [SerializeField] private bool canCount;
    [SerializeField] private TextMeshProUGUI valueText;
    public void OnDragButton()
    {
        if (canCount)
        {
            count++;
            valueText.text = count.ToString();
        }
    }
    public void OnBeginDropButton()
    {
        count = 0;
    }
    public void OnEnterButton()
    {
        canCount = true;
    }
    public void OnExitButton()
    {
        canCount = false;
    }
}