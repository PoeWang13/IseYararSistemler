using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Progress_Bar_3D : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Material objMaterial;
    [SerializeField] private MeshRenderer matObj;
    [SerializeField] private float fillArea;

    private void Start()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        matObj.GetPropertyBlock(mpb);
        float progress = GetComponent<MeshFilter>().mesh.bounds.size.x / 2;
        matObj.material.SetFloat("Vector1_Border", progress);
        fillArea = -progress;
        matObj.material.SetFloat("Vector1_Rate", fillArea);
    }
    private void Update()
    {
        matObj.material.SetFloat("Vector1_Rate", fillArea);
    }
}