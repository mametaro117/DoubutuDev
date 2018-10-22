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
    GameObject _Canvas;
    private GameObject ToolTip = null;
    private GameObject AttentionToolTip = null;

    //ゾウさんのTips
    private string name_Ele = "名前:ゾウ";
    private string tokusei_Ele = "特性:巨大";
    private string speed_Ele = "スピード:10";
    //フクロウのTips
    private string name_Owl = "名前:フクロウ";
    private string tokusei_Owl = "特性:飛ぶ";
    private string speed_Owl = "スピード:5";
    //うさぎさんのTips
    private string name_Rab = "名前:うさぎ";
    private string tokusei_Rab = "特性:小型";
    private string speed_Rab = "スピード:3";
    //ぺんぎんのTips
    private string name_Pen = "名前:ぺんぎん";
    private string tokusei_Pen = "特性:小型";
    private string speed_Pen = "スピード:3";
    //未実装
    private string name_Sec = "名前:";
    private string tokusei_Sec = "特性:";
    private string speed_Sec = "スピード:";
    
    //ツールチップを消す
    public void EraseToolTips()
    {
        //消す
        Destroy(ToolTip);
        //値をnullに戻す
        ToolTip = null;
    }

    public void ToolTips_Animal(GameObject obj)
    {
        if(GameObject.Find("ClickManager").GetComponent<WindowChangeScript>().windowStationary)
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
