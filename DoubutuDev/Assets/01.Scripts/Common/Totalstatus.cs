﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totalstatus : MonoBehaviour {

    //  ステータス
    public float HitPoint;
    public int Attack;
    public float Speed;
    public bool IsSky;

    [SerializeField]
    private bool isUnit = false;

    public enum WeaponType
    {
        Sword, Shield, Arrow, Tower
    };

    public WeaponType weaponType = WeaponType.Sword;

    public float MaxHP { get; private set; }
    private GameObject BerObj;

    void Start()
    {
        MaxHP = HitPoint;
        //Debug.Log(gameObject.name +" HP: " + MaxHP);
        //  HPバーオブジェクトがあったら
        if(isUnit)
            BerObj = transform.GetChild(0).GetChild(0).gameObject;
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
        float parcent = HitPoint / MaxHP;
        parcent = Mathf.Max(0, parcent);
        BerObj.transform.localScale = new Vector3(parcent, BerObj.transform.localScale.y, BerObj.transform.localScale.z);
    }

    public void Heal(float HealPoint)
    {
        HitPoint += Mathf.Min(MaxHP, HitPoint + HealPoint);
        ApplayBer();
    }
}
