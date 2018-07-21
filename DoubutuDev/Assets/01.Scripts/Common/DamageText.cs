using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    [SerializeField]
    private GameObject DamageObject;

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

    public void DiplayText(Vector3 pos, float damage, DamageTextColor damageTextColor = DamageTextColor.Defalut)
    {
        //  重なり順の変更
        pos.z = -5;
        //  文字生成
        GameObject obj = Instantiate(DamageObject, pos, transform.rotation);
        obj.GetComponent<TextMesh>().text = damage.ToString();
        obj.GetComponent<TextMesh>().color = _damageTextColors[damageTextColor];

        obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, 80), Random.Range(100, 180)));
        StartCoroutine(DestryText(obj));
    }

    IEnumerator DestryText(GameObject destryObj)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(destryObj);
        yield break;
    }
}
