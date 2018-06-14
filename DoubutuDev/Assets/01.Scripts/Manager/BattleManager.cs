using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private List<UnitDictionary> AnimalList = new List<UnitDictionary>();

    private List<GameObject> OnFieldUnitsList = new List<GameObject>();

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
        //Debug.Log(AnimalList[0].Name);
        //Debug.Log(AnimalList[0].HitPoint);
        //Debug.Log(AnimalList[0].IsFlying);
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
