using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenechenge : MonoBehaviour {

    [SerializeField]
    private Text text;

    private bool isPlay = false;
    
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    int[] AnimalList = new int[3];
    GameObject[] Animals = new GameObject[3];

    public void ChengeScene(int scenenum)
    {
        Debug.Log("TAP");
        if (!isPlay)
        {
            if(scenenum == 3)
            {
                AnimalChoiceManager Manager = GetComponent<AnimalChoiceManager>();
                if(Manager.AnimalSetCheck())
                {
                    //SelectAnimalListを渡す
                    for (int i = 0; i < 3; i++)
                    {
                        AnimalList[i] = Manager.SelectAnimalList[i, 0];
                        Animals[i] = Manager.Animals[i];
                        Debug.Log(Animals[i]);
                    }
                    //StartCoroutine(ChengeSceneCol(scenenum));
                    Change_Screen();
                }
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
    }

    GameObject _animal = null;
    GameObject parent = null;
    Vector3 ResetPos = new Vector3(0, 0, 0);
    Vector3 ResetScale = new Vector3(1, 1, 1);

    private void AnimalColumnMove()
    {
        for(int i = 0; i < 3; i++)
        {
            parent = GameObject.Find("Canvas/Weapon_List/Tap_Weapon/Tap_Check" + (i + 1));
            _animal = Instantiate(Animals[i]);
            Debug.Log(parent);
            Debug.Log(_animal);
            _animal.transform.SetParent(parent.transform);
            RectTransform _Rect = _animal.GetComponent<RectTransform>();
            Debug.Log(_Rect.anchoredPosition);
            _Rect.anchoredPosition3D = ResetPos;
            _Rect.localScale = ResetScale;

        }
    }

    private void Change_Screen()
    {
        //動物の枠を切り替え
        AnimalColumnMove();
        // Weapon_ListにWeapon_Listを格納
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        // transformのCanvas版
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
        // ResetPosにx(0),y(-135)のVector2の値を代入
        Vector2 ResetPos = new Vector2(0, -70);
        // RectTransformにResetPosの値を入れる
        RectTransform.anchoredPosition = ResetPos;
        // GameObject(AnimalList)を見つけて非表示にする
        GameObject.Find("AnimalList").SetActive(false);
        GameObject.Find("SelectList").SetActive(false);
        // textを"ぶきせんたく"に書き換える
        text.text = "ぶきせんたく";
    }
}
