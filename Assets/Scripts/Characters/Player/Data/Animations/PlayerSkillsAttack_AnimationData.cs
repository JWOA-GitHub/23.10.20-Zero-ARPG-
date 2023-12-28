using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerSkillsAttack_AnimationData
    {
        [field: SerializeField] public string SkillsAttack_01_StateName = "skills_01";
        [field: SerializeField] public string SkillsAttack_02_StateName = "skills_02";
        [field: SerializeField] public string SkillsAttack_03_StateName = "skills_03";
        [field: SerializeField] public string SkillsAttack_04_StateName = "skills_04";
        // [field: SerializeField] public string NormalAttack_02_1_StateName = "combo_02_1";
        // [field: SerializeField] public string NormalAttack_02_2_StateName = "combo_02_2";
        // [field: SerializeField] public string NormalAttack_02_3_StateName = "combo_02_3";
    }
}
