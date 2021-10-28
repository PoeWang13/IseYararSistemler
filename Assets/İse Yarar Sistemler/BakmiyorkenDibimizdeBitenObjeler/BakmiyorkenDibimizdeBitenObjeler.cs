using UnityEngine;

public class BakmiyorkenDibimizdeBitenObjeler : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Transform targetObject;

    private void OnBecameInvisible()
    {
        transform.position = targetObject.position - targetObject.forward;
        Debug.Log("OnBecameInvisible");
        Vector3 lookPos = targetObject.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}