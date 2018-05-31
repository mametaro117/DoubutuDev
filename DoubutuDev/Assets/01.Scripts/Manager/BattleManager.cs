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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddFieldUnit(GameObject obj)
    {
        OnFieldUnitsList.Add(obj);
    }

    void DeathUnit(GameObject obj)
    {
        OnFieldUnitsList.Remove(obj);
    }
    
    public void Attack(GameObject attacker, GameObject deffender)
    {
        deffender.GetComponent<Totalstatus>().HitPoint -= attacker.GetComponent<Totalstatus>().Attack;
        Debug.Log(deffender.GetComponent<Totalstatus>().HitPoint);
        if (deffender.GetComponent<Totalstatus>().HitPoint <= 0)
        {
            DeathUnit(deffender);
            Destroy(deffender);
        }
    }
}
