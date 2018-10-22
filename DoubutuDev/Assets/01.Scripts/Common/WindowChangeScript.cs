using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class WindowChangeScript : MonoBehaviour{        //動物選択から武器選択に移る処理と、武器選択画面の動き
    
    //変数エリア
    public bool windowStationary = true;                //ウィンドウが止まっているかどうか
    public bool equip_Changing = false;                 //武器選択中かどうか

    [SerializeField]
    private GameObject weaponListPrefab;                //武器が入っているPrefab
    private int[] animalList = new int[3];              //選択した動物
    GameObject[] animals = new GameObject[3];           //選択した動物Obj
    public int[,] animalAndWeaponList = new int[3, 4];  //選択済の動物と武器リスト

    private Vector3 resetPos = new Vector3(0, 0, 0);    //リセット用
    private Vector3 resetScale = new Vector3(1, 1, 1);  //

    private Vector3 weaponBox_OriginalPos;              //配置の位置保持
    private Vector3 backButton_OriginalPos;
    private Vector3 activeBox_OriginalPos;
    private Vector3 tooBox_OriginalPos;                 //

    private int activeBoxNum;                           //選択中の動物Num
    public int[] activeBoxWeaponBefore = new int[3];    //選択中の動物の元武器
    private string weaponBoxPrefabName = "_Weapon_Box"; //Prefab名

    //動物選択から武器選択に移る処理
    public void AnimalChoice_End()
    {
        //ウィンドウが動いていない時
        if(windowStationary)
        {
            AudioManager.Instance.PlaySe(0);
            AnimalChoiceManager manager = GetComponent<AnimalChoiceManager>();
            if (manager.AnimalSetCheck())
            {
                animalAndWeaponList = manager.selectAnimalList;
                //SelectAnimalListを渡す
                for (int i = 0; i < 3; i++)
                {
                    animalList[i] = manager.selectAnimalList[i, 0];
                    animals[i] = manager.animals[i];
                    animalAndWeaponList[i, 0] = int.Parse(animals[i].name.Substring(animals[i].name.Length - 1)) - 1;
                }
                Change_Screen();
            }
        }
    }

    //個別武器選択
    public void Select_Weapon(GameObject obj)
    {
        int objnum = int.Parse(obj.name.Substring(obj.name.Length - 1));
        Equip_Change(objnum);
    }

    //動物アイコンを武器選択画面にセットする
    private void AnimalColumnMove()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject parent = GameObject.Find("Animal_Icon" + (i + 1));
            GameObject animal = Instantiate(animals[i]);
            animal.transform.SetParent(parent.transform);
            RectTransform rect = animal.GetComponent<RectTransform>();
            rect.anchoredPosition3D = resetPos;
            rect.localScale = resetScale;
        }
    }

    //移動
    private void Change_Screen()
    {
        AnimalColumnMove();
        StartCoroutine(WindowMove(-1));
    }

    //動物選択の枠を動かすコルーチン
    IEnumerator WindowMove(int MoveDirection, bool gameobjectsDestroy = false)
    {
        int time = 60;
        float speed = 1f;
        windowStationary = false;
        while(time >= 0)
        {
            GameObject.Find("AnimalSelectWindows").transform.position += new Vector3(speed * MoveDirection, 0, 0);
            GameObject.Find("WeaponSelectWindows1").transform.position += new Vector3(speed * MoveDirection, 0, 0);
            GameObject.Find("WeaponSelectWindows2").transform.position += new Vector3(speed * MoveDirection, 0, 0);
            time--;
            speed *= 0.9525f;
            yield return null;
        }
        windowStationary = true;
        if(gameobjectsDestroy)
        {
            Destroy(GameObject.Find("_Weapon_Box"));
            GetComponent<ShowSelectedAnimalScript>().DestroyAnimal();
        }
    }

    //武器選択のウィンドウを出す
    private void Equip_Change(int BoxNum)
    {
        if (!equip_Changing && windowStationary)
        {
            AudioManager.Instance.PlaySe(0);
            equip_Changing = true;

            //Box内武器を予め選択状態にする為の準備
            activeBoxNum = BoxNum;

            //武器リスト(Prefabから持ってくる)
            GameObject weapon_Box = Instantiate(weaponListPrefab) as GameObject;
            weapon_Box.name = weaponBoxPrefabName;
            weapon_Box.transform.SetParent(GameObject.Find("WeaponSelectWindows2").transform);
            var rectTransform_1 = weapon_Box.GetComponent<RectTransform>();
            rectTransform_1.localScale = new Vector3(1, 1, 1);
            Vector2 resetPos_1 = new Vector3(-300, -150, 0);
            rectTransform_1.anchoredPosition3D = resetPos_1;

            StartCoroutine(WindowMove(-1));

            //武器リストを選択済みにする処理を渡す
            Weapon_Choice_Manager weaponManager = weapon_Box.GetComponent<Weapon_Choice_Manager>();
            weaponManager.AlreadySelected();
        }
    }

    //武器選択のウィンドウを隠す
    public void Click_Back_Button(bool Change)
    {
        AudioManager.Instance.PlaySe(0);
        equip_Changing = false;
        GameObject weaponBox = GameObject.Find(weaponBoxPrefabName);
        Weapon_Choice_Manager weaponManager = weaponBox.GetComponent<Weapon_Choice_Manager>();

        //個別武器選択で決定を押した時
        if (Change)
        {
            GameObject Parent = GameObject.Find("Weapon_List/Select_Weapon_List" + activeBoxNum + "");
            //リストの更新
            for (int i = 1; i < 4; i++)
            {
                animalAndWeaponList[activeBoxNum - 1, i] = weaponManager.activeBoxWeaponAfter[i - 1];
                Sprite sprite = null;
                switch (animalAndWeaponList[activeBoxNum - 1, i])
                {
                    case 0:
                        sprite = weaponBox.GetComponent<Weapon_Choice_Manager>().weapon1_Sword;
                        break;
                    case 1:
                        sprite = weaponBox.GetComponent<Weapon_Choice_Manager>().weapon2_Shield;
                        break;
                    case 2:
                        sprite = weaponBox.GetComponent<Weapon_Choice_Manager>().weapon3_Arrow;
                        break;
                    case 3:
                        sprite = weaponBox.GetComponent<Weapon_Choice_Manager>().weapon4_Axe;
                        break;
                    default:
                        break;
                }
                Parent.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = sprite;
            }
        }
        //個別武器選択でキャンセルを押した時
        else
        {
            weaponManager.CancelMove();
        }
        StartCoroutine(WindowMove(1, true));
    }

    //パラメーターを渡す
    public void PassParamator()
    {
        if(windowStationary)
        {
            ChoiceParamator choiceParamator = GameObject.Find("ChoiceParamator").GetComponent<ChoiceParamator>();
            choiceParamator.SelectParamator = animalAndWeaponList;
            AudioManager.Instance.PlayBgm(1);
        }
    }
}

