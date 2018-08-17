using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimalScript : MonoBehaviour {

    private Status status;
    [SerializeField]private float Speed;
    private bool isEnemy = false;                   //  敵なのか


    public enum Skill { Heal, Stun, KnockBack };    //  スキルの種類宣言
    public Skill skill;                             //  Enumをインスペクター上でいじるため

    [SerializeField]private GameObject EnemyObject;
    [SerializeField]private List<GameObject> EnemyObjects = new List<GameObject>();
    [SerializeField]private bool isReady = false;   //  行動可能なのか
    [SerializeField]private bool isAttack = false;  //  攻撃中なのか

    private Animator animator;

    void Awake()
    {
        status = GetComponent<Status>();
        isEnemy = EnemyCheck();
        Speed = status.Speed;
    }

    void Start()
    {
        StartCoroutine(Depoly());               //  出撃時の硬直
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    public void PlaySkill()
    {
        switch (skill)
        {
            case Skill.Heal:
                Heal();
                break;
            case Skill.Stun:
                EnemyObject.GetComponent<AnimalScript>().StunObj();
                break;
            case Skill.KnockBack:
                if (SkillReady())
                    EnemyObject.GetComponent<AnimalScript>().KnockBackObj();
                for (int i = 0; i < EnemyObjects.Count; i++)
                {
                    EnemyObjects[i].GetComponent<AnimalScript>().KnockBackObj();
                }
                break;
        }
    }

    //  スキルを発動するタイミングなのか
    public bool SkillReady()
    {
        switch (skill)
        {
            case Skill.Heal:            //  回復
                if ((status.HitPoint / status.MaxHP) <= 0.7f )
                    return true;
                else
                    return false;
            case Skill.Stun:            //  スタン
                if (EnemyObject != null && EnemyObject.tag == "Enemy")
                    return true;
                else
                    return false;
            case Skill.KnockBack:       //  ノックバック
                if (EnemyObject != null && EnemyObject.tag == "Enemy")
                    return true;
                else
                    return false;
        }
        return false;
    }

    //  移動
    void Move()
    {
        if (EnemyObject == null && isReady && !isAttack && !status.isStun)
        {
            if (isEnemy)
            {
                //transform.position += new Vector3(Speed * Time.deltaTime * 0.1f, 0, 0);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * Time.deltaTime, 0);
            }                
            else
                transform.position += new Vector3(-Speed * Time.deltaTime * 0.1f, 0, 0);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                animator.SetTrigger("Walk");
        }

        if (EnemyObject != null)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    //  EnemyObjectが倒されたら
    public void ResetEnemyObject()
    {
        if (EnemyObjects.Count != 0)
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
        if (isEnemy)
        {
            //  どうぶつ又はどうぶつタワーだったら
            if (collision.tag == "Animal" || collision.tag == "Tower")
            {
                //  要素が含まれていなかったら追加
                if (!EnemyObjects.Contains(collision.gameObject) && EnemyObject != collision.gameObject)
                {
                    EnemyObjects.Add(collision.gameObject);
                    //  攻撃している対象がいなかったらセット
                    if (EnemyObject == null)
                    {
                        EnemyObject = EnemyObjects[0];
                        EnemyObjects.RemoveAt(0);
                    }
                }
                if (!status.isStun)
                {
                    StartCoroutine(AttackAnimal());
                }
            }
        }
        else if (!isEnemy)
        {
            //  どうぶつ又はどうぶつタワーだったら
            if (collision.tag == "Enemy" || collision.tag == "EnemyTower")
            {
                //  要素が含まれていなかったら追加
                if (!EnemyObjects.Contains(collision.gameObject) && EnemyObject != collision.gameObject)
                {
                    EnemyObjects.Add(collision.gameObject);
                    //  攻撃している対象がいなかったらセット
                    if (EnemyObject == null)
                    {
                        EnemyObject = EnemyObjects[0];
                        EnemyObjects.RemoveAt(0);
                    }
                }
                if (!status.isStun)
                {
                    StartCoroutine(AttackAnimal());
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isEnemy)
        {
            //  どうぶつ又はどうぶつタワーだったら
            if (collision.tag == "Animal" || collision.tag == "AnimalTower")
            {
                //  要素が含まれていなかったら追加
                if (!EnemyObjects.Contains(collision.gameObject) && EnemyObject != collision.gameObject)
                {
                    EnemyObjects.Add(collision.gameObject);
                    //  攻撃している対象がいなかったらセット
                    if (EnemyObject == null)
                    {
                        EnemyObject = EnemyObjects[0];
                        EnemyObjects.RemoveAt(0);
                    }
                }
                StartCoroutine(AttackAnimal());
            }
        }
        if (!isEnemy)
        {
            //  エネミー又はエネミータワーだったら
            if (collision.tag == "Eney" || collision.tag == "EnemyTower")
            {
                //  要素が含まれていなかったら追加
                if (!EnemyObjects.Contains(collision.gameObject) && EnemyObject != collision.gameObject)
                {
                    EnemyObjects.Add(collision.gameObject);
                    //  攻撃している対象がいなかったらセット
                    if (EnemyObject == null)
                    {
                        EnemyObject = EnemyObjects[0];
                        EnemyObjects.RemoveAt(0);
                    }
                }
                StartCoroutine(AttackAnimal());
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (EnemyObject == collision.gameObject)
            EnemyObject = null;
        EnemyObjects.Remove(collision.gameObject);
    }

    private bool EnemyCheck()
    {
        if (tag == "Enemy" || tag == "EnemyTower")
            return true;
        else
            return false;
    }

    public void Heal()
    {
        status.Heal(status.MaxHP / 4);
    }

    public void KnockBackObj()
    {
        StartCoroutine(KnockBack());
    }

    public void StunObj()
    {
        StartCoroutine(Stun(1));
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

    IEnumerator Stun(float stunTime)
    {
        status.isStun = true;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(stunTime);
        status.isStun = false;
        yield break;
    }

    IEnumerator KnockBack()
    {
        status.isStun = true;
        yield return null;
        gameObject.transform.DOMove(transform.position + new Vector3(-1f, 0, 0), 1f);
        yield return new WaitForSeconds(1f);
        status.isStun = false;
        yield break;
    }

    //  攻撃硬直
    IEnumerator AttackAnimal()
    {
        //Debug.Log("AttackAnimal");
        while (EnemyObject != null && !isAttack && isReady && !status.isStun)
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
