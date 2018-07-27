using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    [SerializeField]
    private GameObject DamageObject;

    [SerializeField]
    private Font[] Fonts = new Font[3];

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

    #region テキストカラーの各設定
    /// <summary>
    /// カラーの種類定義
    /// </summary>
    public enum DamageTextColor
    {
        Defalut = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Other
    }
    #endregion
    /// <summary>
    /// カラーコードテーブル
    /// </summary>
    /// <returns></returns>>
    private Dictionary<DamageTextColor, Color> _damageTextColors = new Dictionary<DamageTextColor, Color>()
    {
        { DamageTextColor.Defalut, Color.white},
        { DamageTextColor.Red, Color.red},
        { DamageTextColor.Green,  Color.green},
        { DamageTextColor.Blue, Color.blue},
        { DamageTextColor.Other, Color.gray},
    };

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
/// <summary>
/// ダメージのテキストを表示する
/// </summary>
/// <param name="pos">表示座標</param>
/// <param name="damage">ダメージの値</param>
/// <param name="damageTextColor">ダメージカラー(デフォルトは黒)</param>

        //動物
    public void DiplayText_Animal(Vector3 pos, float damage, DamageTextColor damageTextColor = DamageTextColor.Defalut)
    {
        //  重なり順の変更
        pos.z = -9.7f;
        //  文字生成
        GameObject obj = Instantiate(DamageObject, pos, transform.rotation);
        obj.GetComponent<TextMesh>().text = damage.ToString();
        obj.GetComponent<TextMesh>().color = _damageTextColors[damageTextColor];
        switch (BattleManager.Instance.TypeCheckNum)
        {
            case 0:
                obj.GetComponent<TextMesh>().font = Fonts[1];
                obj.GetComponent<TextMesh>().fontSize = 50;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(20, 80), Random.Range(100, 180)));
                break;
            case 1:
                obj.GetComponent<TextMesh>().font = Fonts[1];
                obj.GetComponent<TextMesh>().fontSize = 20;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(20, 80), Random.Range(100, 180)));
                break;
            case 2:
                obj.GetComponent<TextMesh>().font = Fonts[1];
                obj.GetComponent<TextMesh>().fontSize = 40;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(20, 80), Random.Range(100, 180)));
                break;
        }
        StartCoroutine(DestryText(obj));
    }

    public void DiplayText_Enemy(Vector3 pos, float damage, DamageTextColor damageTextColor = DamageTextColor.Defalut)
    {
        //  重なり順の変更
        pos.z = -8;
        //  文字生成
        GameObject obj = Instantiate(DamageObject, pos, transform.rotation);
        obj.GetComponent<TextMesh>().text = damage.ToString();
        obj.GetComponent<TextMesh>().color = _damageTextColors[damageTextColor];
        //
        switch (BattleManager.Instance.TypeCheckNum)
        {
            case 0:
                obj.GetComponent<TextMesh>().font = Fonts[1];
                obj.GetComponent<TextMesh>().fontSize = 50;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-60, -100), Random.Range(100, 180)));
                break;
            case 1:
                obj.GetComponent<TextMesh>().font = Fonts[1];
                obj.GetComponent<TextMesh>().fontSize = 30;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-60, -100), Random.Range(100, 180)));
                break;
            case 2:
                obj.GetComponent<TextMesh>().font = Fonts[1];
                obj.GetComponent<TextMesh>().fontSize = 40;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-60, -100), Random.Range(100, 180)));
                break;
        }
        StartCoroutine(DestryText(obj));
    }

    IEnumerator DestryText(GameObject destryObj)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(destryObj);
        yield break;
    }
}
