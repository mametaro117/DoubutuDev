﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {


    [SerializeField]
    private GameObject EnemyObject;

    [SerializeField]
    private List<GameObject> EnemyObjects = new List<GameObject>();

    private Animator animator;

    bool isReady = false;
    bool isAttack = false;

    bool isStun = false;
    bool isKnockback = false;



    void Start () {
        //  出撃時の硬直
        StartCoroutine(Depoly());
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        Move();
    }

    void Move()
    {
        if (EnemyObject == null && isReady && !isAttack && !isStun && !isKnockback)
        {
            transform.position += new Vector3(0.5f * Time.deltaTime, 0, 0);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                animator.SetTrigger("Walk");
        }
    }

    //  EnemyObjectが倒されたら
    public void ResetEnemyObject()
    {
        if(EnemyObjects.Count != 0)
        {
            //  一番近い敵を標的に再設定
            GameObject nearestEnemy = null;
            float minDis = 100000;
            foreach (GameObject enemy in EnemyObjects)
            {
                float dis = Vector3.Distance(transform.position, enemy.transform.position);
                if (dis < minDis)
                {
                    minDis = dis;
                    nearestEnemy = enemy;
                }
            }
            EnemyObject = nearestEnemy;
            EnemyObjects.Remove(nearestEnemy);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //  どうぶつ又はどうぶつタワーだったら
        if (collision.tag == "Animal" || collision.tag == "AnimalTower")
        {
            //  要素が含まれていなかったら追加
            if (!EnemyObjects.Contains(collision.gameObject) && EnemyObject != collision.gameObject)
            {
                EnemyObjects.Add(collision.gameObject);
                //  攻撃対象している対象がいなかったらセット
                if(EnemyObject == null)
                {
                    EnemyObject = EnemyObjects[0];
                    EnemyObjects.RemoveAt(0);
                }
            }
            if (!isStun)
            {
                StartCoroutine(AttackAnimal());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Animal" || collision.tag == "AnimalTower")
        {
            if (!isStun)
            {
                StartCoroutine(AttackAnimal());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyObjects.Remove(collision.gameObject);
    }


    public void Heal()
    {

    }






    //----------コルーチン----------

    //  動物を配置したときに呼ぶ関数
    IEnumerator Depoly()
    {
        //  配置直後の待ち時間
        yield return new WaitForSeconds(2.0f);
        isReady = true;
        yield break;
    }
    //  攻撃硬直
    IEnumerator AttackAnimal()
    {
        //Debug.Log("AttackAnimal");
        while(EnemyObject != null && !isAttack && isReady)
        {
            isAttack = true;
            animator.SetTrigger("Attack");
            animator.ResetTrigger("Walk");
            BattleManager.Instance.Attack(gameObject, EnemyObject);
            Debug.Log("Attack");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("Idle");
            //  攻撃状態を解除
            isAttack = false;
        }        
        yield break;
    }
}
