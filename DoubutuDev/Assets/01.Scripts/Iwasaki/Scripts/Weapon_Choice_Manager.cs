using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon_Choice_Manager : MonoBehaviour
{
    [SerializeField]
    Sprite Weapon1_Sword;
    [SerializeField]
    Sprite Weapon2_Axe;
    [SerializeField]
    Sprite Weapon3_Shield;
    [SerializeField]
    Sprite Weapon4_Arrow;



    bool SelectListWeapon1Active;
    bool SelectListWeapon2Active;
    bool SelectListWeapon3Active;

    int[,] SelectWeaponList = new int[3, 4];

    enum ChoiceState
    {
        Weapon,
        equip,
    }
    public void EraseToolTip()
    {
        GameObject Manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager TTM = Manager.GetComponent<ToolTipsManager>();
        TTM.EraseToolTips();
    }
    public void SelectColumnClick(GameObject obj)
    {
        Debug.Log("SelectListタップ");
        Debug.Log(obj.name);
    }
    public void WeaponColumnClick(GameObject obj)
    {
        Debug.Log("WeaponlListタップ！");
        Debug.Log(obj.name);
    }

    public void WeaponDrag(GameObject Childobj)
    {
        GameObject ParentObj = Childobj.transform.parent.gameObject;
        Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Childobj.transform.position = TapPos;
    }

    public void EndDrag(GameObject obj)
    {
        GameObject Parent;
        Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 1; i < 4; i++)
        {
            Parent = GameObject.Find("Canvas/SelectList_Weapon/" + i + "");
            Vector2 UiPos = Parent.transform.position;
            float Diff_x = TapPos.x - UiPos.x;
            float Diff_y = TapPos.y - UiPos.y;
            if (-1f <= Diff_y && Diff_y <= 1f)
            {
                switch (i)
                {
                    case 1:
                        obj.transform.SetParent(Parent.transform);
                        SelectListWeapon1Active = true;
                        SelectWeaponList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                        Debug.Log("1枠に挿入");
                        break;
                    case 2:
                        obj.transform.SetParent(Parent.transform);
                        SelectListWeapon2Active = true;
                        SelectWeaponList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                        Debug.Log("2枠に挿入");
                        break;
                    case 3:
                        obj.transform.SetParent(Parent.transform);
                        SelectListWeapon3Active = true;
                        SelectWeaponList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1));
                        Debug.Log("3枠に挿入");
                        break;
                    default:
                        Debug.Log("何枠やねん");
                        break;
                }
            break;
            }
        }
        var RectTransform = obj.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2();
        RectTransform.anchoredPosition = ResetPos;
    }

    public void BackHome(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            int strlength = child.name.Length;
            int BoxNum = int.Parse(child.name.Substring(strlength - 1));

            switch (BoxNum)
            {
                case 1:
                    SelectListWeapon1Active = false;
                    break;
                case 2:
                    SelectListWeapon2Active = false;
                    break;
                case 3:
                    SelectListWeapon3Active = false;
                    break;
                default:
                    Debug.Log("（´・ω・｀）");
                    break;
            }
            GameObject Box = GameObject.Find("Canvas/Weaponlist/Column" + BoxNum + "");
            child.transform.SetParent(Box.transform);
            var RectTransform = child.GetComponent<RectTransform>();
            Vector2 ResetPos = new Vector2();
            RectTransform.anchoredPosition = ResetPos;
        }
    }

    public bool WeaponsetCheck()
    {
        if (SelectListWeapon1Active && SelectListWeapon2Active && SelectListWeapon3Active)
        {
            return true;
        }
        return false;
    }
}

