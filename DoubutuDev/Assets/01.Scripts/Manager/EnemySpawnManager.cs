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
        int rand = Random.Range(0, 5);
        Instantiate(EnemyPrefab, new Vector3(SpawnPositions[rand].position.x, SpawnPositions[rand].position.y, SpawnPositions[rand].position.z), SpawnPositions[rand].rotation);
    }

    RaycastHit2D[] hit;

    [SerializeField]
    int[] LanePowers = new int[5];

    public void PowerCheck()
    {
        for(int i = 0; i < 5; i++)
        {
            LanePowers[i] = 0;  //  初期化
            hit = Physics2D.BoxCastAll(SpawnPositions[i].position, new Vector2(1, 2), 0f, Vector2.right, 10f);
            Debug.Log("BoxCast:" + hit.Length);
            for (int k = 0; hit.Length > k; k++)
            {
                Debug.Log(k + "番目:"  + hit[k].collider.name);
                if (hit[k].collider.tag == "Animal")
                    LanePowers[i]++;
            }
        }
    }
}
