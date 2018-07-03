using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public RaycastHit2D raycastHit;
    private enum State { Idle = 0, Walk, Attack };
    State state = State.Idle;
    private bool IsGround = false;
    [SerializeField]
    private LayerMask TargetLayer;

    [SerializeField]
    private GameObject EnemyObject;

    [SerializeField]
    private List<GameObject> EnemyObjects = new List<GameObject>();

	void Start () {
        StartCoroutine(Depoly());
    }

    // Update is called once per frame
    void Update () {
        Move();
        if (isReady)
        {
            state = State.Walk;
        }
    }

    bool isAttack = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //  どうぶつ又はどうぶつタワーだったら
        if (collision.tag == "Animal" || collision.tag == "AnimalTower")
        {
            //  要素が含まれていなかったら追加
            if (!EnemyObjects.Contains(collision.gameObject))
            {
                EnemyObjects.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(EnemyObjects.Count != 0)
        {
            StartCoroutine(AttackFreeze());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyObjects.Remove(collision.gameObject);
    }


    //  移動
    void Move()
    {
        if(EnemyObject == null && isReady)
            transform.position += new Vector3(0.5f * Time.deltaTime, 0, 0);
    }

    bool isReady = false;

    //  動物を配置したときに呼ぶ関数
    IEnumerator Depoly()
    {
        //  配置直後の待ち時間
        yield return new WaitForSeconds(2.0f);
        isReady = true;
        yield break;
    }
    //  攻撃硬直
    IEnumerator AttackFreeze()
    {
        while(EnemyObjects.Count != 0 && !isAttack)
        {
            isAttack = true;
            BattleManager.Instance.Attack(gameObject, EnemyObjects[0]);
            Debug.Log("Attack");
            yield return new WaitForSeconds(1f);
            //  攻撃状態を解除
            isAttack = false;
        }        
        yield break;
    }
}
