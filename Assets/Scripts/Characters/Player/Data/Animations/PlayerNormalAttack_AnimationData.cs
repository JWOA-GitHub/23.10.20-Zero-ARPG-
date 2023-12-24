using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerNormalAttack_AnimationData
    {
        [field: SerializeField] public string NormalAttack_01_1_StateName = "combo_01_1";
        [field: SerializeField] public string NormalAttack_01_2_StateName = "combo_01_2";
        [field: SerializeField] public string NormalAttack_01_3_StateName = "combo_01_3";
        [field: SerializeField] public string NormalAttack_01_4_StateName = "combo_01_4";
        [field: SerializeField] public string NormalAttack_02_1_StateName = "combo_02_1";
        [field: SerializeField] public string NormalAttack_02_2_StateName = "combo_02_2";
        [field: SerializeField] public string NormalAttack_02_3_StateName = "combo_02_3";
    }
}
