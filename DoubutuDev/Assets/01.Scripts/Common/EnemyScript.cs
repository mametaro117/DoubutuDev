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
        if (collision.tag == "Animal" || collision.tag == "AnimalTower")
            EnemyObjects.Add(collision.gameObject);
        StartCoroutine(AttackFreeze());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyObject = null;
        //EnemyObjects.RemoveRange(collision.gameObject);
    }

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
        while(EnemyObject != null && !isAttack)
        {
            BattleManager.Instance.Attack(gameObject, EnemyObject);
            Debug.Log("Attack");
            yield return new WaitForSeconds(1f);
            //  攻撃状態を解除
            isAttack = false;
        }        
        
        yield break;
    }
}
