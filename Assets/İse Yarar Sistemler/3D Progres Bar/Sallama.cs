using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Sallama : MonoBehaviour
{
    [Header("Script Atamaları")]
    private Renderer rend;
    private Vector3 lastPos;
    private Vector3 velocity;
    private Vector3 lastRot;
    private Vector3 angularVelocity;
    public float maxWobble = 0.01f;
    public float wobbleSpeed = 1.0f;
    public float recovery = 1.0f;
    private float wobbleAmountX;
    private float wobbleAmountZ;
    private float wobbleAmountToAddX;
    private float wobbleAmountToAddZ;
    private float pulse;
    private float time = 0.5f;
    [SerializeField] private float fillArea;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("Vector1_Scale_Y_Min", -transform.parent.localScale.y);
        rend.material.SetFloat("Vector1_Scale_Y_Max", transform.parent.localScale.y);
    }
    private void Update()
    {
        // Set Fill_Rate
        rend.material.SetFloat("Vector1_Fill_Rate", fillArea);

        time += Time.deltaTime;
        // decrease wobble over time
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX,0,Time.deltaTime * (recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ,0,Time.deltaTime * (recovery));

        // make a sine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * wobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        // send it to the shader
        rend.material.SetFloat("Vector1_Sallama_X", wobbleAmountX);
        rend.material.SetFloat("Vector1_Sallama_Z", wobbleAmountZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;

        // add clamped velocity to wobble
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        // keep last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }
}