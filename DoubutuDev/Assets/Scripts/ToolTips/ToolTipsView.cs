using UnityEngine;

public class ToolTipsView : MonoBehaviour
{
    [HideInInspector]
    public GameObject obj;
    public int SpeedOwl;
    public int CostOwl;
    public string TokuseiOwl;

    public int SpeedElephant;
    public int CostElephant;
    public string TokuseiElephant;

    public int SpeedRabbit;
    public int CostRabbit;
    public string TokuseiRabbit;

    public RaycastHit2D hit;
    private GameObject Obj;
    private bool ObjCheck;

    private void Start()
    {
        Owl_Data();
        Elephant_Data();
        Rabbit_Data();

        Obj = GameObject.FindGameObjectWithTag("bird");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);
            if (Obj.transform.CompareTag("bird"))
            {
                ObjCheck = true;
                if (ObjCheck == true)
                {
                    Debug.Log("動物:" + hit.collider.CompareTag("bird"));
                    Debug.Log("速度:" + SpeedOwl);
                    Debug.Log("コスト:" + CostOwl);
                    Debug.Log("特性:" + TokuseiOwl);
                }
            }
        }
    }
    private void Owl_Data()
    {
        SpeedOwl = GameObject.Find("Animal_Data_Owl").GetComponent<ToolTips_Owl>().speed;
        CostOwl = GameObject.Find("Animal_Data_Owl").GetComponent<ToolTips_Owl>().cost;
        TokuseiOwl = GameObject.Find("Animal_Data_Owl").GetComponent<ToolTips_Owl>().tokusei;
    }
    private void Elephant_Data()
    {
        SpeedElephant = GameObject.Find("Animal_Data_Elephant").GetComponent<ToolTips_Elephant>().speed;
        CostElephant = GameObject.Find("Animal_Data_Elephant").GetComponent<ToolTips_Elephant>().cost;
        TokuseiElephant = GameObject.Find("Animal_Data_Elephant").GetComponent<ToolTips_Elephant>().tokusei;
    }
    private void Rabbit_Data()
    {
        SpeedRabbit = GameObject.Find("Animal_Data_Rabbit").GetComponent<ToolTips_Rabbit>().speed;
        CostRabbit = GameObject.Find("Animal_Data_Rabbit").GetComponent<ToolTips_Rabbit>().cost;
        TokuseiRabbit = GameObject.Find("Animal_Data_Rabbit").GetComponent<ToolTips_Rabbit>().tokusei;
    }
}
