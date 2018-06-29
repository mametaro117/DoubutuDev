using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenechenge : MonoBehaviour {

    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject Obj;

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

    private void Change_Screen()
    {
        // Weapon_ListにWeapon_Listを格納
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        // transformのCanvas版
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
        // ResetPosにx(0),y(150)のVector2の値を代入
        Vector2 ResetPos = new Vector2(0, -150);
        // RectTransformにResetPosの値を入れる
        RectTransform.anchoredPosition = ResetPos;
        // GameObject(AnimalList)を見つけて非表示にする
        GameObject.Find("AnimalList").SetActive(false);
        // textを"ぶきせんたく"に書き換える
        text.text = "ぶきせんたく";

    }
}
