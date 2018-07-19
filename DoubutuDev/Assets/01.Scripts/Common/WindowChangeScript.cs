using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowChangeScript : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject WeaponListPrefab;
    int[] AnimalList = new int[3];
    GameObject[] Animals = new GameObject[3];
    //↓この中に入ってますよっと
    int[,] AnimalAndWeaponList = new int[3, 4];

    public void AnimalChoice_End()
    {
        AudioManager.Instance.PlaySe(0);
        AnimalChoiceManager Manager = GetComponent<AnimalChoiceManager>();
        if(Manager.AnimalSetCheck())
        {
            AnimalAndWeaponList = Manager.SelectAnimalList;
            //SelectAnimalListを渡す
            for (int i = 0; i < 3; i++)
            {
                AnimalList[i] = Manager.SelectAnimalList[i, 0];
                Animals[i] = Manager.Animals[i];
                AnimalAndWeaponList[i, 0] = int.Parse(Animals[i].name.Substring(Animals[i].name.Length - 1)) - 1;
                //**/Debug.Log("No." + (i + 1) + "_" + AnimalAndWeaponList[i, 0] + "," + AnimalAndWeaponList[i, 1] + "," + AnimalAndWeaponList[i, 2] + "," + AnimalAndWeaponList[i, 3]);
            }
            Change_Screen();
        }
    }

    public void Select_Weapon(GameObject obj)
    {
        int objnum = int.Parse(obj.name.Substring(obj.name.Length - 1));
        //**/Debug.Log(objnum);
        Equip_Change(objnum);
    }

    GameObject _animal = null;
    GameObject parent = null;
    Vector3 ResetPos = new Vector3(0, 0, 0);
    Vector3 ResetScale = new Vector3(1, 1, 1);

    private void AnimalColumnMove()
    {
        //動物アイコンを持ってくる
        for (int i = 0; i < 3; i++)
        {
            parent = GameObject.Find("Animal_Icon" + (i + 1));
            _animal = Instantiate(Animals[i]);
            Debug.Log(_animal);
            Debug.Log(parent);
            _animal.transform.SetParent(parent.transform);
            RectTransform _Rect = _animal.GetComponent<RectTransform>();
            _Rect.anchoredPosition3D = ResetPos;
            _Rect.localScale = ResetScale;
        }
    }

    private void Change_Screen()
    {
        AnimalColumnMove();
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2(0, -70);
        RectTransform.anchoredPosition = ResetPos;
        //決定ボタン・次へボタン
        GameObject Start_Button = GameObject.Find("StartButton");
        GameObject Doubutu_Decide_Button = GameObject.Find("Doubutu_DecideButton");
        RectTransform Decide_RectTransform = Start_Button.GetComponent<RectTransform>();
        RectTransform Next_RectTransform = Doubutu_Decide_Button.GetComponent<RectTransform>();
        Vector3 tmppos = Decide_RectTransform.anchoredPosition3D;
        Decide_RectTransform.anchoredPosition3D = Next_RectTransform.anchoredPosition3D;
        Next_RectTransform.anchoredPosition3D = tmppos;
        GameObject.Find("SelectList").SetActive(false);
        GameObject.Find("AnimalList").SetActive(false);
        text.text = "ぶきせんたく";
    }

    bool Equip_Changing = false;
    Vector3 WeaponBox_OriginalPos;
    Vector3 BackButton_OriginalPos;
    Vector3 ActiveBox_OriginalPos;
    Vector3 TooBox_OriginalPos;
    int ActiveBoxNum;
    public int[] ActiveBoxWeaponBefore = new int[3];
    string WeaponBoxPrefabName = "_Weapon_Box";

    //武器選択のウィンドウを出す
    private void Equip_Change(int BoxNum)
    {
        if (!Equip_Changing)
        {
            AudioManager.Instance.PlaySe(0);
            Equip_Changing = true;
            Debug.Log("Change");

            //Box内武器を予め選択状態にする為の準備
            ActiveBoxNum = BoxNum;
            string str = "";
            for(int i = 0; i < 3; i++)
            {
                ActiveBoxWeaponBefore[i] = AnimalAndWeaponList[ActiveBoxNum - 1, i + 1];
                str = str + ActiveBoxWeaponBefore[i] + ", ";
            }
            //**/Debug.Log(str);

            /*

            //武器リスト
            GameObject Weapon_Box = GameObject.Find("Weapon_Box");
            var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
            WeaponBox_OriginalPos = RectTransform_1.anchoredPosition3D;
            Vector2 ResetPos_1 = new Vector3(-420, -50, 0);
            RectTransform_1.anchoredPosition3D = ResetPos_1;

            //*/

            GameObject _Canvas = GameObject.Find("Canvas");

            //武器リスト(Prefabから持ってくる)
            GameObject Weapon_Box = Instantiate(WeaponListPrefab) as GameObject;
            Weapon_Box.name = WeaponBoxPrefabName;
            Weapon_Box.transform.SetParent(_Canvas.transform);
            var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
            RectTransform_1.localScale = new Vector3(1, 1, 1);
            Vector2 ResetPos_1 = new Vector3(-420, -50, 0);
            RectTransform_1.anchoredPosition3D = ResetPos_1;

            //戻るボタン
            GameObject Back_Button = GameObject.Find("Back_Button");
            var RectTransform_2 = Back_Button.GetComponent<RectTransform>();
            BackButton_OriginalPos = RectTransform_2.anchoredPosition3D;
            Vector2 ResetPos_2 = new Vector3(130, -300, 0);
            RectTransform_2.anchoredPosition3D = ResetPos_2;

            //決定ボタン・次へボタン
            GameObject Decide_Button = GameObject.Find("DecideButton");
            GameObject Next_Button = GameObject.Find("StartButton");
            RectTransform Decide_RectTransform = Decide_Button.GetComponent<RectTransform>();
            RectTransform Next_RectTransform = Next_Button.GetComponent<RectTransform>();
            Vector3 tmppos = Decide_RectTransform.anchoredPosition3D;
            Decide_RectTransform.anchoredPosition3D = Next_RectTransform.anchoredPosition3D;
            Next_RectTransform.anchoredPosition3D = tmppos;

            //WeaponListから独立させる
            GameObject ActiveBox = GameObject.Find("Select_Weapon_List" + BoxNum);
            RectTransform _RectTransform = ActiveBox.GetComponent<RectTransform>();
            ActiveBox_OriginalPos = _RectTransform.anchoredPosition3D;
            ActiveBox.transform.SetParent(Weapon_Box.transform);
            _RectTransform.anchoredPosition3D = new Vector3(300, 0, 0);
            ActiveBox.transform.SetSiblingIndex(0);
            Weapon_Box.GetComponent<Weapon_Choice_Manager>().Parent = ActiveBox;

            //余ったWeaponListを隠す
            GameObject TooBox = GameObject.Find("Weapon_List");
            RectTransform Too_RectTransform = TooBox.GetComponent<RectTransform>();
            TooBox_OriginalPos = Too_RectTransform.anchoredPosition3D;
            Too_RectTransform.anchoredPosition3D = new Vector3(1000, 0, 0);

            //"ぶきせんたく"のテキストを非表示
            GameObject.Find("Canvas/Text").GetComponent<CanvasGroup>().alpha = 0;

            //武器リストを選択済みにする処理を渡す
            Weapon_Choice_Manager _Manager = Weapon_Box.GetComponent<Weapon_Choice_Manager>();
            _Manager.AlreadySelected();
        }
        else
        {
            Debug.Log("NotChange");
        }
    }

    //武器選択のウィンドウを隠す
    public void Click_Back_Button(bool Change)
    {
        AudioManager.Instance.PlaySe(0);
        Equip_Changing = false;
        Debug.Log("Change");
        GameObject Weapon_Box = GameObject.Find(WeaponBoxPrefabName);
        Weapon_Choice_Manager _Manager = Weapon_Box.GetComponent<Weapon_Choice_Manager>();

        if (Change)
        {
            Debug.Log("変更は保存されます");
            //リストの更新
            for(int i = 1; i < 4; i++)
            {
                AnimalAndWeaponList[ActiveBoxNum - 1, i] = _Manager._ActiveBoxWeaponAfter[i - 1];
                Debug.Log(AnimalAndWeaponList[ActiveBoxNum - 1, i]);
            }
        }
        else
        {
            Debug.Log("変更は保存されません");
            //表示を戻す
            _Manager.CancelMove();
        }

        /*

        //武器リスト
        GameObject Weapon_Box = GameObject.Find("Weapon_Box");
        var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
        RectTransform_1.anchoredPosition3D = WeaponBox_OriginalPos;

        //*/

        //武器リスト(Prefab版)
        Destroy(Weapon_Box);

        //戻るボタン
        GameObject Back_Button = GameObject.Find("Back_Button");
        var RectTransform_2 = Back_Button.GetComponent<RectTransform>();
        RectTransform_2.anchoredPosition3D = BackButton_OriginalPos;

        //決定ボタン・次へボタン
        GameObject Decide_Button = GameObject.Find("DecideButton");
        GameObject Next_Button = GameObject.Find("StartButton");
        RectTransform Decide_RectTransform = Decide_Button.GetComponent<RectTransform>();
        RectTransform Next_RectTransform = Next_Button.GetComponent<RectTransform>();
        Vector3 tmppos = Decide_RectTransform.anchoredPosition3D;
        Decide_RectTransform.anchoredPosition3D = Next_RectTransform.anchoredPosition3D;
        Next_RectTransform.anchoredPosition3D = tmppos;

        //WeaponListを定位置に戻す
        GameObject TooBox = GameObject.Find("Weapon_List");
        RectTransform Too_RectTransform = TooBox.GetComponent<RectTransform>();
        Too_RectTransform.anchoredPosition3D = TooBox_OriginalPos;

        //WeaponListから独立させたBoxを戻す
        GameObject ActiveBox = GameObject.Find("Select_Weapon_List" + ActiveBoxNum);
        RectTransform _RectTransform = ActiveBox.GetComponent<RectTransform>();
        ActiveBox.transform.SetParent(TooBox.transform);
        _RectTransform.anchoredPosition3D = ActiveBox_OriginalPos;
        //何故か非表示になってたので
        if(!ActiveBox.GetComponent<Image>().enabled)
        {
            ActiveBox.GetComponent<Image>().enabled = true;
        }

        GameObject.Find("Canvas/Text").GetComponent<CanvasGroup>().alpha = 1;
    }

    //パラメーターあげます
    public void PassParamator()
    {
        ChoiceParamator _ChoiceParamator = GameObject.Find("ChoiceParamator").GetComponent<ChoiceParamator>();
        _ChoiceParamator.SelectParamator = AnimalAndWeaponList;
        string str;
        for(int i1 = 0; i1 < 3; i1++)
        {
            str = "";
            for(int i2 = 0; i2 < 4; i2++)
            {
                str = str + AnimalAndWeaponList[i1, i2];
            }
            Debug.Log(str);
        }

        AudioManager.Instance.PlayBGM(1);
        //動物Id
        //0:うさぎ
        //1:ふくろう
        //2:ぞう
        //3:ぺんぎん(未実装)

        //武器Id
        //0:剣
        //1:斧(未実装)
        //2;盾
        //3:弓
        /*
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("No.1_" + _ChoiceParamator.SelectParamator[i, 0] + "," + _ChoiceParamator.SelectParamator[i, 1] + "," + _ChoiceParamator.SelectParamator[i, 2] + "," + _ChoiceParamator.SelectParamator[i, 3]);
        }
        //*/
    }
}
