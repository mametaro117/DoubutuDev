﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalChoiceManager : MonoBehaviour {

    bool SelectList1Active;
    bool SelectList2Active;
    bool SelectList3Active;

    //動物が追加になったら増やしてね
    bool[] AnimalListsActive = new bool[10];

    // [動物id, 武器id1, 武器id2, 武器id3]
    int[,] SelectAnimalList = new int[3,4];
    enum ChoiceState
    {
        Animal,
        equip,
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EraseToolTip()
    {
        GameObject Manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager TT_Manager = Manager.GetComponent<ToolTipsManager>();
        TT_Manager.EraseToolTips();
    }

    public void SelectColumnClick(GameObject obj)
    {
        Debug.Log("SelectListタップ！");
        Debug.Log(obj.name);
    }

    public void AnimalColumnClick(GameObject obj)
    {
        Debug.Log("AnimalListタップ！");
        Debug.Log(obj.name);
    }

    public void AnimalDrag(GameObject ChildObj)
    {
        if(!AnimalListsActive[int.Parse(ChildObj.name.Substring(ChildObj.name.Length - 1)) - 1])
        {
            Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ChildObj.transform.position = TapPos;
        }
    }

    public void EndDrag(GameObject obj)
    {
        GameObject Parent;
        Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(TapPos);
        //ドラッグし終わった時に別の枠内に入っていたら
        for (int i = 1; i < 4; i++)
        {
            Parent = GameObject.Find("Canvas/SelectList/SelectColumn" + i + "");
            Vector2 UiPos = Parent.transform.position;
            //Debug.Log(UiPos);
            float Diff_x = TapPos.x - UiPos.x;
            float Diff_y = TapPos.y - UiPos.y;
            if(-1f <= Diff_x && Diff_x <= 1f)
            {
                if(-1f <= Diff_y && Diff_y <= 1f)
                {
                    //枠の子孫とする
                    switch(i)
                    {
                        case 1:
                            if(!SelectList1Active)
                            {
                                obj.transform.SetParent(Parent.transform);
                                SelectList1Active = true;
                                SelectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                                AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                //Debug.Log(SelectAnimalList[i - 1, 0]);
                                Debug.Log("1枠に挿入");
                            }
                            break;
                        case 2:
                            if(!SelectList2Active)
                            {
                                obj.transform.SetParent(Parent.transform);
                                SelectList2Active = true;
                                SelectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                                AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                //Debug.Log(SelectAnimalList[i - 1, 0]);
                                Debug.Log("2枠に挿入");
                            }
                            break;
                        case 3:
                            if(!SelectList3Active)
                            {
                                obj.transform.SetParent(Parent.transform);
                                SelectList3Active = true;
                                SelectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                                AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                //Debug.Log(SelectAnimalList[i - 1, 0]);
                                Debug.Log("3枠に挿入");
                            }
                            break;
                        default:
                            Debug.Log("何枠やねん");
                            break;
                    }
                    break;
                }
            }
        }
        //親のboxに戻る
        var RectTransform = obj.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2();
        RectTransform.anchoredPosition = ResetPos;
    }

    public void BackHome(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            AnimalListsActive[int.Parse(child.name.Substring(child.name.Length - 1)) - 1] = false;
            //Debug.Log(child.name);
            int strlength = child.name.Length;
            int BoxNum = int.Parse(child.name.Substring(strlength - 1));
            //Debug.Log(BoxNum);
            switch(BoxNum)
            {
                case 1:
                    SelectList1Active = false;
                    break;
                case 2:
                    SelectList2Active = false;
                    break;
                case 3:
                    SelectList3Active = false;
                    break;
                default:
                    Debug.Log("（´・ω・｀）");
                    break;
            }
            GameObject Box = GameObject.Find("Canvas/AnimalList/Column" + BoxNum + "");
            child.transform.SetParent(Box.transform);
            var RectTransform = child.GetComponent<RectTransform>();
            Vector2 ResetPos = new Vector2();
            RectTransform.anchoredPosition = ResetPos;
        }
    }

    public bool AnimalSetCheck()
    {
        if(SelectList1Active && SelectList2Active && SelectList3Active)
        {
            return true;
        }
        return false;
    }

}
