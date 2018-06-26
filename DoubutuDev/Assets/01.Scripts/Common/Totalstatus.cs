using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totalstatus : MonoBehaviour {

    //  ステータス
    public float HitPoint;
    public int Attack;
    public int Size;
    public bool IsSky;

    public enum WeaponType
    {
        Sword, Shield, Arrow
    };

    public WeaponType weaponType = WeaponType.Sword;


    private float MaxHP;
    private GameObject BerObj;

    void Start()
    {
        MaxHP = HitPoint;
        Debug.Log(gameObject.name +"のHP: " + MaxHP);
        //  HPバーオブジェクトがあったら
        if(transform.childCount != 0)
            BerObj = transform.GetChild(0).GetChild(0).gameObject;        
    }

    public void SetStatus(float HP,int ATK, int size, bool issky)
    {
        HitPoint = HP;
        Attack = ATK;
        Size = size;
        IsSky = issky;
    }
    
    //  ゲージの割合表示
    public void ApplayBer()
    {
        float parcent = HitPoint / MaxHP;
        parcent = Mathf.Max(0, parcent);
        BerObj.transform.localScale = new Vector3(parcent, BerObj.transform.localScale.y, BerObj.transform.localScale.z);
    }
}
