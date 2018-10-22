using UnityEngine;

public class AnimalChoiceManager : MonoBehaviour {  //動物選択

    //変数エリア
    private bool selectList1Active;                         //枠に動物が入っているかどうか
    private bool selectList2Active;
    private bool selectList3Active;                         //
    
    private bool[] animalListsActive = new bool[4];         //動物が選択済がどうか
    
    //動物と武器の選択配列
    // [動物id, 武器id1, 武器id2, 武器id3]
    public int[,] selectAnimalList = { { 99, 0, 1, 2 },{ 99, 0, 1, 2 },{ 99, 0, 1, 2 } };

    public GameObject[] animals = new GameObject[3];        //選択済の動物Obj入れ

    bool startDrag = true;                                  //ドラッグしているか

    //ツールチップを消す
    public void EraseToolTip()
    {
        GameObject Manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager TT_Manager = Manager.GetComponent<ToolTipsManager>();
        TT_Manager.EraseToolTips();
    }

    //ドラッグ開始処理
    public void AnimalDrag(GameObject ChildObj)
    {
        if(GetComponent<WindowChangeScript>().windowStationary)
        {
            if (!animalListsActive[int.Parse(ChildObj.name.Substring(ChildObj.name.Length - 1)) - 1])
            {
                if (startDrag)
                {
                    AudioManager.Instance.PlaySe(1);
                    startDrag = false;
                }
                Vector2 tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ChildObj.transform.position = tapPos;
            }
        }
    }

    //ドラッグ終了処理
    public void EndDrag(GameObject obj)
    {
        AudioManager.Instance.PlaySe(1);
        startDrag = true;
        GameObject parent;
        Vector2 tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //ドラッグし終わった時に別の枠内に入っていたら
        for (int i = 1; i < 4; i++)
        {
            parent = GameObject.Find("SelectList/SelectColumn" + i + "");
            Vector2 uiPos = parent.transform.position;
            float diff_x = tapPos.x - uiPos.x;
            float diff_y = tapPos.y - uiPos.y;
            if(-1f <= diff_x && diff_x <= 1f)
            {
                if(-1f <= diff_y && diff_y <= 1f)
                {
                    AudioManager.Instance.PlaySe(2);
                    //枠の子孫とする
                    switch (i)
                    {
                        case 1:                            
                            if(!selectList1Active && !animalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
                            {
                                obj.transform.SetParent(parent.transform);
                                selectList1Active = true;
                                selectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1;
                                animalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                Debug.Log("1枠に挿入");
                                animals[i - 1] = obj;
                            }
                            break;
                        case 2:
                            if(!selectList2Active && !animalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
                            {
                                obj.transform.SetParent(parent.transform);
                                selectList2Active = true;
                                selectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1;
                                animalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                Debug.Log("2枠に挿入");
                                animals[i - 1] = obj;
                            }
                            break;
                        case 3:
                            if(!selectList3Active && !animalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1])
                            {
                                obj.transform.SetParent(parent.transform);
                                selectList3Active = true;
                                selectAnimalList[i - 1, 0] = int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1;
                                animalListsActive[int.Parse(obj.name.Substring(obj.name.Length - 1)) - 1] = true;
                                Debug.Log("3枠に挿入");
                                animals[i - 1] = obj;
                            }
                            break;
                        default:
                            Debug.Log("何枠か分からない");
                            break;
                    }
                    break;
                }
            }
        }

        //親のboxに戻る
        var RectTransform = obj.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2();
        RectTransform.anchoredPosition = ResetPos;
    }

    //入った枠をクリックで、元の枠に戻る
    public void BackHome(GameObject obj)
    {
        if(GetComponent<WindowChangeScript>().windowStationary)
        {
            foreach (Transform child in obj.transform)
            {
                animalListsActive[int.Parse(child.name.Substring(child.name.Length - 1)) - 1] = false;
                int strlength = obj.name.Length;
                int boxNum = int.Parse(obj.name.Substring(strlength - 1));
                switch (boxNum)
                {
                    case 1:
                        selectList1Active = false;
                        break;
                    case 2:
                        selectList2Active = false;
                        break;
                    case 3:
                        selectList3Active = false;
                        break;
                    default:
                        break;
                }
                GameObject box = GameObject.Find("AnimalList/Column" + int.Parse(child.name.Substring(child.name.Length - 1)) + "");
                child.transform.SetParent(box.transform);
                var rectTransform = child.GetComponent<RectTransform>();
                Vector2 resetPos = new Vector2();
                rectTransform.anchoredPosition = resetPos;
            }
        }
    }

    //決定ボタンが押された時に、全部の枠に動物がセットされているかどうか
    public bool AnimalSetCheck()
    {
        if(selectList1Active && selectList2Active && selectList3Active)
        {
            return true;
        }
        else
        {
            ToolTipsManager _Tool = GameObject.Find("ToolTipsManager").GetComponent<ToolTipsManager>();
            _Tool.Show_AttentionToolTips();
            return false;
        }
    }
}

