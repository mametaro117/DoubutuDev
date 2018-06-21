using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ToolPrefabs;
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

    private Vector3 pos;
    private RaycastHit2D hit;

    private void Start()
    {
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);
            if (hit.collider.tag == "rabbit"){ Debug.Log("aaaaa"); } 
            //ToolTips_Animal();
        }
        if (Input.GetMouseButtonUp(0))
        {
            
        }
	}

    private void ToolTips_Animal()
    {
        GameObject obj = Instantiate(ToolPrefabs, parent);
        if (hit.transform.gameObject.name == "Animal_Obj_Rabbit")
        {
        obj.transform.GetChild(0).GetComponent<Text>().text = name_Rab;
        obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Rab;
        obj.transform.GetChild(2).GetComponent<Text>().text = speed_Rab;
        Debug.Log("この動物は「うさぎ」です");
        }
        if (gameObject.name == "Column2")
        {
        obj.transform.GetChild(0).GetComponent<Text>().text = name_Owl;
        obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Owl;
        obj.transform.GetChild(2).GetComponent<Text>().text = speed_Owl;
        Debug.Log("この動物は「ふくろう」です");
        }
        if (gameObject.name == "Column3")
        {
        obj.transform.GetChild(0).GetComponent<Text>().text = name_Ele;
        obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Ele;
        obj.transform.GetChild(2).GetComponent<Text>().text = speed_Ele;
        Debug.Log("この動物は「ぞう」です");
        }
        if (gameObject.name == "Column4")
        {
        obj.transform.GetChild(0).GetComponent<Text>().text = name_Pen;
        obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Pen;
        obj.transform.GetChild(2).GetComponent<Text>().text = speed_Pen;
        Debug.Log("この動物は「ペンギン」です");
        }
    }
}
