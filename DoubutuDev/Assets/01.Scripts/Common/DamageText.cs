using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _System = System;

public class DamageText : MonoBehaviour {

    [SerializeField]
    private GameObject DamageObject;
    [SerializeField]
    private Sprite[] sprites = new Sprite[10];

    #region Singleton

    private static DamageText instance;

    public static DamageText Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (DamageText)FindObjectOfType(typeof(DamageText));

                if (instance == null)
                {
                    Debug.LogError(typeof(DamageText) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /*public void DiplayText(Vector3 pos, float damage)
    {//重なり順の変更
        
        /os.z = -5;
        //  文字生成
        GameObject obj = Instantiate(DamageObject, pos, transform.rotation);
        obj.GetComponent<TextMesh>().text = damage.ToString();
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, 80), Random.Range(100, 180)));
        StartCoroutine(DestryText(obj));
    }*/
    public void DiplayTextSprite(Vector3 pos, float damagenum)
    {
        int damagenum_int = (int)_System.Math.Round(damagenum, 0);
        switch (damagenum_int)
        {
            case 0:
                DamageNumDisplay(0, pos);
                Debug.Log("0");
                break;
            case 1:
                DamageNumDisplay(1, pos);
                Debug.Log("1");
                break;
            case 2:
                DamageNumDisplay(2, pos);
                Debug.Log("2");
                break;
            case 3:
                DamageNumDisplay(3, pos);
                Debug.Log("3");
                break;
            case 4:
                DamageNumDisplay(4, pos);
                Debug.Log("4");
                break;
            case 5:
                DamageNumDisplay(5, pos);
                Debug.Log("5");
                break;
            case 6:
                DamageNumDisplay(6, pos);
                Debug.Log("6");
                break;
            case 7:
                DamageNumDisplay(7, pos);
                Debug.Log("7"); 
                break;
            case 8:
                DamageNumDisplay(8, pos);
                Debug.Log("8");
                break;
            case 9:
                DamageNumDisplay(9, pos);
                Debug.Log("9");
                break;
        }
    }
    // ダメージを表示させる関数
    public void DamageNumDisplay(int num, Vector3 pos)
    {
        GameObject _damagetextobj = new GameObject("DamageTextObj");
        _damagetextobj = Instantiate(DamageObject, pos, transform.rotation);
        _damagetextobj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, 80), Random.Range(100, 180)));
        _damagetextobj.GetComponent<SpriteRenderer>().sprite = sprites[num];
        //　一定時間で消すコルーチン開始
        StartCoroutine(DestryText(_damagetextobj));
    }
    //　DamageTextを一定の時間で消すコルーチン
    IEnumerator DestryText(GameObject destryObj)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(destryObj);
        yield break;
    }
}
