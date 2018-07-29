using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowChangeScript : MonoBehaviour
{
    //ウィンドウが止まっているかどうか
    public bool WindowStationary = true;
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject WeaponListPrefab;
    int[] AnimalList = new int[3];
    GameObject[] Animals = new GameObject[3];
    //↓この中に入ってますよっと
    public int[,] AnimalAndWeaponList = new int[3, 4];

    public void AnimalChoice_End()
    {
        if(WindowStationary)
        {
            AudioManager.Instance.PlaySe(0);
            AnimalChoiceManager Manager = GetComponent<AnimalChoiceManager>();
            if (Manager.AnimalSetCheck())
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
            //Debug.Log(_animal);
            //Debug.Log(parent);
            _animal.transform.SetParent(parent.transform);
            RectTransform _Rect = _animal.GetComponent<RectTransform>();
            _Rect.anchoredPosition3D = ResetPos;
            _Rect.localScale = ResetScale;
        }
    }

    private void Change_Screen()
    {
        AnimalColumnMove();
        StartCoroutine(WindowMove(-1));
    }

    private int time = 90;
    private float speed = 0.85f;
    [SerializeField]
    private Vector3 pospos;
    [SerializeField]
    private Vector3 rectpospos;

    //動物選択の枠を動かすコルーチン
    IEnumerator WindowMove(int MoveDirection, bool gameobjectsDestroy = false)
    {
        time = 90;
        speed = 0.85f;
        Debug.Log("コルーチン開始");
        WindowStationary = false;
        while(time >= 0)
        {
            GameObject.Find("AnimalSelectWindows").transform.position += new Vector3(speed * MoveDirection, 0, 0);
            GameObject.Find("WeaponSelectWindows1").transform.position += new Vector3(speed * MoveDirection, 0, 0);
            GameObject.Find("WeaponSelectWindows2").transform.position += new Vector3(speed * MoveDirection, 0, 0);
            time--;
            speed *= 0.9525f;
            yield return null;
        }
        Debug.Log("コルーチン終了");
        pospos = GameObject.Find("AnimalSelectWindows").transform.position;
        rectpospos = GameObject.Find("AnimalSelectWindows").GetComponent<RectTransform>().anchoredPosition3D;
        WindowStationary = true;
        if(gameobjectsDestroy)
        {
            Destroy(GameObject.Find("_Weapon_Box"));
            GetComponent<ShowSelectedAnimalScript>().DestroyAnimal();
        }
    }

    public bool Equip_Changing = false;
    Vector3 WeaponBox_OriginalPos;
    Vector3 BackButton_OriginalPos;
    Vector3 ActiveBox_OriginalPos;
    Vector3 TooBox_OriginalPos;
    private int ActiveBoxNum;
    public int[] ActiveBoxWeaponBefore = new int[3];
    string WeaponBoxPrefabName = "_Weapon_Box";

    //武器選択のウィンドウを出す
    private void Equip_Change(int BoxNum)
    {
        if (!Equip_Changing && WindowStationary)
        {
            AudioManager.Instance.PlaySe(0);
            Equip_Changing = true;
            //Debug.Log("Change");

            //Box内武器を予め選択状態にする為の準備
            ActiveBoxNum = BoxNum;
            string str = "";
            for(int i = 0; i < 3; i++)
            {
                ActiveBoxWeaponBefore[i] = AnimalAndWeaponList[ActiveBoxNum - 1, i + 1];
                str = str + ActiveBoxWeaponBefore[i] + ", ";
            }
            //**/Debug.Log(str);

            GameObject _Canvas = GameObject.Find("Canvas");

            //武器リスト(Prefabから持ってくる)
            GameObject Weapon_Box = Instantiate(WeaponListPrefab) as GameObject;
            Weapon_Box.name = WeaponBoxPrefabName;
            Weapon_Box.transform.SetParent(GameObject.Find("WeaponSelectWindows2").transform);
            var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
            RectTransform_1.localScale = new Vector3(1, 1, 1);
            Vector2 ResetPos_1 = new Vector3(-300, -150, 0);
            RectTransform_1.anchoredPosition3D = ResetPos_1;

            StartCoroutine(WindowMove(-1));

            //武器リストを選択済みにする処理を渡す
            Weapon_Choice_Manager _Manager = Weapon_Box.GetComponent<Weapon_Choice_Manager>();
            _Manager.AlreadySelected();
        }
        else
        {
            Debug.Log("NotChange");
        }
    }

    private GameObject Weapon_Box;
    Sprite _sprite;

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
            GameObject Parent = GameObject.Find("Weapon_List/Select_Weapon_List" + ActiveBoxNum + "");
            //リストの更新
            for (int i = 1; i < 4; i++)
            {
                AnimalAndWeaponList[ActiveBoxNum - 1, i] = _Manager._ActiveBoxWeaponAfter[i - 1];
                Debug.Log(AnimalAndWeaponList[ActiveBoxNum - 1, i]);
                switch (AnimalAndWeaponList[ActiveBoxNum - 1, i])
                {
                    case 0:
                        _sprite = Weapon_Box.GetComponent<Weapon_Choice_Manager>().Weapon1_Sword;
                        break;
                    case 1:
                        _sprite = Weapon_Box.GetComponent<Weapon_Choice_Manager>().Weapon2_Shield;
                        break;
                    case 2:
                        _sprite = Weapon_Box.GetComponent<Weapon_Choice_Manager>().Weapon3_Arrow;
                        break;
                    case 3:
                        _sprite = Weapon_Box.GetComponent<Weapon_Choice_Manager>().Weapon4_Axe;
                        break;
                    default:
                        break;
                }
                Parent.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = _sprite;
            }
        }
        else
        {
            Debug.Log("変更は保存されません");
            //表示を戻す
            _Manager.CancelMove();
        }

        StartCoroutine(WindowMove(1, true));
        
    }

    //パラメーターあげます
    public void PassParamator()
    {
        if(WindowStationary)
        {
            ChoiceParamator _ChoiceParamator = GameObject.Find("ChoiceParamator").GetComponent<ChoiceParamator>();
            _ChoiceParamator.SelectParamator = AnimalAndWeaponList;
            string str;
            for (int i1 = 0; i1 < 3; i1++)
            {
                str = "";
                for (int i2 = 0; i2 < 4; i2++)
                {
                    str = str + AnimalAndWeaponList[i1, i2];
                }
                Debug.Log(str);
            }
            AudioManager.Instance.PlayBgm(1);
        }

        //動物Id
        //0:うさぎ
        //1:ふくろう
        //2:ぞう
        //3:ぺんぎん(未実装)

        //武器Id
        //0:剣
        //1;盾
        //2:弓
        //3:斧(未実装)
        /*
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("No.1_" + _ChoiceParamator.SelectParamator[i, 0] + "," + _ChoiceParamator.SelectParamator[i, 1] + "," + _ChoiceParamator.SelectParamator[i, 2] + "," + _ChoiceParamator.SelectParamator[i, 3]);
        }
        //*/
    }
}
