using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ToolPrefabs;
    [SerializeField]
    private GameObject AttentionToolTips;
    [SerializeField]
    private GameObject Animal_Obj_Rabbit;
    [SerializeField]
    private GameObject Animal_Obj_Owl;
    [SerializeField]
    private GameObject Animal_Obj_Elephant;
    [SerializeField]
    private GameObject Animal_Obj_Penguin;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    GameObject _Canvas;
    private GameObject ToolTip = null;
    private GameObject AttentionToolTip = null;

    //ゾウさんのTips
    private string name_Ele = "なまえ:ぞう";
    private string tokusei_Ele = "とくせい:きょだい";
    private string speed_Ele = "スピード:5";
    //フクロウのTips
    private string name_Owl = "なまえ:ふくろう";
    private string tokusei_Owl = "とくせい:とぶ";
    private string speed_Owl = "すぴーど:5";
    //うさぎさんのTips
    private string name_Rab = "なまえ:うさぎ";
    private string tokusei_Rab = "とくせい:こがた";
    private string speed_Rab = "すぴーど:10";
    //ぺんぎんのTips
    private string name_Pen = "なまえ:ぺんぎん";
    private string tokusei_Pen = "とくせい:こがた";
    private string speed_Pen = "すぴーど:3";
    //未実装
    private string name_Sec = "なまえ:";
    private string tokusei_Sec = "とくせい:";
    private string speed_Sec = "すぴーど:";

    private Vector3 pos;
    private RaycastHit2D hit;



    //ツールチップを消す
    public void EraseToolTips()
    {
        //消す
        Destroy(ToolTip);
        //値をnullに戻す
        ToolTip = null;
        //Debug.Log("<color=red>Erase</color>");
    }

    public void ToolTips_Animal(GameObject obj)
    {
        //ツールチップの位置を指定する
        int objnum = int.Parse(obj.name.Substring(obj.name.Length - 1));
        Vector2 pos = obj.transform.position;
        GameObject canvas = GameObject.Find("Canvas");
        //ツールチップが表示されていない時は、色々指定する
        if (ToolTip == null)
        {
            ToolTip = Instantiate(ToolPrefabs, pos, Quaternion.identity);
            ToolTip.transform.SetParent(canvas.transform);
            var RectTransform = ToolTip.GetComponent<RectTransform>();
            RectTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            ToolTip.transform.position = pos;
        }
        //ToolTipのテキストを変更する
        switch (objnum)
        {
            case 1:
                ToolTip.transform.GetChild(0).GetComponent<Text>().text = name_Rab;
                ToolTip.transform.GetChild(1).GetComponent<Text>().text = tokusei_Rab;
                ToolTip.transform.GetChild(2).GetComponent<Text>().text = speed_Rab;
                break;
            case 2:
                ToolTip.transform.GetChild(0).GetComponent<Text>().text = name_Owl;
                ToolTip.transform.GetChild(1).GetComponent<Text>().text = tokusei_Owl;
                ToolTip.transform.GetChild(2).GetComponent<Text>().text = speed_Owl;
                break;
            case 3:
                ToolTip.transform.GetChild(0).GetComponent<Text>().text = name_Ele;
                ToolTip.transform.GetChild(1).GetComponent<Text>().text = tokusei_Ele;
                ToolTip.transform.GetChild(2).GetComponent<Text>().text = speed_Ele;
                break;
            case 4:
                ToolTip.transform.GetChild(0).GetComponent<Text>().text = name_Pen;
                ToolTip.transform.GetChild(1).GetComponent<Text>().text = tokusei_Pen;
                ToolTip.transform.GetChild(2).GetComponent<Text>().text = speed_Pen;
                break;
            default:
                ToolTip.transform.GetChild(0).GetComponent<Text>().text = name_Sec;
                ToolTip.transform.GetChild(1).GetComponent<Text>().text = tokusei_Sec;
                ToolTip.transform.GetChild(2).GetComponent<Text>().text = speed_Sec;
                break;
        }
    }

    float _alpha;
    CanvasGroup _CanvasGroup;
    public bool ShowAttentionActive;

    //アテンションツールチップを表示する
    public void Show_AttentionToolTips()
    {
        if(!ShowAttentionActive)
        {
            ShowAttentionActive = true;
            AttentionToolTip = Instantiate(AttentionToolTips) as GameObject;
            AttentionToolTip.transform.SetParent(_Canvas.transform);
            _CanvasGroup = AttentionToolTip.GetComponent<CanvasGroup>();
            RectTransform _AttentionRect = AttentionToolTip.GetComponent<RectTransform>();
            _AttentionRect.localScale = new Vector3(1, 1, 1);
            _AttentionRect.anchoredPosition3D = new Vector3(0, 0, 0);
            //テキストいじるならココ
            //ToolTip.transform.GetChild(0).GetComponent<Text>().text = "Text";
            _alpha = 1;
            _CanvasGroup.alpha = _alpha;
            //コルーチン便利かよ!!!!!
            StartCoroutine(FadeOut_AttentionToolTips());
        }
        else
        {
            Debug.Log("表示中");
        }
    }

    IEnumerator FadeOut_AttentionToolTips()
    {
        float _Time = 0;
        while (_alpha > 0)
        {
            _Time += Time.deltaTime;
            if (_Time >= 1)
            {
                _alpha -= Time.deltaTime;
            }
            _CanvasGroup.alpha = _alpha;
            yield return null;
        }
        ShowAttentionActive = false;
        Destroy(AttentionToolTip);
        AttentionToolTip = null;
    }
}
