using UnityEngine;
using UnityEngine.Events;

public class LifeBehaviour : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int myLife;
    [SerializeField] private UnityEvent enterAction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Bullet power kadar life düs.
            myLife--;
            if (myLife <= 0)
            {
                enterAction?.Invoke();
            }
        }
    }
}