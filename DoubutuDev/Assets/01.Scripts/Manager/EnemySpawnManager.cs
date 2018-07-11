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
        stacktime += Time.deltaTime;
        //Debug.Log(stacktime);
        Debug.Log(GetComponent<TimeManager>().GetIsReady());
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
}
