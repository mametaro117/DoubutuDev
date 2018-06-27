using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    #region Singleton

    private static EffectManager instance_Effect;

    public static EffectManager Instance_Effect
    {
        get
        {
            if (instance_Effect == null)
            {
                instance_Effect = (EffectManager)FindObjectOfType(typeof(EffectManager));

                if (instance_Effect == null)
                {
                    Debug.LogError(typeof(EffectManager) + "is nothing");
                }
            }
            return instance_Effect;
        }
    }

    #endregion Singleton

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SingleTest()
    {
        Debug.Log("hoge");
        Instantiate(Resources.Load("CFX3_MagicAura_D_Runic"));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
