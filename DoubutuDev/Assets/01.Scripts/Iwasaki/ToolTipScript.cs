using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipScript : MonoBehaviour
{

    public void EraseToolTip()
    {
        GameObject Manager = GameObject.Find("ToolTipsManager");
        ToolTipsManager TT_Manager = Manager.GetComponent<ToolTipsManager>();
        TT_Manager.EraseToolTips();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EraseToolTip();
        }
    }
}
