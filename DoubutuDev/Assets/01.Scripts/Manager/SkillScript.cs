using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour {

    public enum SkillName
    {
        BaffSkill,
        DebaffSkill,
        AttackSkill,
        ExSkill
    };

    public SkillName skillName;

    void HealSkill(float HealValue) {
        Totalstatus totalstatus = GetComponent<Totalstatus>();
        totalstatus.HitPoint = Mathf.Min(totalstatus.MaxHP, totalstatus.HitPoint + HealValue);
        Debug.Log(HealValue);
    }

    //void 

}
