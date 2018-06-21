﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltips_Animal : MonoBehaviour
{
    [SerializeField]
    private GameObject ToolPrefabs;
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

    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Animal_Tips();
        }
    }
    void Animal_Tips()
    {
        GameObject obj = Instantiate(ToolPrefabs, parent);
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);
        if (hit.transform.tag == "elephant")
        {
            obj.transform.GetChild(0).GetComponent<Text>().text = name_Ele;
            obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Ele;
            obj.transform.GetChild(2).GetComponent<Text>().text = speed_Ele;
        }
        if (hit.transform.tag == "bird")
        {
            obj.transform.GetChild(0).GetComponent<Text>().text = name_Owl;
            obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Owl;
            obj.transform.GetChild(2).GetComponent<Text>().text = speed_Owl;
        }
        if (hit.transform.tag == "rabbit")
        {
            obj.transform.GetChild(0).GetComponent<Text>().text = name_Rab;
            obj.transform.GetChild(1).GetComponent<Text>().text = tokusei_Rab;
            obj.transform.GetChild(2).GetComponent<Text>().text = speed_Rab;
        }
    }
}