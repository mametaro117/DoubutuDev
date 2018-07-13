using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Skill : MonoBehaviour
{
    [SerializeField]
    private Totalstatus totalstatus;

    int WeaponNum;
    void Start()
    {
       WeaponNum = (int)totalstatus.weaponType;
    }

    void UltimateSkill(GameObject obj)
    {
        switch (WeaponNum)
        {
            case 0:     //Sword
                if (obj)   //ぞう
                {

                }
                break;
            case 1:     //Shield
                if (obj)
                {

                }
                break;
            case 2:     //Arrow
                if (obj)
                {

                }
                break;
            default:

                break;
        }
    }
}
