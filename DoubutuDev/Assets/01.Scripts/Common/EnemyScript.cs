using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public RaycastHit2D raycastHit { get; private set; }
    private enum State { Idle = 0, Walk, Attack };
    State state = State.Idle;
    private bool IsGround = false;
    [SerializeField]
    private LayerMask TargetLayer;

	void Start () {
        StartCoroutine(Depoly());
    }

    // Update is called once per frame
    void Update () {
        if (raycastHit.collider == null)
        {
            Debug.Log("無いよ");
        }
        Move();
        if (isReady)
        {
            state = State.Walk;
        }
    }

    void FixedUpdate()
    {
        raycastHit = Physics2D.BoxCast(
            transform.position,
            size: transform.GetChild(0).GetComponent<BoxCollider2D>().size,
            angle: 0f,
            direction: Vector2.right,
            distance: 2,
            layerMask: TargetLayer
            );
        IsGround = raycastHit;
    }

    bool isAttack = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Animal"|| collision.transform.tag == "AnimalTower")
        {
            if (!isAttack)
            {
                state = State.Attack;
                Debug.Log("Attack");
                isAttack = true;
                StartCoroutine(AttackFreeze());
                BattleManager.Instance.Attack(gameObject, collision.gameObject);
            }

        }
    }

    void Move()
    {
        if(!IsGround)
            transform.position += new Vector3(0.5f * Time.deltaTime, 0, 0);
    }

    bool isReady = false;

    //  動物を配置したときに呼ぶ関数
    IEnumerator Depoly()
    {
        //  配置直後の待ち時間
        yield return new WaitForSeconds(1.0f);
        isReady = true;
        yield break;
    }
    //  攻撃硬直
    IEnumerator AttackFreeze()
    {
        //  硬直時間の設定
        yield return new WaitForSeconds(1);
        //  攻撃状態を解除
        isAttack = false;
        yield break;
    }

}
