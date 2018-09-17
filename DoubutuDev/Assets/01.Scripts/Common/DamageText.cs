using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _System = System;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private GameObject DamageObject;
    // 0～9のSprite画像を格納する変数
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
    // 相性計算されたダメージの数値をSpriteの画像を適用して表示する
    public void DiplayTextSprite(Vector3 pos, float damagenum)
    {
        // 値の切り捨て
        int damagenum_int = (int)_System.Math.Round(damagenum, 0);

        if (damagenum < 10)
        {
            //　switch文でダメージの値によって画像を表示する
            switch (damagenum_int)
            {
                case 0:
                    DamageNumDisplay(0, pos);
                    break;
                case 1:
                    DamageNumDisplay(1, pos);
                    break;
                case 2:
                    DamageNumDisplay(2, pos);
                    break;
                case 3:
                    DamageNumDisplay(3, pos);
                    break;
                case 4:
                    DamageNumDisplay(4, pos);
                    break;
                case 5:
                    DamageNumDisplay(5, pos);
                    break;
                case 6:
                    DamageNumDisplay(6, pos);
                    break;
                case 7:
                    DamageNumDisplay(7, pos);
                    break;
                case 8:
                    DamageNumDisplay(8, pos);
                    break;
                case 9:
                    DamageNumDisplay(9, pos);
                    break;
            }
        }
        else if (damagenum > 10)
        {
            Debug.Log(damagenum);

        }
        
    }
    // ダメージを表示させる関数
    void DamageNumDisplay(int num, Vector3 pos)
    {
        //　変数に格納
        GameObject _damagetextobj = new GameObject("DamageTextObj");
        //　数値を表示する
        _damagetextobj = Instantiate(DamageObject, pos, transform.rotation);
        //　RigidbodyにAddForceする
        _damagetextobj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, 80), Random.Range(100, 180)));
        //　SpriteRendererのSpriteにspritesに格納してある画像を読み込む
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
