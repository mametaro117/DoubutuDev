using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totalstatus : MonoBehaviour {

    public int HitPoint;
    public int Attack;
    public int Size;
    public bool IsSky;

    public void SetStatus(int HP,int ATK, int size, bool issky)
    {
        HitPoint = HP;
        Attack = ATK;
        Size = size;
        IsSky = issky;
    }
}
