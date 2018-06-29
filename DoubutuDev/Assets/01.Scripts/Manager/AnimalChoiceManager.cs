using System.Collections;
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
    void Start ()
    {

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
        //Debug.Log("SelectListタップ！");
        //Debug.Log(obj.name);
    }

    public void AnimalColumnClick(GameObject obj)
    {
        //Debug.Log("AnimalListタップ！");
        //Debug.Log(obj.name);
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
                            Debug.Log("<color=red>" + SelectList1Active + "</color>");
                            if(!SelectList1Active && !AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
                            {
                                obj.transform.SetParent(Parent.transform);
                                SelectList1Active = true;
                                SelectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                                AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                //Debug.Log(SelectAnimalList[i - 1, 0]);
                                Debug.Log("1枠に挿入");
                            }
                            else if(SelectList1Active)
                            {
                                Debug.Log("オムライス");
                            }
                            break;
                        case 2:
                            if(!SelectList2Active && !AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
                            {
                                obj.transform.SetParent(Parent.transform);
                                SelectList2Active = true;
                                SelectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                                AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                //Debug.Log(SelectAnimalList[i - 1, 0]);
                                Debug.Log("2枠に挿入");
                            }
                            else if (SelectList2Active)
                            {
                                Debug.Log("からあげ");
                            }
                            break;
                        case 3:
                            if(!SelectList3Active && !AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
                            {
                                obj.transform.SetParent(Parent.transform);
                                SelectList3Active = true;
                                SelectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                                AnimalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                //Debug.Log(SelectAnimalList[i - 1, 0]);
                                Debug.Log("3枠に挿入");
                            }
                            else if (SelectList3Active)
                            {
                                Debug.Log("カレーライス");
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
            int strlength = obj.name.Length;
            int BoxNum = int.Parse(obj.name.Substring(strlength - 1));
            Debug.Log("<color=red>" + BoxNum + "</color>");
            switch(BoxNum)
            {
                case 1:
                    SelectList1Active = false;
                    Debug.Log("List1_false");
                    break;
                case 2:
                    SelectList2Active = false;
                    Debug.Log("List2_false");
                    break;
                case 3:
                    SelectList3Active = false;
                    Debug.Log("List3_false");
                    break;
                default:
                    Debug.Log("（´・ω・｀）");
                    break;
            }
            GameObject Box = GameObject.Find("Canvas/AnimalList/Column" + int.Parse(child.name.Substring(child.name.Length - 1)) + "");
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
            for(int i = 0; i < 3; i++)
            {
                Debug.Log(SelectAnimalList[i, 0]);
            }
            return true;
        }
        return false;
    }
}
