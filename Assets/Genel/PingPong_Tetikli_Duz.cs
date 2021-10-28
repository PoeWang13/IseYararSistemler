using UnityEngine;

public class PingPong_Tetikli_Duz : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private float beklemeTime = 1;
    [SerializeField] private int pingPongSpeed = 125;
    [SerializeField] private Vector3 secondPos;
    [SerializeField] private Transform goruntu;
    private float beklemeTimeNext;
    private int direction = 1;
    private bool bekle;
    private Vector3 targetPos;
    private bool tetiklendi;
    public void SetTetikle()
    {
        tetiklendi = true;
    }
    private void Update()
    {
        if (!tetiklendi)
        {
            return;
        }
        if (bekle)
        {
            beklemeTimeNext += Time.deltaTime;
            if (beklemeTimeNext > beklemeTime)
            {
                beklemeTimeNext = 0;
                bekle = false;
            }
        }
        else
        {
            goruntu.localPosition = Vector3.MoveTowards(goruntu.localPosition, targetPos, Time.deltaTime * pingPongSpeed);
            if (Vector3.Distance(goruntu.localPosition, targetPos) < 0.05f)
            {
                bekle = true;
                if (direction == 1)
                {
                    targetPos = secondPos;
                    tetiklendi = false;
                }
                else
                {
                    targetPos = transform.position;
                }
                direction *= -1;
            }
        }
    }
}