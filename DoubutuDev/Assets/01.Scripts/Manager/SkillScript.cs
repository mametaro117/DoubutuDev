using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour {

    public enum SkillTyepe { none, BaffSkill, DebaffSkill, AttackSkill, ExSkill };
    public SkillTyepe skillType;

    void HealSkill(float HealValue) {
        Status totalstatus = GetComponent<Status>();
        totalstatus.HitPoint = Mathf.Min(totalstatus.MaxHP, totalstatus.HitPoint + HealValue);
        Debug.Log(HealValue);
    }
}
