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
    

    List<int> listNum = new List<int>();
    [SerializeField]
    private Sprite[] numImage;

    public void DiplayText_Animal(Vector3 pos, float damage, DamageTextColor damageTextColor = DamageTextColor.Defalut)
    {
        //  重なり順の変更
        pos.z = -9.7f;
        //  文字生成
        GameObject obj = Instantiate(DamageObject, pos, transform.rotation);
        obj.GetComponent<TextMesh>().text = damage.ToString();
        obj.GetComponent<TextMesh>().color = _damageTextColors[damageTextColor];
        //Debug.Log(BattleManager.Instance.TypeCheckNum);


        GameObject.Find("DamageTextImage").GetComponent<Image>().sprite = numImage[listNum[0]];
        for (int i = 1; i < listNum.Count; i++)
        {
            RectTransform _damageTextImage = (RectTransform)Instantiate(GameObject.Find("DamageTextImage")).transform;
            _damageTextImage.SetParent(this.transform, false);
            _damageTextImage.localPosition = new Vector2(_damageTextImage.localPosition.x - _damageTextImage.sizeDelta.x * i, _damageTextImage.localPosition.y);
            _damageTextImage.GetComponent<Image>().sprite = numImage[listNum[i]];
        }

        switch (BattleManager.Instance.TypeCheckNum)
        {
            case 0:
                Debug.Log("Player同じ武器の攻撃");
                break;
            case 1:
                Debug.Log("Player自分が有利");
                break;
            case 2:
                Debug.Log("Player相手が有利");
                break;
        }

      /*  switch (BattleManager.Instance.TypeCheckNum)
        {   
            case 0:
                obj.GetComponent<TextMesh>().fontSize = 25;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(20, 80), Random.Range(100, 180)));
                break;
            case 1:
                obj.GetComponent<TextMesh>().fontSize = 50;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(20, 80), Random.Range(100, 180)));
                break;
            case 2:
                obj.GetComponent<TextMesh>().fontSize = 30;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(20, 80), Random.Range(100, 180)));
                break;
        }*/
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

       /* switch (BattleManager.Instance.TypeCheckNum)
        {
            case 0:
                Debug.Log("Enemy同じ武器の攻撃");
                break;
            case 1:
                Debug.Log("Enemy相手が有利");
                break;
            case 2:
                Debug.Log("Enemy自分が有利");
                break;
        }
        */
        /*switch (BattleManager.Instance.TypeCheckNum)
        {
            case 0:

                obj.GetComponent<TextMesh>().fontSize = 25;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-60, -100), Random.Range(100, 180)));
                break;
            case 1:

                obj.GetComponent<TextMesh>().fontSize = 50;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-60, -100), Random.Range(100, 180)));
                break;
            case 2:

                obj.GetComponent<TextMesh>().fontSize = 30;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-60, -100), Random.Range(100, 180)));
                break;
        }*/
        StartCoroutine(DestryText(obj));
    }

    IEnumerator DestryText(GameObject destryObj)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(destryObj);
        yield break;
    }
}
