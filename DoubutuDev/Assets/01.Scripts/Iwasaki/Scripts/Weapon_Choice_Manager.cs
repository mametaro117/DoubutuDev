using UnityEngine;
using UnityEngine.UI;

public class Weapon_Choice_Manager : MonoBehaviour {    //武器選択

    //変数エリア
    public Sprite weapon1_Sword;                        //武器画像
    public Sprite weapon2_Shield;                       
    public Sprite weapon3_Arrow;                        
    public Sprite weapon4_Axe;                          //

    [SerializeField]
    private bool[] weaponListsActive = new bool[4];     //どの武器が使われているかの配列
    [SerializeField]
    public int[] activeBoxWeaponAfter = new int[3];     //選択後の武器配列

    public GameObject parent;                           //動物脇のBox
    private int beforeObjNum;                           //入れ替え用
    private int afterObjNum;                            //
    private Sprite _sprite;                             //武器画像入れ
    private bool startDrag = true;                      //ドラッグしているかどうか

    //武器選択を表示する時に、選択済の武器を動物脇のBoxに入れる処理
    public void AlreadySelected()
    {
        WindowChangeScript window = GameObject.Find("ClickManager").GetComponent<WindowChangeScript>();
        weaponListsActive = new bool[4];
        activeBoxWeaponAfter = window.activeBoxWeaponBefore.Clone() as int[];
        for (int i = 0; i < 3; i++)
        {
            int weapon_Num = window.activeBoxWeaponBefore[i];
            weaponListsActive[weapon_Num] = true;
            transform.GetChild(weapon_Num).GetChild(0).SetSiblingIndex(1);
            switch (weapon_Num)
            {
                case 0:
                    _sprite = weapon1_Sword;
                    break;
                case 1:
                    _sprite = weapon2_Shield;
                    break;
                case 2:
                    _sprite = weapon3_Arrow;
                    break;
                case 3:
                    _sprite = weapon4_Axe;
                    break;
                default:
                    break;
            }
            parent = GameObject.Find("SelectingBox");
            parent.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = _sprite;
        }
    }
    
    //
    public void CancelMove()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(activeBoxWeaponAfter[i] + 1).GetChild(0).SetSiblingIndex(1);
        }
    }

    //ツールチップを消去する
    public void EraseToolTip()
    {
        GameObject manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager ttm = manager.GetComponent<ToolTipsManager>();
        ttm.EraseToolTips();
    }

    //ドラッグ処理
    public void WeaponDrag(GameObject Childobj)
    {
        if(!weaponListsActive[int.Parse(Childobj.name.Substring(Childobj.name.Length - 1)) - 1] && GameObject.Find("ClickManager").GetComponent<WindowChangeScript>().windowStationary)
        {
            if (startDrag)
            {
                AudioManager.Instance.PlaySe(1);
                startDrag = false;
            }
            Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Childobj.transform.position = TapPos;
        }
    }

    //指を離した処理
    public void EndDrag(GameObject obj)
    {
        if (GameObject.Find("ClickManager").GetComponent<WindowChangeScript>().windowStationary)
        {
            startDrag = true;
            Vector2 TapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!weaponListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
            {
                for (int i = 1; i < 4; i++)
                {
                    GameObject Target = parent.transform.GetChild(i - 1).gameObject;
                    Vector2 UiPos = Target.transform.position;
                    float Diff_x = TapPos.x - UiPos.x;
                    float Diff_y = TapPos.y - UiPos.y;
                    afterObjNum = -1;
                    //武器の枠に入っていたら
                    if (-0.6f <= Diff_x && Diff_x <= 0.6f)
                    {
                        if (-0.6f <= Diff_y && Diff_y <= 0.6f)
                        {
                            AudioManager.Instance.PlaySe(2);
                            //入れ替え
                            beforeObjNum = activeBoxWeaponAfter[i - 1];
                            afterObjNum = int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1;
                            activeBoxWeaponAfter[i - 1] = afterObjNum;
                            weaponListsActive[beforeObjNum] = false;
                            weaponListsActive[afterObjNum] = true;
                            //アイコンの灰色転換
                            transform.GetChild(beforeObjNum).GetChild(0).SetSiblingIndex(1);
                            transform.GetChild(afterObjNum).GetChild(0).SetSiblingIndex(1);
                            //Boxのアイコン切り替え
                            switch (afterObjNum)
                            {
                                case 0:
                                    _sprite = weapon1_Sword;
                                    break;
                                case 1:
                                    _sprite = weapon2_Shield;
                                    break;
                                case 2:
                                    _sprite = weapon3_Arrow;
                                    break;
                                case 3:
                                    _sprite = weapon4_Axe;
                                    break;
                                default:
                                    break;
                            }
                            parent.transform.GetChild(i - 1).GetChild(0).gameObject.GetComponent<Image>().sprite = _sprite;
                            break;
                        }
                    }
                }
            }
            var RectTransform = obj.GetComponent<RectTransform>();
            Vector2 ResetPos = new Vector2();
            RectTransform.anchoredPosition = ResetPos;
        }
    }
}

