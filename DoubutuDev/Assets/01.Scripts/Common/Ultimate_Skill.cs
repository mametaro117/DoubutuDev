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

    void UltimateSkill()
    {
        switch (WeaponNum = 0)  //近距離
        {
            case 1:             // ペンギンの近距離
                break;
            case 2:             // うさぎの近距離
                break;          
            case 3:             // ぞうの近距離
                break;  
            default:            // ふくろうの近距離
                break;
        }
        switch (WeaponNum = 1)  //盾
        {
            case 1:             // ぺんぎんの盾
                break;
            case 2:             // うさぎの盾
                break;
            case 3:             // ぞうの盾
                break;
            default:            // ふくろうの盾
                break;
        }
        switch (WeaponNum = 2)  //遠距離
        {
            case 1:             // ぺんぎんの遠距離
                break;
            case 2:             // うさぎの遠距離
                break;
            case 3:             // ぞうの遠距離
                break;
            default:            // ふくろうの遠距離
                break;
        }
    }
}
