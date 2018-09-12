using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

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
    private UnitTable unitTable;

    private TimeManager timeManager;
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        //タイムをいじる用の参照
      timeManager = GetComponent<TimeManager>();
        //編成情報の反映
        GetComponent<AnimalButtonManager>().SetAnimalStatus();
    }

    public void AddFieldUnit(GameObject obj)
    {
        OnFieldUnitsList.Add(obj);
        Debug.Log(obj.name);
    }

    //ユニットの破棄、タワーだった場合の処理などを書く予定
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
        //ダメージの計算
        float damage = Mathf.Ceil(attacker.GetComponent<Status>().Attack * TypeCheck(attacker, deffender));
        //攻撃される側のHPを減らす
        deffender.GetComponent<Status>().HitPoint -= damage;
        //攻撃エフェクトを表示
        HitEffect(attacker, deffender);
        //ダメージの表示
        //DamageText.Instance.DiplayText(deffender.transform.position, damage);
        DamageText.Instance.DiplayTextSprite(deffender.transform.position, damage);
        //ヒット時のサウンド再生
        AudioManager.Instance.PlaySe(HitSound(attacker, deffender));
        //ゲージの割合変化
        if (deffender.tag == "Enemy" || deffender.tag == "Animal")
        {
            deffender.GetComponent<Status>().ApplayBer();
        }
        //HPが「0」以下になったときは削除
        if (deffender.GetComponent<Status>().HitPoint <= 0)
        {
            attacker.GetComponent<AnimalScript>().ResetEnemyObject();
            DeathUnit(deffender);
        }
    }
    //ダメージの倍率チェック
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

    //鳴らす音の判定
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

    private void HitEffect(GameObject Attacker, GameObject Deffender)
    {
        switch (Attacker.GetComponent<Status>().weaponType)
        {
            case Status.WeaponType.Sword:
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Smoke, Deffender.transform.position, 1, Deffender);
                Debug.Log("剣");
                break;

            case Status.WeaponType.Shield:
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Smoke, Deffender.transform.position, 1, Deffender);
                Debug.Log("盾");
                break;

            default:
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Hit, Deffender.transform.position, 1, Deffender);
                Debug.Log("弓");
                break;
        }
    }

    IEnumerator DelayDestry(GameObject deleteObj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(deleteObj);
        yield break;
    }
}
