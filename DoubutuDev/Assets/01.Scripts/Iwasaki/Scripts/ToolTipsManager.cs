using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ToolPrefabs;
    [SerializeField]
<<<<<<< HEAD
=======
    private GameObject AttentionToolTips;
    [SerializeField]
>>>>>>> Hirokawa
    private GameObject Animal_Obj_Rabbit;
    [SerializeField]
    private GameObject Animal_Obj_Owl;
    [SerializeField]
    private GameObject Animal_Obj_Elephant;
    [SerializeField]
    private GameObject Animal_Obj_Penguin;
    [SerializeField]
    private Transform parent;
    private GameObject ToolTip;

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

    private Vector3 pos;
    private RaycastHit2D hit;

<<<<<<< HEAD
    void Update ()
    {
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);

    //        ToolTips_Animal();
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
            
    //    }
	}
=======

>>>>>>> Hirokawa

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
<<<<<<< HEAD
=======

    float _alpha;

    public void Show_AttentionToolTips()
    {
        ToolTip = Instantiate(AttentionToolTips) as GameObject;
        Debug.Log(ToolTip);
        GameObject _Canvas = GameObject.Find("Canvas");
        AttentionToolTips.transform.SetParent(_Canvas.transform);
        CanvasGroup _CanvasGroup = AttentionToolTips.GetComponent<CanvasGroup>();
        _alpha = _CanvasGroup.alpha;
        _alpha = 1;
        FadeOut_AttentionToolTips();
    }

    private void FadeOut_AttentionToolTips()
    {
        float _Time = 0;
        while(_alpha > 0)
        {
            _Time += Time.deltaTime;
            if(_Time >= 1)
            {
                _alpha -= Time.deltaTime;
            }
        }
    }
>>>>>>> Hirokawa
}
