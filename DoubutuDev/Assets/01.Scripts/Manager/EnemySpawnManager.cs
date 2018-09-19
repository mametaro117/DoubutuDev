using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] EnemyPrefab = new GameObject[3];

    [SerializeField]
    private Transform[] SpawnPositions = new Transform[5];

    [SerializeField]
    float interval = 5f;

    float stacktime;

    private int EnemyPowers;

    [SerializeField]
    private int TekagenEnemyPowers = 5;

    [SerializeField]
    private float DeraySpawnTime = 0.5f;
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PowerCheck();
        }
        stacktime += Time.deltaTime;
        //Debug.Log(stacktime);
        //Debug.Log(GetComponent<TimeManager>().GetIsReady());
        if(EnemyPowers - TekagenEnemyPowers >= 1)
        {
            if (stacktime >= interval + (EnemyPowers - TekagenEnemyPowers) * DeraySpawnTime && GetComponent<TimeManager>().GetIsReady() == true)
            {
                stacktime = 0;
                EnemyInstance();
            }
        }
        else
        {
            if (stacktime >= interval && GetComponent<TimeManager>().GetIsReady() == true)
            {
                stacktime = 0;
                EnemyInstance();
            }
        }
    }

    [SerializeField]
    private int kimagure = 20;

    public void EnemyInstance()
    {
        PowerCheck();
        int SpawnPos = SetSpawnPos();
        if(Random.Range(0, 100) <= kimagure)
        {
            //Debug.Log("気まぐれ");
            SpawnPos = Random.Range(0, SpawnPositions.Length);
        }
        if(SpawnPos == -1)
        {
            SpawnPos = Random.Range(0, SpawnPositions.Length);
        }
        int ran = Random.Range(0, EnemyPrefab.Length);
        Instantiate(EnemyPrefab[ran], new Vector3(SpawnPositions[SpawnPos].position.x, SpawnPositions[SpawnPos].position.y, SpawnPositions[SpawnPos].position.z), SpawnPositions[SpawnPos].rotation);
    }

    private int SetSpawnPos()
    {
        List<int> PlayerRaneNum = new List<int>();
        List<int> EnemyRaneNum = new List<int>();
        int MaxPow = 0;
        int MinPow = 0;
        int SetRaneResult = 0;
        float MaxFrontPos = 0f;
        
        for (int i = 0; i < PlayerLanePowers.Length; i++)
        {
            //パワーを比較
            //最大値より大きければリストを初期化してからレーン番号を入れる
            if (PlayerLanePowers[i] > MaxPow)
            {
                MaxPow = PlayerLanePowers[i];
                PlayerRaneNum = new List<int>();
                PlayerRaneNum.Add(i);
            }
            //最大値と一緒ならレーン番号を追加する
            else if (PlayerLanePowers[i] == MaxPow)
            {
                PlayerRaneNum.Add(i);
            }
        }
        //もし、対象のレーンが複数存在する場合、自分側のポジションと敵側のキャラ数を見て判断する
        //ここに入る条件は(恐らく)自分のキャラが何もない時なのでランダムで返す
        if(PlayerRaneNum.Count >= 5 && MaxPow == 0)
        {
            SetRaneResult = Random.Range(0, PlayerRaneNum.Count);
        }
        //複数ある時
        else if (PlayerRaneNum.Count >= 2)
        {
            MaxPow = 0;
            //敵側キャラのキャラ数を比較
            for(int i = 0; i < PlayerRaneNum.Count; i++)
            {
                if(EnemyLanePowers[PlayerRaneNum[i]] > MaxPow)
                {
                    EnemyRaneNum = new List<int>();
                    EnemyRaneNum.Add(PlayerRaneNum[i]);
                    MaxPow = EnemyLanePowers[PlayerRaneNum[i]];
                }
                else if(EnemyLanePowers[PlayerRaneNum[i]] == MaxPow)
                {
                    EnemyRaneNum.Add(PlayerRaneNum[i]);
                }
            }
            //ポジションを見る条件は、敵側キャラが同数いる時
            if(PlayerRaneNum.Count == EnemyRaneNum.Count)
            {
                for(int i = 0; i < EnemyRaneNum.Count; i++)
                {
                    if (MaxFrontPos == 0f)
                    {
                        MaxFrontPos = PlayerCharaFrontPos[PlayerRaneNum[i]];
                        SetRaneResult = PlayerRaneNum[i];
                    }
                    else
                    {
                        if(PlayerCharaFrontPos[PlayerRaneNum[i]] < MaxFrontPos)
                        {
                            MaxFrontPos = PlayerCharaFrontPos[PlayerRaneNum[i]];
                            SetRaneResult = PlayerRaneNum[i];
                        }
                    }
                }
            }
            //敵側キャラを見て、少ない所に配置
            else
            {
                MinPow = 0;
                for(int i = 0; i < PlayerRaneNum.Count; i++)
                {
                    if(i == 0)
                    {
                        EnemyRaneNum = new List<int>();
                        MinPow = EnemyLanePowers[PlayerRaneNum[i]];
                        EnemyRaneNum.Add(PlayerRaneNum[i]);
                        Debug.Log("hoge");
                    }
                    else
                    {
                        if(MinPow > EnemyLanePowers[PlayerRaneNum[i]])
                        {
                            MinPow = EnemyLanePowers[PlayerRaneNum[i]];
                            EnemyRaneNum = new List<int>();
                            EnemyRaneNum.Add(PlayerRaneNum[i]);
                        }
                        else if(MinPow == EnemyLanePowers[PlayerRaneNum[i]])
                        {
                            EnemyRaneNum.Add(PlayerRaneNum[i]);
                        }
                    }
                }
                SetRaneResult = EnemyRaneNum[Random.Range(0, EnemyRaneNum.Count)];
            }
        }
        //そこに配置
        else
        {
            SetRaneResult = PlayerRaneNum[Random.Range(0, PlayerRaneNum.Count)];
        }
        string str = "Max is_";
        for(int i = 0; i < PlayerRaneNum.Count; i++)
        {
            str = str + i + "_";
        }
        str = str + "同パワー数:" + PlayerRaneNum.Count;

        //複数ある場合はランダムで返す
        return SetRaneResult;
    }
    RaycastHit2D[] hit;

    [SerializeField]
    int[] PlayerLanePowers = new int[5];
    int[] EnemyLanePowers = new int[5];
    float[] PlayerCharaFrontPos = new float[5];
    float[] EnemyCharaFrontPos = new float[5];

    public void PowerCheck()
    {
        for(int i = 0; i < 5; i++)
        {
            //初期化
            PlayerLanePowers[i] = 0;
            EnemyLanePowers[i] = 0;
            PlayerCharaFrontPos[i] = 10f;
            EnemyCharaFrontPos[i] = -10f;
            EnemyPowers = 0;

            hit = Physics2D.BoxCastAll(SpawnPositions[i].position, new Vector2(1, 1.0875f), 0f, Vector2.right, 10f);
            //Debug.Log("BoxCast:" + hit.Length);
            for (int k = 0; hit.Length > k; k++)
            {
                //Debug.Log(k + "番目:"  + hit[k].collider.name);
                if (hit[k].collider.tag == "Animal")
                {
                    PlayerLanePowers[i]++;
                    if(hit[k].transform.position.x < PlayerCharaFrontPos[i])
                    {
                        PlayerCharaFrontPos[i] = hit[k].transform.position.x;
                    }
                }
                else if(hit[k].collider.tag == "Enemy")
                {
                    EnemyLanePowers[i]++;
                    EnemyPowers++;
                    if (hit[k].transform.position.x > EnemyCharaFrontPos[i])
                    {
                        EnemyCharaFrontPos[i] = hit[k].transform.position.x;
                    }
                }
            }
        }
    }
}
