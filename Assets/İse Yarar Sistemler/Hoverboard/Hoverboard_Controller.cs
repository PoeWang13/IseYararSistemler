using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hoverboard_Controller : MonoBehaviour
{
    [Header("Script Atamaları")]
    public float multier;
    public float addForce, addTorque;
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private List<Transform> corners = new List<Transform>();
    private RaycastHit[] raycastHit = new RaycastHit[4];
    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            float guc = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            myRigidbody.AddForceAtPosition(transform.up * guc * multier, anchor.position , ForceMode.Acceleration);
        }
    }
    private void FixedUpdate()
    {
        for (int e = 0; e < corners.Count; e++)
        {
            ApplyForce(corners[e], raycastHit[e]);
        }
        myRigidbody.AddForce(Input.GetAxis("Vertical") * addForce * transform.forward);
        myRigidbody.AddTorque(Input.GetAxis("Horizontal") * addTorque * transform.up);
    }
}