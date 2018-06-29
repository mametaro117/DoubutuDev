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

    public void Select_Weapon()
    {

    }

    private void Change_Screen()
    {
        // Weapon_ListにWeapon_Listを格納
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        GameObject Check_Button = GameObject.Find("Tap_Weapon");
        // transformのCanvas版
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
        var RectTransform_ = Check_Button.GetComponent<RectTransform>();
        // ResetPosにx(0),y(150)のVector2の値を代入
        Vector2 ResetPos = new Vector2(0, -150);
        Vector2 NextPos = new Vector2(0, 125);
        // RectTransformにResetPosの値を入れる
        RectTransform.anchoredPosition = ResetPos;
        RectTransform_.anchoredPosition = NextPos;
        // GameObject(AnimalList)を見つけて非表示にする
        GameObject.Find("AnimalList").SetActive(false);
        // textを"ぶきせんたく"に書き換える
        text.text = "ぶきせんたく";
    }
}
