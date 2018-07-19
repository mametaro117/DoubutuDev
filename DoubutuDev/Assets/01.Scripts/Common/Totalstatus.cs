﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totalstatus : MonoBehaviour {

    //  ステータス
    public float HitPoint;
    public float MaxHP { get; private set; }
    public float SkillPoint;
    public float MaxSP { get; private set; }
    public int Attack;
    public float Speed;

    public bool IsSky;
    public bool isStun;

    private GameObject HPberObj;
    private GameObject SPberObj;


    public enum WeaponType
    {
        Sword, Shield, Arrow, Tower
    };

    public WeaponType weaponType = WeaponType.Sword;

    void Start()
    {
        MaxHP = HitPoint;
        //  どうぶつまたはエネミーだったらゲージを設定
        if(tag == "Animal" || tag == "Enemy")
        {
            HPberObj = transform.GetChild(0).GetChild(0).gameObject;
            SPberObj = transform.GetChild(1).GetChild(0).gameObject;
            MaxSP = SkillPoint;
            SkillPoint = 0;
            StartCoroutine(SkillPointUp());
            ApplayBer();
        }
    }
    public void SetStatus(float HP,int ATK, float SPD, bool issky)
    {
        HitPoint = HP;
        Attack = ATK;
        Speed = SPD;
        IsSky = issky;
    }
    
    //  ゲージの割合表示
    public void ApplayBer()
    {
        //  割合計算
        float HPparcent = HitPoint / MaxHP;
        float SPparcent = SkillPoint / MaxSP;
        //  0未満にしない
        HPparcent = Mathf.Max(0, HPparcent);
        SPparcent = Mathf.Max(0, SPparcent);
        //  Pivotオブジェのスケールを割合に適用
        HPberObj.transform.localScale = new Vector3(HPparcent, HPberObj.transform.localScale.y, HPberObj.transform.localScale.z);
        SPberObj.transform.localScale = new Vector3(SPparcent, SPberObj.transform.localScale.y, SPberObj.transform.localScale.z);
    }

    //  回復スキル用
    public void Heal(float HealPoint)
    {
        HitPoint = Mathf.Min(MaxHP, HitPoint + HealPoint);
        ApplayBer();
    }

    //Castのデバッグ用
    public void _CastTest()
    {
        Debug.Log("<color=red>Cast成功</color>");
    }

    IEnumerator SkillPointUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (SkillPoint >= MaxSP)
            {
                Debug.Log("Play");
                if (tag == "Animal")
                {
                    Debug.Log("Play2");
                    if (GetComponent<AnimalScript>().SkillReady())
                    {
                        GetComponent<AnimalScript>().PlaySkill();
                        SkillPoint = 0;
                    }                        
                    else
                        continue;                    
                }
                else if (tag == "Enemy")
                {
                    if (GetComponent<EnemyScript>().SkillReady())
                    {
                        GetComponent<EnemyScript>().PlaySkill();
                        SkillPoint = 0;
                    }
                    else
                        continue;
                }
            }
            Debug.Log("SkillPointUp");
            if(SkillPoint < MaxSP)
                SkillPoint += 0.1f;
            SkillPoint = Mathf.Min(SkillPoint, MaxSP);
            ApplayBer();
        }
        yield break;
    }
}
