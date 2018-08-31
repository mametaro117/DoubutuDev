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
    private UnitTable _animalStatus;

    private TimeManager timeManager;
    private ChoiceParamator choiceParamator;
    private CostScript CostScript;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        timeManager = GetComponent<TimeManager>();
        choiceParamator = GameObject.FindObjectOfType<ChoiceParamator>();
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
        float damage = Mathf.Ceil(attacker.GetComponent<Status>().Attack * TypeCheck(attacker, deffender));
        //  攻撃される側のHPを減らす
        deffender.GetComponent<Status>().HitPoint -= damage;
        //  攻撃エフェクトを表示
        EffectManager.Instance_Effect.PlayEffect_Smoke(deffender.transform.position);
        //  ダメージの表示
        DamageText.Instance.DiplayText(deffender.transform.position, damage);
        //  ヒット時のサウンド再生
        AudioManager.Instance.PlaySe(HitSound(attacker, deffender));
        //  ゲージの割合変化
        if (deffender.tag == "Enemy" || deffender.tag == "Animal")
        {
            deffender.GetComponent<Status>().ApplayBer();
        }
        //  HPが「0」以下になったときは削除
        if (deffender.GetComponent<Status>().HitPoint <= 0)
        {
            attacker.GetComponent<AnimalScript>().ResetEnemyObject();
            DeathUnit(deffender);
        }
    }

    //  ダメージの倍率チェック
    public float TypeCheck(GameObject attacker, GameObject deffender)
    {
        float num = 1;
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Sword && deffender.GetComponent<Status>().weaponType == Status.WeaponType.Shield)
        {
            num = 1.5f;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Sword && deffender.GetComponent<Status>().weaponType == Status.WeaponType.Arrow)
        {
            num = 0.5f;
            Debug.Log("効果いまひとつ…");
            return num;
        }

        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Shield && deffender.GetComponent<Status>().weaponType == Status.WeaponType.Arrow)
        {
            num = 1.5f;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Shield && deffender.GetComponent<Status>().weaponType == Status.WeaponType.Sword)
        {
            num = 0.5f;
            Debug.Log("効果いまひとつ…");
            return num;
        }

        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Arrow && deffender.GetComponent<Status>().weaponType == Status.WeaponType.Sword)
        {
            num = 1.5f;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Arrow && deffender.GetComponent<Status>().weaponType == Status.WeaponType.Shield)
        {
            num = 0.5f;
            Debug.Log("効果いまひとつ…");
            return num;
        }
        return num;
    }

    //  鳴らす音の判定
    private int HitSound(GameObject attacker, GameObject deffender)
    {
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Sword && deffender.GetComponent<Status>().weaponType != Status.WeaponType.Tower)
            return (int)AudioManager.SelistName.Sword;
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Shield && deffender.GetComponent<Status>().weaponType != Status.WeaponType.Tower)
            return (int)AudioManager.SelistName.Shield;
        if (attacker.GetComponent<Status>().weaponType == Status.WeaponType.Arrow && deffender.GetComponent<Status>().weaponType != Status.WeaponType.Tower)
            return (int)AudioManager.SelistName.Arrow;
        if (deffender.GetComponent<Status>().weaponType == Status.WeaponType.Tower)
            return (int)AudioManager.SelistName.KnockBack;
        return 0;
    }

    IEnumerator DelayDestry(GameObject deleteObj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(deleteObj);
        yield break;
    }
}
