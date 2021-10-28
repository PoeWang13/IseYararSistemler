using UnityEngine;

public class SphereGizmos : MonoBehaviour
{
    public static bool isEnabled = true;
    [Header("Script Atamaları")]
    [SerializeField] private float size = 0.1f;
    [SerializeField] private Color gizmosColor = Color.red;

    private void OnDrawGizmos()
    {
        if (isEnabled)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawSphere(transform.position, size);
        }
    }
}