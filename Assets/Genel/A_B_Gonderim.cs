using UnityEngine;

public class A_B_Gonderim : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Transform obje;
    [SerializeField] private int speed;
    [SerializeField] private Vector3 bNoktasi;
    private bool bitti = true;

    public void SendObje()
    {
        bitti = false;
    }
    private void Update()
    {
        if (!bitti)
        {
            obje.localPosition = Vector3.MoveTowards(obje.localPosition, bNoktasi, Time.deltaTime * speed);
            if (Vector3.Distance(obje.localPosition, bNoktasi) < 0.05f)
            {
                bitti = true;
            }
        }
    }
}