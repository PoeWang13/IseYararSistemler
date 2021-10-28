using UnityEngine;
using UnityEngine.Events;

public class Tetik_Timer_Surekli : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private float tetik_Time = 1;
    [SerializeField] private int tetik_Adet = 1;
    [SerializeField] private UnityEvent enterAction;

    private int tetik_AdetNext;
    private float tetik_TimeNext;
    private void Update()
    {
        tetik_TimeNext += Time.deltaTime;
        if (tetik_TimeNext >= tetik_Time)
        {
            tetik_TimeNext = 0;
            tetik_AdetNext++;
            if (tetik_AdetNext == tetik_Adet)
            {
                tetik_AdetNext = 0;
                enterAction?.Invoke();
            }
        }
    }
}