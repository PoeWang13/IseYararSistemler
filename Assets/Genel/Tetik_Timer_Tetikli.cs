using UnityEngine;
using UnityEngine.Events;

public class Tetik_Timer_Tetikli : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private float tetik_Time = 1;
    [SerializeField] private int tetik_Adet = 1;
    [SerializeField] private UnityEvent enterAction;
    private bool tetikBasla = true;
    private float tetik_TimeNext;
    public void SetTetik(int tetikAdet = 1)
    {
        tetik_Adet = tetikAdet;
        tetikBasla = true;
    }
    private void Update()
    {
        if (tetikBasla)
        {
            tetik_TimeNext += Time.deltaTime;
            if (tetik_TimeNext >= tetik_Time)
            {
                tetik_TimeNext = 0;
                tetik_Adet--;
                if (tetik_Adet == 0)
                {
                    tetikBasla = false;
                    enterAction?.Invoke();
                }
            }
        }
    }
}