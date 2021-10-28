using UnityEngine;

public class TrailEffect_Manager : MonoBehaviour
{
    [Header("Script Atamaları")]
    public Pooler trailEffect_GameObject;
    private int effectObjectTime = 1;
    private float effectObjectCreateTime = 1;
    private bool effectStart = false;
    private float effectObjectTimeNext;

    public void SetEffectManager(bool effect, int effectObject, float effectObjectCreate)
    {
        effectStart = effect;
        effectObjectTime = effectObject;
        effectObjectCreateTime = effectObjectCreate;
        effectObjectTimeNext = 0;
    }
    private void Update()
    {
        if (effectStart)
        {
            effectObjectTimeNext += Time.deltaTime;
            if (effectObjectTimeNext > effectObjectCreateTime)
            {
                effectObjectTimeNext = 0;
                CreateEffectObject();
            }
        }
    }
    private void CreateEffectObject()
    {
        trailEffect_GameObject.HavuzdanObjeIste(transform.position).
            GetComponent<TrailEffect_GameObject>().SetEffect(effectObjectTime);
    }
}