using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animalstate : MonoBehaviour {

    private int StateNum = 0;
    [SerializeField]
    private int NowStateNum = 0;
    private Animator animator;


    void Start () {
        animator = GetComponent<Animator>();
        //Debug.Log("Start");
        StartCoroutine(Depoly());
        //  マネージャーへこのオブジェクトを追加
        BattleManager.Instance.AddFieldUnit(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        //  ステートのチェック
        CheckState();
        //  どうぶつの動作
        Move();
    }

    void CheckState()
    {
        //  ステート値の変化があったら
        if(NowStateNum != StateNum)
        {
            //Debug.Log("ChechState");
            NowStateNum = StateNum;
            switch (NowStateNum)
            {
                case 0:
                    //  待機ステート
                    animator.SetInteger("State", 0);
                    break;
                case 1:
                    //  歩行ステート
                    animator.SetInteger("State", 1);
                    break;
                case 2:
                    //  攻撃ステート
                    animator.SetInteger("State", 2);
                    break;
                default:
                    //  それ以外は待機状態に
                    animator.SetInteger("State", 0);
                    Debug.Log("ステートに違う値が来ている");
                    break;
            }
        }
    }

    //  現在のステートでの移動
    void Move()
    {
        switch (NowStateNum)
        {
            case 1:
                Vector3 v;
                v = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
                transform.position = v;
                break;
            case 0:
            case 2:
            default:
                break;
        }
    }


    bool isAttack = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        //  当たった対象が敵もしくはタワーなら攻撃
        if (collision.tag == "Enemy" || collision.tag == "TowerEnemy")
        {
            if (!isAttack)
            {
                Debug.Log("Attack");
                isAttack = true;
                StateNum = 2;
                StartCoroutine(AttackFreeze());
                BattleManager.Instance.Attack(gameObject, collision.gameObject);
            }
        }
    }

    //  動物を配置したときに呼ぶ関数
    IEnumerator Depoly()
    {
        //  配置直後の待ち時間
        yield return new WaitForSeconds(2.0f);
        //  歩行ステートに変更
        StateNum = 1;
        yield break;
    }

    IEnumerator AttackFreeze()
    {
        //  硬直時間の設定
        yield return new WaitForSeconds(1);
        //  攻撃状態を解除
        isAttack = false;
        //  ステートを
        StateNum = 1;
        yield break;
    }
}
