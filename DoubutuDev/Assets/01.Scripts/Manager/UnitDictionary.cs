using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Animal/Create Animal Parameter", fileName = "AnimalParameter")]
public class UnitDictionary : ScriptableObject
{

    public string Name;
    public int HitPoint;
    public int Attack;
    public bool IsFlying;

}
