using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status/Create Parameter", fileName = "AnimalParameter")]
public class UnitTable : ScriptableObject {
    public List<AnimalStatus> AnimalTable = new List<AnimalStatus>();
    public List<WeaponStatus> WeaponTable = new List<WeaponStatus>();
}

public enum AnimalName
{
    Rabbit = 0,
    Owl,
    Elephant,
    Penguin
};

[System.Serializable]
public struct AnimalStatus{
    public AnimalName animalName;
    public Sprite AnimalImage;
    public int AnimalCost;
    public AnimalScript.Skill Skill;
    public List<TypeStatus> typeStatuses;
}

[System.Serializable]
public struct TypeStatus{
    public Status.WeaponType Type;
    public GameObject Animal;
    public float HP;
    public float Power;
    public float MoveSpeed;
    public float AttackSpeed;
}

[System.Serializable]
public struct WeaponStatus{
    public string WeaponName;
    public Status.WeaponType Type;
    public Sprite WeaponImage;
    public int WeaponCost;
}