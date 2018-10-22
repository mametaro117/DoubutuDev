using System.Collections.Generic;

using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {            //敵キャラを生成する

    //変数エリア
    [SerializeField]
    private GameObject[] enemyPrefab = new GameObject[3];   //敵のPrefab入れ

    [SerializeField]
    private Transform[] spawnPositions = new Transform[5];  //敵のスポーン位置入れ

    [SerializeField]
    private float interval = 5f;                                    //敵の生成速度
    private float stackTime;                                        //最後に敵を生成してからの時間
    private int enemyPowers;                                        //敵の生成数

    [SerializeField]
    private int tekagenEnemyPowers = 5;                             //手加減し始める数
    [SerializeField]
    private float deraySpawnTime = 0.5f;                            //手加減する時に延ばす時間

    [SerializeField]
    private int kimagure = 20;                                      //生成場所をランダムに変える確率
    
    private RaycastHit2D[] hit;                                     //レイキャストでヒットしたオブジェクト入れ

    private int[] playerLanePowers = new int[5];                    //レーン毎のキャラ数、一番前にいるキャラのpos
    private int[] enemyLanePowers = new int[5];                     
    private float[] playerCharaFrontPos = new float[5];             
    private float[] enemyCharaFrontPos = new float[5];              //

    void Update ()
    {
        //時間経過
        stackTime += Time.deltaTime;

        //手加減をする
        if(enemyPowers - tekagenEnemyPowers >= 1)
        {
            //手加減の時間を加えて、生成時間になったら/ゲームが開始されていたら
            if (stackTime >= interval + (enemyPowers - tekagenEnemyPowers) * deraySpawnTime 
                && GetComponent<TimeManager>().GetIsReady() == true)
            {
                //敵を生成
                stackTime = 0;
                EnemyInstance();
            }
        }
        //手加減していない
        else
        {
            //生成時間になったら/ゲームが開始されていたら
            if (stackTime >= interval && GetComponent<TimeManager>().GetIsReady() == true)
            {
                //敵を生成
                stackTime = 0;
                EnemyInstance();
            }
        }
    }

    //敵を生成する処理
    public void EnemyInstance()
    {
        //レーン毎の数、前線位置を見る
        PowerCheck();
        //スポーン位置決定
        int spawnPos = SetSpawnPos();
        //生成の 法則が 乱れる！
        if(Random.Range(0, 100) <= kimagure)
        {
            spawnPos = Random.Range(0, spawnPositions.Length);
        }
        //スポーン位置が決まらなかったらランダムで置く
        if(spawnPos == -1)
        {
            spawnPos = Random.Range(0, spawnPositions.Length);
        }
        //置く敵Prefabをランダムで決定
        int ran = Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[ran], 
        new Vector3(spawnPositions[spawnPos].position.x, spawnPositions[spawnPos].position.y, 
        spawnPositions[spawnPos].position.z), spawnPositions[spawnPos].rotation);
    }

    //スポーン位置を決める
    private int SetSpawnPos()
    {
        //初期化
        List<int> playerRaneNum = new List<int>();
        List<int> enemyRaneNum = new List<int>();
        int maxPow = 0;
        int minPow = 0;
        int setRaneResult = 0;
        float maxFrontPos = 0f;
        
        for (int i = 0; i < playerLanePowers.Length; i++)
        {
            //パワーを比較
            //最大値より大きければリストを初期化してからレーン番号を入れる
            if (playerLanePowers[i] > maxPow)
            {
                maxPow = playerLanePowers[i];
                playerRaneNum = new List<int>();
                playerRaneNum.Add(i);
            }
            //最大値と一緒ならレーン番号を追加する
            else if (playerLanePowers[i] == maxPow)
            {
                playerRaneNum.Add(i);
            }
        }
        //もし、対象のレーンが複数存在する場合、自分側のポジションと敵側のキャラ数を見て判断する
        //ここに入る条件は自分のキャラが何もない時なのでランダムで返す
        if(playerRaneNum.Count >= 5 && maxPow == 0)
        {
            setRaneResult = Random.Range(0, playerRaneNum.Count);
        }
        //複数ある時
        else if (playerRaneNum.Count >= 2)
        {
            maxPow = 0;
            //敵側キャラのキャラ数を比較
            for(int i = 0; i < playerRaneNum.Count; i++)
            {
                if(enemyLanePowers[playerRaneNum[i]] > maxPow)
                {
                    enemyRaneNum = new List<int>();
                    enemyRaneNum.Add(playerRaneNum[i]);
                    maxPow = enemyLanePowers[playerRaneNum[i]];
                }
                else if(enemyLanePowers[playerRaneNum[i]] == maxPow)
                {
                    enemyRaneNum.Add(playerRaneNum[i]);
                }
            }
            //ポジションを見る条件は、敵側キャラが同数いる時
            if(playerRaneNum.Count == enemyRaneNum.Count)
            {
                for(int i = 0; i < enemyRaneNum.Count; i++)
                {
                    if (maxFrontPos == 0f)
                    {
                        maxFrontPos = playerCharaFrontPos[playerRaneNum[i]];
                        setRaneResult = playerRaneNum[i];
                    }
                    else
                    {
                        if(playerCharaFrontPos[playerRaneNum[i]] < maxFrontPos)
                        {
                            maxFrontPos = playerCharaFrontPos[playerRaneNum[i]];
                            setRaneResult = playerRaneNum[i];
                        }
                    }
                }
            }
            //プレイヤー側キャラを見て、少ない所に配置
            else
            {
                minPow = 0;
                for(int i = 0; i < playerRaneNum.Count; i++)
                {
                    if(i == 0)
                    {
                        enemyRaneNum = new List<int>();
                        minPow = enemyLanePowers[playerRaneNum[i]];
                        enemyRaneNum.Add(playerRaneNum[i]);
                    }
                    else
                    {
                        if(minPow > enemyLanePowers[playerRaneNum[i]])
                        {
                            minPow = enemyLanePowers[playerRaneNum[i]];
                            enemyRaneNum = new List<int>();
                            enemyRaneNum.Add(playerRaneNum[i]);
                        }
                        else if(minPow == enemyLanePowers[playerRaneNum[i]])
                        {
                            enemyRaneNum.Add(playerRaneNum[i]);
                        }
                    }
                }
                setRaneResult = enemyRaneNum[Random.Range(0, enemyRaneNum.Count)];
            }
        }
        //そこに配置
        else
        {
            setRaneResult = playerRaneNum[Random.Range(0, playerRaneNum.Count)];
        }

        //複数ある場合はランダムで返す
        return setRaneResult;
    }

    //プレイヤーと敵の、レーン毎の数を見る
    public void PowerCheck()
    {
        //レーン毎に見る
        for(int i = 0; i < 5; i++)
        {
            //初期化
            playerLanePowers[i] = 0;
            enemyLanePowers[i] = 0;
            playerCharaFrontPos[i] = 10f;
            enemyCharaFrontPos[i] = -10f;
            enemyPowers = 0;

            //レイキャスト
            hit = Physics2D.BoxCastAll(spawnPositions[i].position, new Vector2(1, 1.0875f), 0f, Vector2.right, 10f);

            for (int k = 0; hit.Length > k; k++)
            {
                //当たったキャラのタグを見る
                if (hit[k].collider.tag == "Animal")
                {
                    //自キャラパワーに加える
                    playerLanePowers[i]++;
                    if(hit[k].transform.position.x < playerCharaFrontPos[i])
                    {
                        //一番敵のタワーに近いキャラポジション
                        playerCharaFrontPos[i] = hit[k].transform.position.x;
                    }
                }
                else if(hit[k].collider.tag == "Enemy")
                {
                    //敵キャラパワーに加える
                    enemyLanePowers[i]++;
                    enemyPowers++;
                    if (hit[k].transform.position.x > enemyCharaFrontPos[i])
                    {
                        //一番プレイヤーのタワーに近いキャラポジション
                        enemyCharaFrontPos[i] = hit[k].transform.position.x;
                    }
                }
            }
        }
    }
}

