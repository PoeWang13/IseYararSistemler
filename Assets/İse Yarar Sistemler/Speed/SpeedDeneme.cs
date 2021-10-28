using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeScaleler
{
    private static float genelTimeScale = 1;
    private static float playerTimeScale = 1;
    private static float enemyTimeScale = 1;
    private static float esyaTimeScale = 1;
    private static float ozelTimeScale = 1;

    #region Set All Time Scales
    public static void GenelTimeScale(float genel)
    {
        genelTimeScale = genel;
    }
    public static void PlayerTimeScale(float player)
    {
        playerTimeScale = player;
    }
    public static void EnemyTimeScale(float enemy)
    {
        enemyTimeScale = enemy;
    }
    public static void EsyaTimeScale(float esya)
    {
        esyaTimeScale = esya;
    }
    public static void OzelTimeScale(float ozel)
    {
        ozelTimeScale = ozel;
    }
    #endregion

    #region Return All Time Scales
    public static float GenelTimeScale()
    {
        return genelTimeScale;
    }
    public static float PlayerTimeScale()
    {
        return playerTimeScale * genelTimeScale;
    }
    public static float EnemyTimeScale()
    {
        return enemyTimeScale * genelTimeScale;
    }
    public static float EsyaTimeScale()
    {
        return esyaTimeScale * genelTimeScale;
    }
    public static float OzelTimeScale()
    {
        return ozelTimeScale * genelTimeScale;
    } 
    #endregion
}
[System.Serializable]
public class TimeScaleObje
{
    public string timeScaleObjeName;
    public float timeScale = 1;
    public Transform genelTransform;
    public Animator genelAnimator;
}
public class SpeedDeneme : MonoBehaviour
{
    public bool onValid;
    public Transform genelObje;
    private void OnValidate()
    {
        if (onValid)
        {
            onValid = false;
            objeTimeScaleler.Clear();
            for (int e = 0; e < genelObje.childCount; e++)
            {
                TimeScaleObje newObje = new TimeScaleObje();
                newObje.timeScaleObjeName = genelObje.GetChild(e).name;
                newObje.timeScale = 1;
                newObje.genelTransform = genelObje.GetChild(e).transform;
                newObje.genelAnimator = genelObje.GetChild(e).GetComponent<Animator>(); ;
                objeTimeScaleler.Add(newObje);
            }
        }
    }
    [Header("Script Atamaları")]
    public int speed = 5;
    public List<TimeScaleObje> objeTimeScaleler = new List<TimeScaleObje>();
    private void Start()
    {
        TimeScaleler.GenelTimeScale(objeTimeScaleler[0].timeScale);
        TimeScaleler.PlayerTimeScale(objeTimeScaleler[1].timeScale);
        TimeScaleler.EnemyTimeScale(objeTimeScaleler[2].timeScale);
        TimeScaleler.EsyaTimeScale(objeTimeScaleler[3].timeScale);
        TimeScaleler.OzelTimeScale(objeTimeScaleler[4].timeScale);

        objeTimeScaleler[0].genelAnimator.SetFloat("AnimSpeed", TimeScaleler.GenelTimeScale());
        objeTimeScaleler[1].genelAnimator.SetFloat("AnimSpeed", TimeScaleler.PlayerTimeScale());
        objeTimeScaleler[2].genelAnimator.SetFloat("AnimSpeed", TimeScaleler.EnemyTimeScale());
        objeTimeScaleler[3].genelAnimator.SetFloat("AnimSpeed", TimeScaleler.EsyaTimeScale());
        objeTimeScaleler[4].genelAnimator.SetFloat("AnimSpeed", TimeScaleler.OzelTimeScale());
    }
    private void Update()
    {
        objeTimeScaleler[0].genelTransform.Translate(Time.deltaTime * TimeScaleler.GenelTimeScale() * Vector3.right * speed);
        objeTimeScaleler[1].genelTransform.Translate(Time.deltaTime * TimeScaleler.PlayerTimeScale() * Vector3.right * speed);
        objeTimeScaleler[2].genelTransform.Translate(Time.deltaTime * TimeScaleler.EnemyTimeScale() * Vector3.right * speed);
        objeTimeScaleler[3].genelTransform.Translate(Time.deltaTime * TimeScaleler.EsyaTimeScale() * Vector3.right * speed);
        objeTimeScaleler[4].genelTransform.Translate(Time.deltaTime * TimeScaleler.OzelTimeScale() * Vector3.right * speed);
    }
}