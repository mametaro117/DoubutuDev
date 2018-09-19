using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipScript : MonoBehaviour
{

    public void EraseToolTip()
    {
        //　Managerを呼び出す
        GameObject Manager = GameObject.Find("ToolTipsManager");
        //　GetComponentする
        ToolTipsManager TT_Manager = Manager.GetComponent<ToolTipsManager>();
        //　ToolTiosを実行
        TT_Manager.EraseToolTips();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //EraseToolTipを呼び出す。
            EraseToolTip();
        }
    }
}
