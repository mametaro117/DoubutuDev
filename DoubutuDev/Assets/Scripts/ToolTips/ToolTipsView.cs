using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ToolTipsView : MonoBehaviour
{
    ToolTips tt;
    public GameObject obj;
    private int Speed;
    private int Cost;
    private string Tokusei;
    private RaycastHit2D hit;

    private void Start()
    {
        Speed = GameObject.Find("Animal_Data").GetComponent<ToolTips>().speed;
        Cost = GameObject.Find("Animal_Data").GetComponent<ToolTips>().cost;
        Tokusei = GameObject.Find("Animal_Data").GetComponent<ToolTips>().tokusei;
        //Debug.Log(Speed);
    }
    private void Update()
    {
     

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);
            Debug.Log(Speed);
            Debug.Log(Cost);
            Debug.Log(Tokusei);
            Debug.Log(hit.collider);
        }
    }
    
}
