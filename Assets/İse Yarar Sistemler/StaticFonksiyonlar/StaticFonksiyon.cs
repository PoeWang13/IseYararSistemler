using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class StaticFonksiyon : MonoBehaviour
{
    ///// <summary>
    ///// Ekranda TextMesh olusturur. Yaz� yazari yerini ayarlar, renk verir
    ///// </summary>
    //public static TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPos = default(Vector3), int fontSize = 25, Color? color = null, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 5000)
    //{
    //    if (color == null)
    //    {
    //        color = Color.white;
    //    }
    //    return CreateWorldText(parent, text, localPos, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    //}
    ///// <summary>
    ///// Ekranda TextMesh olusturur. Yaz� yazar, yerini ayarlar, renk verir
    ///// </summary>
    //public static TextMeshPro CreateWorldText(Transform parent, string text, Vector3 localPos, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    //{
    //    GameObject gameObj = new GameObject("World_Text", typeof(TextMesh));
    //    Transform tr = gameObj.transform;
    //    tr.SetParent(parent);
    //    tr.localPosition = localPos;
    //    TextMesh textMesh = gameObj.GetComponent<TextMesh>();
    //    textMesh.anchor = textAnchor;
    //    textMesh.alignment = textAlignment;
    //    textMesh.text = text;
    //    textMesh.color = color;
    //    textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    //    return textMesh;
    //}
    /// <summary>
    /// Ekranda TextMesh olusturur. Yaz� yazari yerini ayarlar, renk verir
    /// </summary>
    public static TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPos = default(Vector3), float fontSize = 25, float debugWidth = 0, float debugHeight = 0, Color? color = null, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center, int sortingOrder = 5000)
    {
        if (color == null)
        {
            color = Color.white;
        }
        return CreateWorldText(parent, text, localPos, fontSize, debugWidth, debugHeight, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    /// <summary>
    /// Ekranda TextMesh olusturur. Yaz� yazar, yerini ayarlar, renk verir
    /// </summary>
    public static TextMeshPro CreateWorldText(Transform parent, string text, Vector3 localPos, float fontSize, float debugWidth, float debugHeight, Color color, TextAnchor textAnchor, TextAlignmentOptions textAlignment, int sortingOrder)
    {
        GameObject gameObj = new GameObject("World_Text", typeof(TextMeshPro));
        Transform tr = gameObj.transform;
        RectTransform rect = gameObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(debugWidth, debugHeight);
        tr.SetParent(parent);
        tr.localPosition = localPos;
        TextMeshPro textMesh = gameObj.GetComponent<TextMeshPro>();
        textMesh.fontSize = fontSize;
        //textMesh. = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
    /// <summary>
    /// Ekranda verdi�iniz pozisyonun ve kameran�n oyun i�indeki konumunu d�nderir
    /// </summary>
    public static Vector3 MousePozisyonWithZ(Vector3 screenPoint, Camera myCamera)
    {
        Vector3 mouseYeri = myCamera.ScreenToWorldPoint(screenPoint);
        return mouseYeri;
    }
    /// <summary>
    /// Ekranda verdi�iniz pozisyonun oyun i�indeki konumunu d�nderir. Ana kameray� kullan�r
    /// </summary>
    public static Vector3 MousePozisyonWithZ(Vector3 screenPoint)
    {
        Vector3 mouseYeri = Camera.main.ScreenToWorldPoint(screenPoint);
        return mouseYeri;
    }
    /// <summary>
    /// Ekranda verdi�iniz kameraya g�re oyun i�indeki mouse pozisyonu konumunu d�nderir
    /// </summary>
    public static Vector3 MousePozisyonWithZ(Camera myCamera)
    {
        Vector3 mouseYeri = myCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseYeri;
    }
    /// <summary>
    /// Ana kameray� kullanarak mouse pozisyonunun konumunu d�nderir.
    /// </summary>
    public static Vector3 MousePozisyonWithZ()
    {
        Vector3 mouseYeri = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouseYeri;
    }
    /// <summary>
    /// Ekranda verdi�iniz pozisyonun ve kameran�n oyun i�indeki konumunu Z de�erini 0 yap�p d�nderir
    /// </summary>
    public static Vector3 MousePozisyon(Vector3 screenPoint, Camera myCamera)
    {
        Vector3 mouseYeri = myCamera.ScreenToWorldPoint(screenPoint);
        mouseYeri.z = 0;
        return mouseYeri;
    }
    /// <summary>
    /// Ekranda verdi�iniz pozisyonun oyun i�indeki konumunu Z de�erini 0 yap�p d�nderir. Ana kameray� kullan�r
    /// </summary>
    public static Vector3 MousePozisyon(Vector3 screenPoint)
    {
        Vector3 mouseYeri = Camera.main.ScreenToWorldPoint(screenPoint);
        mouseYeri.z = 0;
        return mouseYeri;
    }
    /// <summary>
    /// Ekranda verdi�iniz kameraya g�re oyun i�indeki mouse pozisyonu Z de�erini 0 yap�p konumunu d�nderir
    /// </summary>
    public static Vector3 MousePozisyon(Camera myCamera)
    {
        Vector3 mouseYeri = myCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseYeri.z = 0;
        return mouseYeri;
    }
    /// <summary>
    /// Ana kameray� kullanarak mouse pozisyonunun konumunu Z de�erini 0 yap�p d�nderir.
    /// </summary>
    public static Vector3 MousePozisyon()
    {
        Vector3 mouseYeri = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseYeri.z = 0;
        return mouseYeri;
    }
    /// <summary>
    /// Verdi�imiz kamera, mesafe, layer mask kullan�larak �arpan RaycastHit'i veya bo� d�nderir.
    /// </summary>
    public static RaycastHit CanHitSomeThing3D(Camera myCamera, float mesafe, LayerMask mask)
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, mesafe, mask);
        return hit;
    }
    /// <summary>
    /// Layer mask kullan�larak �arpan RaycastHit'i veya bo� d�nderir.
    /// </summary>
    public static RaycastHit CanHitSomeThing3D(LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 999, mask);
        return hit;
    }
    /// <summary>
    /// Layer mask kullan�larak �arpan RaycastHit'iin pointini d�nderir.
    /// </summary>
    public static Vector3 CanSomeThingHitPoint(LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999, mask))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    /// <summary>
    /// Verdi�imiz kamera, mesafe, layer mask kullan�larak �arpan RaycastHit2D'i veya bo� d�nderir.
    /// </summary>
    public static RaycastHit2D CanHitSomeThing2D(Camera myCamera, float mesafe, LayerMask mask)
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, mesafe, mask);
        return hit;
    }
    /// <summary>
    /// Verdi�imiz pozisyon, y�n, mesafe, layer mask kullan�larak �arpan bir�ey var m�?
    /// </summary>
    public static bool CanHitSomeThing2D(Vector3 pozisyon, Vector3 targetDirection, float mesafe, LayerMask mask)
    {
        return Physics.Raycast(pozisyon, targetDirection, mesafe, mask);
    }
    /// <summary>
    /// Verdi�imiz pozisyon, resim ve fonksiyon ile ekrana bir buton yerle�tirir.
    /// </summary>
    public static void CreateImageButton(Vector2 pozisyon, Sprite resim, Action fonksiyon)
    {
        // recttransform - �mage - button ekle
        GameObject buton = new GameObject("Gecici Button", typeof(RectTransform), typeof(Image), typeof(SpriteButton_UI));
        // butonun yerini ayarla
        buton.GetComponent<RectTransform>().anchoredPosition = pozisyon;
        // butonun resmini ayarla
        buton.GetComponent<Image>().sprite = resim;
        //buton.GetComponent<Image>().sprite = whiteSpriteCenterPoint;
        // butonun fonksiyonunu ayarla
        buton.GetComponent<SpriteButton_UI>().OnClick = fonksiyon;
    }
    /// <summary>
    /// Verdi�imiz pozisyon, resim ve fonksiyon ile ekrana bir buton yerle�tirir.
    /// </summary>
    public static void CreateSpriteButton(Vector3  pozisyon, Sprite resim, Action fonksiyon)
    {
        // recttransform - �mage - button ekle
        GameObject buton = new GameObject("Gecici Button", typeof(SpriteButton_World), typeof(SpriteRenderer), typeof(BoxCollider2D));
        // butonun resmini ayarla
        buton.transform.position = pozisyon;
        // butonun yerini ayarla
        buton.GetComponent<SpriteRenderer>().sprite = resim;
        // butonun fonksiyonunu ayarla
        buton.GetComponent<SpriteButton_World>().OnClick = fonksiyon;
    }
    /// <summary>
    /// Yaz�y� float'a �evirir
    /// </summary>
    public static float Float_Parse(string rakam, float defaultDeger)
    {
        if (float.TryParse(rakam, out float deger))
        {
            return deger;
        }
        return defaultDeger;
    }
    /// <summary>
    /// Yaz�y� int'e �evirir
    /// </summary>
    public static float Int_Parse(string rakam, int defaultDeger)
    {
        if (int.TryParse(rakam, out int deger))
        {
            return deger;
        }
        return defaultDeger;
    }
    /// <summary>
    /// Mouse bir UI objesinin �st�nde mi? De�il mi?
    /// </summary>
    public static bool IsMouseOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            ped.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, hits);
            return hits.Count > 0;
        }
    }
    /// <summary>
    /// Tamamen random renk d�nderir.
    /// </summary>
    public static Color RandomColorBetween0_1()
    {
        return new Color(UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1));
    }
    /// <summary>
    /// Random renk d�nderir ama alphas� 1 olur.
    /// </summary>
    public static Color RandomColorExceptAlphaBetween0_1()
    {
        return new Color(UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1), 1);
    }
    /// <summary>
    /// Random renk d�nderir ama alphas�n� biz ayarlar�z.
    /// </summary>
    public static Color RandomColorBetween0_1(float alpha)
    {
        return new Color(UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1),
                         UnityEngine.Random.Range(0, 1), alpha);
    }
    /// <summary>
    /// Tamamen random renk d�nderir.
    /// </summary>
    public static Color RandomColorBetween0_255()
    {
        return new Color(UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255));
    }
    /// <summary>
    /// Random renk d�nderir ama alphas� 255 olur.
    /// </summary>
    public static Color RandomColorExceptAlphaBetween0_255()
    {
        return new Color(UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255), 255);
    }
    /// <summary>
    /// Random renk d�nderir ama alphas�n� biz ayarlar�z.
    /// </summary>
    public static Color RandomColorBetween0_255(int alpha)
    {
        return new Color(UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255),
                         UnityEngine.Random.Range(0, 255), alpha);
    }
    /// <summary>
    /// Random vector3 d�nderir.
    /// </summary>
    public static Vector3 RandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2),
                           UnityEngine.Random.Range(-1, 2),
                           UnityEngine.Random.Range(-1, 2)).normalized;
    }
    /// <summary>
    /// Random vector3 d�nderir ama speed ile �arp�lm�� olur.
    /// </summary>
    public static Vector3 RandomDirectionWithSpeed(int speed)
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2),
                           UnityEngine.Random.Range(-1, 2),
                           UnityEngine.Random.Range(-1, 2)).normalized * speed;
    }
    /// <summary>
    /// X ve Y eksenlerine g�re random vector3 d�nderir
    /// </summary>
    public static Vector3 RandomDirectionXY()
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2),
                           UnityEngine.Random.Range(-1, 2), 0).normalized;
    }
    /// <summary>
    /// X ve Y eksenlerine g�re random vector3 d�nderir ama speed ile �arp�lm�� olur.
    /// </summary>
    public static Vector3 RandomDirectionXYWithSpeed(int speed)
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2),
                           UnityEngine.Random.Range(-1, 2), 0).normalized * speed;
    }
    /// <summary>
    /// X ve Z eksenlerine g�re random vector3 d�nderir
    /// </summary>
    public static Vector3 RandomDirectionXZ()
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2), 0,
                           UnityEngine.Random.Range(-1, 2)).normalized;
    }
    /// <summary>
    /// X ve Z eksenlerine g�re random vector3 d�nderir ama speed ile �arp�lm�� olur.
    /// </summary>
    public static Vector3 RandomDirectionXZWithSpeed(int speed)
    {
        return new Vector3(UnityEngine.Random.Range(-1, 2), 0,
                           UnityEngine.Random.Range(-1, 2)).normalized * speed;
    }
    /// <summary>
    /// X ve Y eksenlerindeki vector a��lar�na g�re a�� d�nderir.
    /// </summary>
    public static float GetFloatAngleFromVectorXY(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return (angle + 360) % 360;
    }
    /// <summary>
    /// X ve Z eksenlerindeki vector a��lar�na g�re a�� d�nderir.
    /// </summary>
    public static float GetFloatAngleFromVectorXZ(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        return (angle + 360) % 360;
    }
    /// <summary>
    /// X ve Y eksenlerindeki vector a��lar�na g�re int bir a�� d�nderir.
    /// </summary>
    public static int GetIntAngleFromVectorXY(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        int a = Mathf.RoundToInt((angle + 360) % 360);
        return a;
    }
    /// <summary>
    /// X ve Z eksenlerindeki vector a��lar�na g�re int bir a�� d�nderir.
    /// </summary>
    public static int GetIntAngleFromVectorXZ(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        int a = Mathf.RoundToInt((angle + 360) % 360);
        return a;
    }
    /// <summary>
    /// A��ya g�re X ve Y eksenleri i�in vector d�nderir.
    /// </summary>
    public static Vector3 GetVectorFromIntAngleXZ(int angle)
    {
        float angleRed = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRed), Mathf.Sin(angleRed));
    }
    /// <summary>
    /// A��ya g�re X ve Y eksenleri i�in vector d�nderir.
    /// </summary>
    public static Vector3 GetVectorFromIntAngleXZ(float angle)
    {
        float angleRed = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRed), Mathf.Sin(angleRed));
    }
    /// <summary>
    /// List i�inden rastgele bir eleman d�nderir.
    /// </summary>
    public static T GetRandomListElement<T>(List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    /// <summary>
    /// Array i�inden rastgele bir eleman d�nderir.
    /// </summary>
    public static T GetRandomArrayElement<T>(T[] list)
    {
        return list[UnityEngine.Random.Range(0, list.Length)];
    }
    /// <summary>
    /// Dictionary'nin key k�sm�n� list �eklinde d�nderir.
    /// </summary>
    public static List<T> GetKeyListFromDictionary<T>(Dictionary<T, T> list)
    {
        return new List<T>(list.Keys);
    }
    /// <summary>
    /// Verilen rakamlar� isme ekliyecek hale getirir.
    /// </summary>
    public static string SetGameObjectName(params int[] rakamlar)
    {
        string newName = "";
        for (int e = 0; e < rakamlar.Length; e++)
        {
            newName += "---" + rakamlar[e];
        }
        return newName;
    }
    /// <summary>
    /// String yaz�y� ayrac'a g�re ay�r�r.
    /// </summary>
    public static string[] SplitString(string splitString, string ayrac)
    {
        return splitString.Split(new string[] { ayrac }, StringSplitOptions.None);
    }
    /// <summary>
    /// Parentin alt�ndaki t�m �ocuklar� yok eder.
    /// </summary>
    public static void DestroyTransforChildren(Transform parent)
    {
        for (int e = parent.childCount - 1; e >= 0 ; e--)
        {
            GameObject.Destroy(parent.GetChild(e));
        }
    }
    /// <summary>
    /// Alan i�inden rastgele bir pozisyon d�nderir.
    /// </summary>
    public static Vector3 RandomPozisyonInKare(float xMin, float xMax, float yMin, float yMax)
    {
        return new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));
    }
    /// <summary>
    /// Alan i�inden rastgele bir pozisyon d�nderir.
    /// </summary>
    public static Vector3 RandomPozisyonInKare(Vector2 xKenar, Vector2 yKenar)
    {
        return new Vector3(UnityEngine.Random.Range(xKenar.x, yKenar.x), UnityEngine.Random.Range(xKenar.y, yKenar.y));
    }
    /// <summary>
    /// Transfor objesini klonlar.
    /// </summary>
    public static Transform CloneObject(Transform cloneObject, string objectName = null)
    {
        Transform clone = Instantiate(cloneObject, cloneObject.parent);
        if (objectName == null)
        {
            clone.name = cloneObject.name;
        }
        else
        {
            clone.name = objectName;
        }
        return clone;
    }
    /// <summary>
    /// Float degerini y�zde �eklinde string olarak d�nderir.
    /// </summary>
    public static string GetPercent(float deger)
    {
        return "% " + Mathf.RoundToInt(deger * 100);
    }
    /// <summary>
    /// Float degerini y�zde ve noktal� rakam �eklinde string olarak d�nderir.
    /// </summary>
    public static string GetPercentOndal�kl�(float deger)
    {
        return "% " + Mathf.RoundToInt(deger * 100).ToString("F2");
    }

    [SerializeField] private Sprite whiteSpriteCenterPoint;
    [SerializeField] private Sprite whiteSpriteLeftPoint;

    private void Update()
    {
        //if (is3D)
        //{
        //    RaycastHit hit = CanHitSomeThing3D(myCamera, mesafe_3D, mask);
        //    if (hit.transform != null)
        //    {
        //        myObject.position = hit.point;
        //    }
        //}
        //else
        //{
        //    myObject.position = MousePozisyon(myCamera);
        //}
    }
}