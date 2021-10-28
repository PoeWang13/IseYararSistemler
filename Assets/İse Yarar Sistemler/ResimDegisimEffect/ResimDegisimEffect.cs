using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResimDegisimEffect : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int resimSayisi = 1;
    [SerializeField] private Image resimKucuk, resimBuyuk, resimFull;
    [SerializeField] private List<Sprite> resimler = new List<Sprite>();

    private void Update()
    {
        resimKucuk.fillAmount += Time.deltaTime;
        resimBuyuk.fillAmount += Time.deltaTime;
        if (resimBuyuk.fillAmount >= 1)
        {
            resimSayisi++;
            resimSayisi = resimSayisi == resimler.Count ? 0 : resimSayisi;

            resimKucuk.fillAmount = 0;
            resimBuyuk.fillAmount = 0.5f;

            resimFull.sprite = resimBuyuk.sprite;
            resimBuyuk.sprite = resimKucuk.sprite;
            resimKucuk.sprite = resimler[resimSayisi];
        }
    }
}