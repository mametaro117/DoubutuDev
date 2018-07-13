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
    
    bool[] WeaponListsActive = new bool[10];

    public void EraseToolTip()
    {
        GameObject Manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager TTM = Manager.GetComponent<ToolTipsManager>();
        TTM.EraseToolTips();
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
        Parent = obj.transform.root.gameObject;
        Vector2 UiPos = Parent.transform.position;
        float Diff_x = TapPos.x - UiPos.x;
        float Diff_y = TapPos.y - UiPos.y;
        
        var RectTransform = obj.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2();
        RectTransform.anchoredPosition = ResetPos;
    }
}

