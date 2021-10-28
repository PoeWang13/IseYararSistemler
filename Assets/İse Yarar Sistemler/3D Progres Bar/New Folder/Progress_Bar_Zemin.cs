using UnityEngine;

public class Progress_Bar_Zemin : MonoBehaviour
{
    [Header("Script Atamaları")]
    public Transform progressBar;
    public float progressScaleOranTime;
    private bool progressBasladi = false;
    private bool progressKullanıldı = false;
    private bool progressKullanılmıyor = true;
    private float progressScaleOran;
    private Vector3 progressScale = new Vector3(1, 0, 1);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progressKullanıldı)
            {
                return;
            }
            progressBasladi = true;
            progressKullanılmıyor = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (progressKullanıldı)
            {
                return;
            }
            progressBasladi = false;
        }
    }
    private void Update()
    {
        if (progressKullanılmıyor)
        {
            return;
        }
        if (progressBasladi)
        {
            if (progressScaleOran < 1)
            {
                progressScaleOran += 1 / progressScaleOranTime * Time.deltaTime;
                progressScale.y = progressScaleOran;
                progressBar.localScale = progressScale;
            }
            else
            {
                // İşlem Başarılı
                progressKullanıldı = true;
                progressKullanılmıyor = true;
            }
        }
        else
        {
            if (progressScaleOran > 0)
            {
                progressScaleOran -= Time.deltaTime;
                progressScale.y = progressScaleOran;
                progressBar.localScale = progressScale;
            }
            else
            {
                // İşlem Tamamen bitti
                progressKullanılmıyor = true;
            }
        }
    }
}