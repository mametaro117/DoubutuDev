using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenechenge : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private bool isPlay = false;

    int[] AnimalList = new int[3];
    GameObject[] Animals = new GameObject[3];
    int[,] AnimalAndWeaponList = new int[3, 4];
    bool WindowChange = true;

    public void ChengeScene(int scenenum)
    {
        //Debug.Log("TAP");
        if (!isPlay)
        {
            if(scenenum == 3 && WindowChange)
            {
                WindowChange = false;
                AnimalChoiceManager Manager = GetComponent<AnimalChoiceManager>();
                if(Manager.AnimalSetCheck())
                {
                    AnimalAndWeaponList = Manager.SelectAnimalList;
                    //SelectAnimalListを渡す
                    for (int i = 0; i < 3; i++)
                    {
                        AnimalList[i] = Manager.SelectAnimalList[i, 0];
                        Animals[i] = Manager.Animals[i];
                        AnimalAndWeaponList[i, 0] = int.Parse(Animals[i].name.Substring(Animals[i].name.Length - 1));
                        //Debug.Log(Animals[i]);
                    }
                    //StartCoroutine(ChengeSceneCol(scenenum));
                    Change_Screen();
                }
            }
            else if(scenenum == 3 && !WindowChange)
            {
                //AnimalAndWeaponListに動物idと武器idが入ってます
                for (int i = 0; i < 3; i++)
                {
                    Debug.Log("No.1_" + AnimalAndWeaponList[i, 0] + "," + AnimalAndWeaponList[i, 1] + "," + AnimalAndWeaponList[i, 2] + "," + AnimalAndWeaponList[i, 3]);
                }
                StartCoroutine(ChengeSceneCol(scenenum));
            }
            else
            {
                StartCoroutine(ChengeSceneCol(scenenum));
            }
        }
    }

    IEnumerator ChengeSceneCol(int scenenum)
    {
        Debug.Log("Chenge中");
        isPlay = true;
        FadeManager.Instance.LoadScene(scenenum, 1f);
        yield return new WaitForSeconds(1f);
        isPlay = false;
        yield break;
    }

    public void Select_Weapon(GameObject obj)
    {
        int objnum = int.Parse(obj.name.Substring(obj.name.Length - 1));
        Debug.Log(objnum);
<<<<<<< HEAD
        Equip_Change(objnum);
=======
        Equip_Change();
    }
    public void Click_Back_Bottun()
    {

>>>>>>> origin/Hirokawa
    }

    GameObject _animal = null;
    GameObject parent = null;
    Vector3 ResetPos = new Vector3(0, 0, 0);
    Vector3 ResetScale = new Vector3(1, 1, 1);

    private void AnimalColumnMove()
    {
        //動物アイコンを持ってくる
        for(int i = 0; i < 3; i++)
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
<<<<<<< HEAD
        AnimalColumnMove();
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2(0, -70);
        RectTransform.anchoredPosition = ResetPos;
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

    //武器選択のウィンドウを出す
    private void Equip_Change(int BoxNum)
    {
        if(!Equip_Changing)
        {
            Equip_Changing = true;
            Debug.Log("Change_" + Equip_Changing);

            ActiveBoxNum = BoxNum;

            //武器リスト
            GameObject Weapon_Box = GameObject.Find("Weapon_Box");
            var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
            WeaponBox_OriginalPos = RectTransform_1.anchoredPosition3D;
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

            //余ったWeaponListを隠す
            GameObject TooBox = GameObject.Find("Weapon_List");
            RectTransform Too_RectTransform = TooBox.GetComponent<RectTransform>();
            TooBox_OriginalPos = Too_RectTransform.anchoredPosition3D;
            Too_RectTransform.anchoredPosition3D = new Vector3(1000, 0, 0);

            //"ぶきせんたく"のテキストを非表示
            GameObject.Find("Canvas/Text").GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            Debug.Log("NotChange");
        }
    }

    //武器選択のウィンドウを隠す
    public void Click_Back_Button()
    {
        Equip_Changing = false;
        Debug.Log("Change_" + Equip_Changing);

        //武器リスト
        GameObject Weapon_Box = GameObject.Find("Weapon_Box");
        var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
        RectTransform_1.anchoredPosition3D = WeaponBox_OriginalPos;
=======
<<<<<<< HEAD
        //動物の枠を切り替え
        AnimalColumnMove();
        // Weapon_ListにWeapon_Listを格納
=======
>>>>>>> origin/iwasaki
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
<<<<<<< HEAD
        // ResetPosにx(0),y(-135)のVector2の値を代入
        Vector2 ResetPos = new Vector2(0, -70);
        // RectTransformにResetPosの値を入れる
=======
        Vector2 ResetPos = new Vector2(0, -150);
>>>>>>> origin/iwasaki
        RectTransform.anchoredPosition = ResetPos;
        GameObject.Find("AnimalList").SetActive(false);
<<<<<<< HEAD
        GameObject.Find("SelectList").SetActive(false);
        // textを"ぶきせんたく"に書き換える
=======
>>>>>>> origin/iwasaki
        text.text = "ぶきせんたく";
    }

    private void Equip_Change()
    {
        //武器リスト
        GameObject Weapon_Box = GameObject.Find("Weapon_Box");
        var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
        Vector2 ResetPos_1 = new Vector2(-420, -50);
        RectTransform_1.anchoredPosition = ResetPos_1;
>>>>>>> origin/Hirokawa

        //戻るボタン
        GameObject Back_Button = GameObject.Find("Back_Button");
        var RectTransform_2 = Back_Button.GetComponent<RectTransform>();
<<<<<<< HEAD
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

        GameObject.Find("Canvas/Text").GetComponent<CanvasGroup>().alpha = 1;


=======
        Vector2 ResetPos_2 = new Vector2(-500, 280);
        RectTransform_2.anchoredPosition = ResetPos_2;


        GameObject.Find("Text").GetComponent<CanvasGroup>().alpha = 0;
>>>>>>> origin/Hirokawa
    }
}
