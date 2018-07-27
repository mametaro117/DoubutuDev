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
    [SerializeField]
    private DamageText.DamageTextColor _playerDamageFontColor = DamageText.DamageTextColor.Blue;
    [SerializeField]
    private DamageText.DamageTextColor _enemyDamageFontColor = DamageText.DamageTextColor.Red;
    
    [HideInInspector]
    public int TypeCheckNum; 

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
    public enum DamageColor
    {
        Defalut = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
    }
    string[] ColorTable =
    {
        "<color = #000000>",
        "<color = #ff0000>",
        "<color = #00ff00>",
        "<color = #0000ff>",
    };
    //テキスト・カラーを変更できるようにする

    public enum DamageType
    {
        Nomal = 0,
        Weak,
        Noteffect
    };

    Dictionary<int, float> TypeMagnification = new Dictionary<int, float>()
    {
        {0, 0f},
        {1, 2f},
        {2, 0.5f}
    };


    public void Attack(GameObject attacker, GameObject deffender)
    {
        //  ダメージの計算
        float damage = Mathf.Ceil(attacker.GetComponent<Totalstatus>().Attack * TypeCheck(attacker, deffender));
        //  攻撃される側のHPを減らす
        deffender.GetComponent<Totalstatus>().HitPoint -= damage;
        //  ダメージの表示
        DamageText.Instance.DiplayText_Animal(deffender.transform.position, damage);
        //  ヒット時のサウンド再生
        AudioManager.Instance.PlaySe(HitSound(attacker, deffender));
        //  ゲージの割合変化
        if (deffender.tag == "Enemy" || deffender.tag == "Animal")
        {
            deffender.GetComponent<Totalstatus>().ApplayBer();
        }
        //  減らした後のHPを表示
        //Debug.Log(deffender.GetComponent<Totalstatus>().HitPoint);
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
    /// <summary>
    /// 攻撃カラーを有効にする
    /// </summary>
    /// <param name="attacker">攻撃側のゲームオブジェクト</param>
    /// <param name="deffender">攻撃される側のゲームオブジェクト</param>
    /// <param name="damageColor"></param>
    public void AttackWithColor(GameObject attacker, GameObject deffender, DamageColor damageColor = DamageColor.Defalut)
    {
        //  ダメージの計算
        float damage = Mathf.Ceil(attacker.GetComponent<Totalstatus>().Attack * TypeCheck(attacker, deffender));
        TypeCheckNum = TypeCheckInt(attacker, deffender);
        //  攻撃される側のHPを減らす
        deffender.GetComponent<Totalstatus>().HitPoint -= damage;
        //  ダメージの表示
        if (deffender.tag == "Enemy")
        {
            //暫定で敵の攻撃を赤くしている
            DamageText.Instance.DiplayText_Enemy(deffender.transform.position, damage, _enemyDamageFontColor);
        }
        else if (deffender.tag == "Animal")
        {
            DamageText.Instance.DiplayText_Animal(deffender.transform.position, damage, _playerDamageFontColor);

        }
        //else if ()
        //{

        //}
        //else
        //{

        //}
        
        //  ヒット時のサウンド再生
        AudioManager.Instance.PlaySe(HitSound(attacker, deffender));
        //  ゲージの割合変化
        if (deffender.tag == "Enemy" || deffender.tag == "Animal")
        {
            deffender.GetComponent<Totalstatus>().ApplayBer();
        }
        //  減らした後のHPを表示
        //Debug.Log(deffender.GetComponent<Totalstatus>().HitPoint);
        //  HPが「0」以下になったときは削除
        if (deffender.GetComponent<Totalstatus>().HitPoint <= 0)
        {
            if (deffender.tag == "Animal")
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


    public int TypeCheckInt(GameObject attacker, GameObject deffender)
    {
        int num = 0;
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield)
        {
            num = (int)DamageType.Weak;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow)
        {
            num = (int)DamageType.Noteffect;
            Debug.Log("効果いまひとつ…");
            return num;
        }

        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow)
        {
            num = (int)DamageType.Weak;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword)
        {
            num = (int)DamageType.Noteffect;
            Debug.Log("効果いまひとつ…");
            return num;
        }

        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword)
        {
            num = (int)DamageType.Weak;
            Debug.Log("効果抜群！");
            return num;
        }
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow && deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield)
        {
            num = (int)DamageType.Noteffect;
            Debug.Log("効果いまひとつ…");
            return num;
        }
        return num;
    }

    //  鳴らす音の判定
    private int HitSound(GameObject attacker, GameObject deffender)
    {
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Sword && deffender.GetComponent<Totalstatus>().weaponType != Totalstatus.WeaponType.Tower)
            return (int)AudioManager.SelistName.Sword;
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Shield && deffender.GetComponent<Totalstatus>().weaponType != Totalstatus.WeaponType.Tower)
            return (int)AudioManager.SelistName.Shield;
        if (attacker.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Arrow && deffender.GetComponent<Totalstatus>().weaponType != Totalstatus.WeaponType.Tower)
            return (int)AudioManager.SelistName.Arrow;
        if (deffender.GetComponent<Totalstatus>().weaponType == Totalstatus.WeaponType.Tower)
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
