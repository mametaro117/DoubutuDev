using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totalstatus : MonoBehaviour {

    //  ステータス
    public float HitPoint;
    public float MaxHP { get; private set; }
    public int Attack;
    public float Speed;
    public bool IsSky;

    public bool isStun;
    public bool isKnockback;

    private GameObject HPberObj;
    private GameObject SkillBerObj;


    [SerializeField]
    private bool isUnit = false;

    public enum WeaponType
    {
        Sword, Shield, Arrow, Tower
    };

    public WeaponType weaponType = WeaponType.Sword;


    void Start()
    {
        MaxHP = HitPoint;
        //Debug.Log(gameObject.name +" HP: " + MaxHP);
        //  HPバーオブジェクトがあったら
        if(tag == "Animal" || tag == "Enemy")
        {
            HPberObj = transform.GetChild(0).GetChild(0).gameObject;
            SkillBerObj = transform.GetChild(0).GetChild(1).gameObject;
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
        float parcent = HitPoint / MaxHP;
        parcent = Mathf.Max(0, parcent);
        HPberObj.transform.localScale = new Vector3(parcent, HPberObj.transform.localScale.y, HPberObj.transform.localScale.z);
    }

    public void Heal(float HealPoint)
    {
        HitPoint += Mathf.Min(MaxHP, HitPoint + HealPoint);
        ApplayBer();
    }

    //Castのデバッグ用
    public void _CastTest()
    {
        Debug.Log("<color=red>Cast成功</color>");
    }
}
