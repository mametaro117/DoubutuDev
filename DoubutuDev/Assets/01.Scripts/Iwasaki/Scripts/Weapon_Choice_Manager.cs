using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Weapon_Choice_Manager : MonoBehaviour
{
    public Sprite Weapon1_Sword;
    public Sprite Weapon2_Shield;
    public Sprite Weapon3_Arrow;
    public Sprite Weapon4_Axe;

    [SerializeField]
    private bool[] WeaponListsActive = new bool[10];
    [SerializeField]
    private int[] _ActiveBoxWeaponBefore = new int[3];
    public int[] _ActiveBoxWeaponAfter = new int[3];

    public void AlreadySelected()
    {
        WindowChangeScript _Window = GameObject.Find("ClickManager").GetComponent<WindowChangeScript>();
        WeaponListsActive = new bool[10];
        /*
        //_ActiveBoxWeaponBefore = _Window.ActiveBoxWeaponBefore;
        //_ActiveBoxWeaponAfter = _Window.ActiveBoxWeaponBefore;Clone()
        //_Window.ActiveBoxWeaponBefore.CopyTo(_ActiveBoxWeaponBefore, 0);
        //_Window.ActiveBoxWeaponBefore.CopyTo(_ActiveBoxWeaponAfter, 0);
        //Array.Copy(_Window.ActiveBoxWeaponBefore, _ActiveBoxWeaponBefore, 3);
        //Array.Copy(_Window.ActiveBoxWeaponBefore, _ActiveBoxWeaponAfter, 3);
        */
        //やっぱCloneだね
        _ActiveBoxWeaponBefore = _Window.ActiveBoxWeaponBefore.Clone() as int[];
        _ActiveBoxWeaponAfter = _Window.ActiveBoxWeaponBefore.Clone() as int[];
        for (int i = 0; i < 3; i++)
        {
            int Weapon_Num = _Window.ActiveBoxWeaponBefore[i];
            //Debug.Log(Weapon_Num);
            WeaponListsActive[Weapon_Num] = true;
            //Debug.Log(transform.GetChild(Weapon_Num + 1));
            transform.GetChild(Weapon_Num).GetChild(0).SetSiblingIndex(1);
            switch (Weapon_Num)
            {
                case 0:
                    _sprite = Weapon1_Sword;
                    break;
                case 1:
                    _sprite = Weapon2_Shield;
                    break;
                case 2:
                    _sprite = Weapon3_Arrow;
                    break;
                case 3:
                    _sprite = Weapon4_Axe;
                    break;
                default:
                    break;
            }
            Parent = GameObject.Find("SelectingBox");
            Parent.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = _sprite;
        }
    }

    public void CancelMove()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(_ActiveBoxWeaponAfter[i] + 1).GetChild(0).SetSiblingIndex(1);
        }
    }

    public void EraseToolTip()
    {
        GameObject Manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager TTM = Manager.GetComponent<ToolTipsManager>();
        TTM.EraseToolTips();
    }

    bool StartDrag = true;

    public void WeaponDrag(GameObject Childobj)
    {
        if(!WeaponListsActive[int.Parse(Childobj.name.Substring(Childobj.name.Length - 1)) - 1] && GameObject.Find("ClickManager").GetComponent<WindowChangeScript>().WindowStationary)
        {
            if (StartDrag)
            {
                AudioManager.Instance.PlaySe(1);
                StartDrag = false;
            }
            Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Childobj.transform.position = TapPos;
        }
    }

    public GameObject Parent;
    int BeforeObjNum;
    int AfterObjNum;
    Sprite _sprite = null;

    public void EndDrag(GameObject obj)
    {
        if (GameObject.Find("ClickManager").GetComponent<WindowChangeScript>().WindowStationary)
        {
            StartDrag = true;
            Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!WeaponListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
            {
                for (int i = 1; i < 4; i++)
                {
                    string str = "SelectColumn" + i;
                    GameObject Target = Parent.transform.GetChild(i - 1).gameObject;
                    Vector2 UiPos = Target.transform.position;
                    float Diff_x = TapPos.x - UiPos.x;
                    float Diff_y = TapPos.y - UiPos.y;
                    AfterObjNum = -1;
                    if (-0.6f <= Diff_x && Diff_x <= 0.6f)
                    {
                        if (-0.6f <= Diff_y && Diff_y <= 0.6f)
                        {
                            Debug.Log("BoxNum_" + i);
                            AudioManager.Instance.PlaySe(2);
                            BeforeObjNum = _ActiveBoxWeaponAfter[i - 1];
                            AfterObjNum = int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1;
                            _ActiveBoxWeaponAfter[i - 1] = AfterObjNum;
                            //Debug.Log("<color=red>" + _ActiveBoxWeaponBefore[i - 1] + ", " + _ActiveBoxWeaponAfter[i - 1] + "</color>");
                            WeaponListsActive[BeforeObjNum] = false;
                            WeaponListsActive[AfterObjNum] = true;
                            //アイコンの灰色転換
                            transform.GetChild(BeforeObjNum).GetChild(0).SetSiblingIndex(1);
                            transform.GetChild(AfterObjNum).GetChild(0).SetSiblingIndex(1);
                            //Boxのアイコン切り替え
                            switch (AfterObjNum)
                            {
                                case 0:
                                    _sprite = Weapon1_Sword;
                                    break;
                                case 1:
                                    _sprite = Weapon2_Shield;
                                    break;
                                case 2:
                                    _sprite = Weapon3_Arrow;
                                    break;
                                case 3:
                                    _sprite = Weapon4_Axe;
                                    break;
                                default:
                                    break;
                            }
                            Parent.transform.GetChild(i - 1).GetChild(0).gameObject.GetComponent<Image>().sprite = _sprite;
                            break;
                        }
                    }
                }
            }
            Debug.Log("枠検索終了");
            var RectTransform = obj.GetComponent<RectTransform>();
            Vector2 ResetPos = new Vector2();
            RectTransform.anchoredPosition = ResetPos;
        }
    }
}

