using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimalScript : MonoBehaviour {

    private Status status;
    [SerializeField]private float Speed;
    private bool isEnemy = false;                   //  敵なのか
    private Vector2 direction;                      //  進む向き
    public enum Skill { Heal, Stun, KnockBack };    //  スキルの種類宣言
    public Skill skill;                             //  Enumをインスペクター上でいじるため
    [SerializeField]private GameObject EnemyObject;
    [SerializeField]private List<GameObject> EnemyObjects = new List<GameObject>();
    private GameObject FriendObject;
    private List<GameObject> FriendObjects = new List<GameObject>();
    [SerializeField]private bool isReady = false;   //  行動可能なのか
    [SerializeField]private bool isAttack = false;  //  攻撃中なのか

    private Animator animator;

    void Awake()
    {
        status = GetComponent<Status>();
        isEnemy = EnemyCheck();
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
                //  回復スキル
                Heal();
                //  回復スキルのエフェクトの表示
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Heart, gameObject.transform.position, 1, gameObject);
                break;
            case Skill.Stun:
                //  攻撃中の敵にスタン
                EnemyObject.GetComponent<AnimalScript>().StunObj();
                //  スタンスキルのエフェクトの表示
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Star, EnemyObject.transform.position, 1, EnemyObject.gameObject);
                break;
            case Skill.KnockBack:
                //  範囲に入ってるオブジェクトに対してノックバック
                if (SkillReady())
                    EnemyObject.GetComponent<AnimalScript>().KnockBackObj();
                for (int i = 0; i < EnemyObjects.Count; i++)
                {
                    EnemyObjects[i].GetComponent<AnimalScript>().KnockBackObj();
                }
                //  ノックバックスキルのエフェクト表示
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Spark, EnemyObject.transform.position, 1, EnemyObject.gameObject);
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

    //  移動系
    void Move()
    {
        Speed = status.Speed;
        //  攻撃する対象がいない＆レディ状態が満たしていた＆攻撃中じゃなければ＆スタン中じゃなければ
        if (EnemyObject == null && isReady && !isAttack && !status.isStun)
        {
            //  前に進む処理
            if (!isEnemy)
                direction = new Vector2(-Speed * Time.deltaTime, 0);
            else
                direction = new Vector2(Speed * Time.deltaTime, 0);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                animator.SetTrigger("Walk");
        }
        //  目の前に味方がいたら縦にずれる
        if (FriendObject != null)
        {
            if (gameObject.transform.position.y >= FriendObject.transform.position.y)
                direction += new Vector2(0, Time.deltaTime);
            else if (gameObject.transform.position.y <= FriendObject.transform.position.y)
                direction += new Vector2(0, -Time.deltaTime);
        }
        //  攻撃対象がいたら止まる
        if (EnemyObject != null)
            direction = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction;
    }

    //  EnemyObjectが倒されたら
    public void ResetEnemyObject()
    {
        Debug.Log("敵の再検索");
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
        //Debug.Log("Enemy? = " + isEnemy + "\nName = " + gameObject.transform.name + "\n当たった対象 = " + collision.name);
        //  自分が敵かつ触れた対象が動物または動物タワーだったら
        if (isEnemy)
        {
            if (collision.tag == "Animal" || collision.tag == "AnimalTower")
            {
               // Debug.Log("TriggerEnterヒット");
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
                    StartCoroutine(AttackAnimal());
            }
        }
        //  自分が動物かつ触れた対象が敵または敵タワーだったら
        if (!isEnemy)
        {
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
                    StartCoroutine(AttackAnimal());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isEnemy)
        {
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
                if (!status.isStun)
                    StartCoroutine(AttackAnimal());
            }
        }
        if (!isEnemy)
        {
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

    //コリジョン型で味方を参照する
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isEnemy)
        {
            if (collision.transform.tag == "Enemy")
            {
                //要素が含まれていなかったら追加
                if (!FriendObjects.Contains(collision.gameObject) && FriendObject != collision.gameObject)
                {
                    FriendObjects.Add(collision.gameObject);
                    if (FriendObject == null)
                    {
                        FriendObject = FriendObjects[0];
                        FriendObjects.RemoveAt(0);
                    }
                }
            }
        }
        if (!isEnemy)
        {
            if (collision.transform.tag == "Animal")
            {
                //要素が含まれていなかったら追加
                if (!FriendObjects.Contains(collision.gameObject) && FriendObject != collision.gameObject)
                {
                    FriendObjects.Add(collision.gameObject);
                    if (FriendObject == null)
                    {
                        FriendObject = FriendObjects[0];
                        FriendObjects.RemoveAt(0);
                    }
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isEnemy && collision.transform.tag == "Enemy")
        {
            //  要素が含まれていなかったら追加
            if (!FriendObjects.Contains(collision.gameObject) && FriendObject != collision.gameObject)
            {
                FriendObjects.Add(collision.gameObject);
                //  攻撃している対象がいなかったらセット
                if (FriendObject == null)
                {
                    FriendObject = FriendObjects[0];
                    FriendObjects.RemoveAt(0);
                }
            }
        }
        if (!isEnemy && collision.transform.tag == "Animal")
        {
            //  要素が含まれていなかったら追加
            if (!FriendObjects.Contains(collision.gameObject) && FriendObject != collision.gameObject)
            {
                FriendObjects.Add(collision.gameObject);
                //  攻撃している対象がいなかったらセット
                if (FriendObject == null)
                {
                    FriendObject = FriendObjects[0];
                    FriendObjects.RemoveAt(0);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (FriendObject == collision.gameObject)
            FriendObject = null;
        FriendObjects.Remove(collision.gameObject);
    }

    //  敵か動物側かを判定する
    private bool EnemyCheck()
    {
        if (tag == "Enemy" || tag == "EnemyTower")
            return true;
        else
            return false;
    }

    //  回復スキル
    public void Heal()
    {
        status.Heal(status.MaxHP / 4);
    }
    //  ノックバックスキル
    public void KnockBackObj()
    {
        StartCoroutine(KnockBack());
    }
    //  スタンスキル
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
    //  スタン用のコルーチン
    IEnumerator Stun(float stunTime)
    {
        status.isStun = true;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(stunTime);
        status.isStun = false;
        yield break;
    }
    //  ノックバック用のコルーチン
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
            //Debug.Log("Attack");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("Idle");
            //  攻撃状態を解除
            isAttack = false;
        }
        yield break;
    }

}
