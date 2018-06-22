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

    public enum WeaponType
    {
        Sword, Shield, Arrow
    };

    [SerializeField]
    private WeaponType weaponType;

    private List<GameObject> OnFieldUnitsList = new List<GameObject>();

    //Inspectorに表示される
    [SerializeField]
    private List<AnimalList> _animalListList = new List<AnimalList>();



    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void AddFieldUnit(GameObject obj)
    {
        OnFieldUnitsList.Add(obj);
        Debug.Log(obj.name);
    }

    void DeathUnit(GameObject obj)
    {
        OnFieldUnitsList.Remove(obj);
        StartCoroutine(DelayDestry(obj));
    }
    
    public void Attack(GameObject attacker, GameObject deffender)
    {
        //  攻撃される側のHPを、攻撃側アタック分減らす
        deffender.GetComponent<Totalstatus>().HitPoint -= attacker.GetComponent<Totalstatus>().Attack;
        //  ゲージの割合変化
        if (deffender.tag == "Enemy")
        {
            deffender.GetComponent<Totalstatus>().ApplayBer();
        }
        //  減らした後のHPを表示
        Debug.Log(deffender.GetComponent<Totalstatus>().HitPoint);
        //  HPが「0」以下になったときは削除
        if (deffender.GetComponent<Totalstatus>().HitPoint <= 0)
        {
            DeathUnit(deffender);
        }
    }

    IEnumerator DelayDestry(GameObject deleteObj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(deleteObj);
        yield break;
    }
}
