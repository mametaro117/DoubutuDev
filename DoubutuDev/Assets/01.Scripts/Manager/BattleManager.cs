using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class AnimalList
{
    public List<UnitDictionary> List = new List<UnitDictionary>();

    public AnimalList(List<UnitDictionary> list)
    {
        List = list;
    }
}


public class BattleManager : MonoBehaviour {

    #region Singleton

    private static BattleManager instance;

    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (BattleManager)FindObjectOfType(typeof(BattleManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(BattleManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    private List<GameObject> OnFieldUnitsList = new List<GameObject>();

    //Inspectorに表示される
    [SerializeField]
    private List<AnimalList> _animalListList = new List<AnimalList>();

    private TimeManager timeManager;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        timeManager = GetComponent<TimeManager>();
    }

    public void AddFieldUnit(GameObject obj)
    {
        OnFieldUnitsList.Add(obj);
        Debug.Log(obj.name);
    }

    //  ユニットの破棄、タワーだった場合の処理などを書く予定
    void DeathUnit(GameObject obj)
    {
        OnFieldUnitsList.Remove(obj);
        if (obj.tag == "Animal" || obj.tag == "Enemy")
        {
            StartCoroutine(DelayDestry(obj));
        }
        else if (obj.tag == "EnemyTower")
        {
            timeManager.GameClear();
        }
        else if (obj.tag == "AnimalTower")
        {
            timeManager.GameFaild();
        }
    }
    
    public void Attack(GameObject attacker, GameObject deffender)
    {
        //  ダメージの計算
        float damage = Mathf.Ceil(attacker.GetComponent<Totalstatus>().Attack * TypeCheck(attacker, deffender));
        //  攻撃される側のHPを減らす
        deffender.GetComponent<Totalstatus>().HitPoint -= damage;
        //  ダメージの表示
        DamageText.Instance.DiplayText(deffender.transform.position, damage);
        //  ゲージの割合変化
        if (deffender.tag == "Enemy" || deffender.tag == "Animal")
        {
            deffender.GetComponent<Totalstatus>().ApplayBer();
        }
        //  減らした後のHPを表示
        Debug.Log(deffender.GetComponent<Totalstatus>().HitPoint);
        //  HPが「0」以下になったときは削除
        if (deffender.GetComponent<Totalstatus>().HitPoint <= 0)
        {
            if(deffender.tag == "Animal")
            {
                attacker.GetComponent<EnemyScript>().ResetEnemyObject();
            }
            DeathUnit(deffender);
        }
    }

    //  ダメージの倍率チェック
    public float TypeCheck(GameObject attacker, GameObject deffender)
    {
        float num = 1;
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield)
        {
            num = 1.5f;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow)
        {
            num = 0.5f;
            Debug.Log("効果いまひとつ…");
            return num;
        }

        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow)
        {
            num = 1.5f;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword)
        {
            num = 0.5f;
            Debug.Log("効果いまひとつ…");
            return num;
        }

        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword)
        {
            num = 1.5f;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield)
        {
            num = 0.5f;
            Debug.Log("効果いまひとつ…");
            return num;
        }
        return num;
    }

    IEnumerator DelayDestry(GameObject deleteObj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(deleteObj);
        yield break;
    }
}
