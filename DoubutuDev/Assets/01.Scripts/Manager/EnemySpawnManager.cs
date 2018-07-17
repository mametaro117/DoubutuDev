using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private Transform[] SpawnPositions = new Transform[6];

    [SerializeField]
    float interval = 5f;

    float stacktime;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PowerCheck();
        }

        stacktime += Time.deltaTime;
        //Debug.Log(stacktime);
        //Debug.Log(GetComponent<TimeManager>().GetIsReady());
        if (stacktime >= interval && GetComponent<TimeManager>().GetIsReady() == true)
        {
            stacktime = 0;
            EnemyInstance();
        }
    }

    public void EnemyInstance()
    {
        PowerCheck();
        int SpawnPos = SetSpawnPos();
        if(SpawnPos == -1)
        {
            SpawnPos = Random.Range(0, 5);
        }
        Instantiate(EnemyPrefab, new Vector3(SpawnPositions[SpawnPos].position.x, SpawnPositions[SpawnPos].position.y, SpawnPositions[SpawnPos].position.z), SpawnPositions[SpawnPos].rotation);
    }

    private int SetSpawnPos()
    {
        List<int> RaneNum = new List<int>();
        int MaxPow = 0;
        for(int i = 0; i < LanePowers.Length; i++)
        {
            //パワーを比較
            //最大値より大きければリストを初期化してからレーン番号を入れる
            if(LanePowers[i] > MaxPow)
            {
                MaxPow = LanePowers[i];
                RaneNum = new List<int>();
                RaneNum.Add(i);
            }
            //最大値と一緒ならレーン番号を追加する
            else if(LanePowers[i] == MaxPow)
            {
                RaneNum.Add(i);
            }
        }
        //*/
        string str = "";
        for(int i = 0; i < RaneNum.Count; i++)
        {
            str = str + i + "_";
        }
        Debug.Log(str);
        //*/
        //複数ある場合はランダムで返す
        return RaneNum[Random.Range(0, RaneNum.Count)];
    }

    RaycastHit2D[] hit;

    [SerializeField]
    int[] LanePowers = new int[5];

    public void PowerCheck()
    {
        for(int i = 0; i < 5; i++)
        {
            LanePowers[i] = 0;  //  初期化
            hit = Physics2D.BoxCastAll(SpawnPositions[i].position, new Vector2(1, 1.6875f), 0f, Vector2.right, 10f);
            Debug.Log("BoxCast:" + hit.Length);
            for (int k = 0; hit.Length > k; k++)
            {
                Debug.Log(k + "番目:"  + hit[k].collider.name);
                if (hit[k].collider.tag == "Animal")
                    LanePowers[i]++;
                Debug.Log(SpawnPositions[i].position);
            }
        }
    }
}
