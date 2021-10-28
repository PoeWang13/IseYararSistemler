using UnityEngine;

public class TrailEffect_GameObject : PoolObje
{
    [Header("Script Atamaları")]
    public bool effectStart = false;
    public int effectTime = 1;

    private void Update()
    {
        if (effectStart)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale,
                new Vector3(0.01f, 0.01f, 0.01f), Time.deltaTime / effectTime);
            //new Vector3(0.01f, 0.01f, 0.01f), Time.deltaTime * (1.0f / effectTime));
            if (transform.localScale.x < 0.02f)
            {
                effectStart = false;
                havuzum.ObjeyiHavuzaYerlestir(this);
            }
        }
    }
    public void SetEffect(int zaman = 1)
    {
        transform.localScale = Vector3.one;
        effectStart = true;
        if (zaman != 1)
        {
            effectTime = zaman;
        }
    }
}