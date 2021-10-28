using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FortuneNo : EventArgs
{
    public int FortuneObjectNo;
}
public class FortuneSpinWheel : MonoBehaviour
{
    private void OnValidate()
    {
        if (oduller.Count != cemberObjeAmount)
        {
            oduller.Clear();
            for (int e = 0; e < cemberObjeAmount; e++)
            {
                oduller.Add("Odul " + (e + 1));
            }
        }
    }
    public EventHandler<FortuneNo> OnSpinBitti;
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform çarkıfelekParent;
    [SerializeField] private Image çarkıfelekOdulImage;
    [SerializeField] private CanvasRenderer cemberRenderer;
    [SerializeField] private Material cemberMaterial;
    [SerializeField] private int cemberOdulParcaUzunlugu = 500;

    [Header("Minumum 3 olmak zorunda yoksa objeler için çember oluşturamaz.")]
    [SerializeField, Range(3, 15)] private int cemberObjeAmount = 12;
    [Header("cemberObjeAmount ile aynı olmak zorunda yoksa ödül ismi uyuşmaz.")]
    public List<string> oduller = new List<string>();

    [SerializeField] private List<Color> renkler = new List<Color>();
    private float cemberDonmeSpeed = 10;
    private float cemberDonmeSpeedBaslangic = 1;
    private float cemberDonmeSpeedTurn = 50;
    private float cemberDonmeSpeedBaslangicNext = 1;
    private float cemberDonmeSpeedTurnNext = 50;
    private bool don;
    private float objeAcisi;
    private int cemberdeSecilenObje;
    private void Start()
    {
        cemberDonmeSpeedTurnNext = cemberDonmeSpeedTurn;
        cemberDonmeSpeedBaslangicNext = cemberDonmeSpeedBaslangic;
        objeAcisi = 360 * -1.0f / cemberObjeAmount;
        SetWheel();
    }
    private void Update()
    {
        if (don)
        {
            çarkıfelekParent.Rotate(new Vector3(0, 0, cemberDonmeSpeedTurn / cemberDonmeSpeedBaslangic));
            if (cemberDonmeSpeedTurn / cemberDonmeSpeedBaslangic > 5)
            {
                cemberDonmeSpeedBaslangic += Time.deltaTime;
            }
            else if (cemberDonmeSpeedTurn > 0)
            {
                cemberDonmeSpeedTurn -= cemberDonmeSpeed * Time.deltaTime;
            }
            else
            {
                cemberDonmeSpeedTurn = 0;
                don = false;
                // 35 derece diyelim
                float cemberinYeri = (çarkıfelekParent.eulerAngles.z + 360) % 360;
                // 35/30 = 1 yani 0 değil 1. eleman geldi
                cemberdeSecilenObje = (int)(cemberinYeri / objeAcisi);
                OnSpinBitti?.Invoke(this, new FortuneNo { FortuneObjectNo = cemberdeSecilenObje });
            }
        }
    }
    public void Donder()
    {
        cemberDonmeSpeedTurn = cemberDonmeSpeedTurnNext;
        cemberDonmeSpeedBaslangic = cemberDonmeSpeedBaslangicNext;
        don = true;
    }
    public void SetWheel()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = Quaternion.Euler(0, 0, objeAcisi * 0) * Vector3.up * cemberOdulParcaUzunlugu;
        vertices[2] = Quaternion.Euler(0, 0, objeAcisi * 1) * Vector3.up * cemberOdulParcaUzunlugu;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        cemberMaterial.color = renkler[0];
        cemberRenderer.SetMesh(mesh);
        cemberRenderer.SetMaterial(cemberMaterial, null);
        çarkıfelekOdulImage.color = renkler[0];
        çarkıfelekOdulImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = oduller[0];

        for (int e = 1; e < cemberObjeAmount; e++)
        {
            CanvasRenderer canRender = Instantiate(cemberRenderer, cemberRenderer.transform.parent);
            Image canRenderImage = Instantiate(çarkıfelekOdulImage, çarkıfelekOdulImage.transform.parent);
            canRenderImage.color = renkler[e];
            canRenderImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = oduller[e];
            mesh = new Mesh();

            vertices = new Vector3[3];
            uv = new Vector2[3];
            triangles = new int[3];

            vertices[0] = Vector3.zero;
            vertices[1] = Quaternion.Euler(0, 0, objeAcisi * 0) * Vector3.up * cemberOdulParcaUzunlugu;
            vertices[2] = Quaternion.Euler(0, 0, objeAcisi * 1) * Vector3.up * cemberOdulParcaUzunlugu;

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            canRender.SetMesh(mesh);
            Material mat = new Material(cemberMaterial);
            mat.color = renkler[e];
            canRender.SetMaterial(mat, null);
            canRender.transform.Rotate(0, 0, objeAcisi * e);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Welcome to Luck Wheel");
        }
    }
    IEnumerator Warning()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        warningPanel.SetActive(false);
    }
    public void SetWarning(string s)
    {
        warningText.text = s;
        StartCoroutine(Warning());
    }
}