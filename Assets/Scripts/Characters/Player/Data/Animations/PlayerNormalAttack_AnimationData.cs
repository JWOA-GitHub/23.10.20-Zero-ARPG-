using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerNormalAttack_AnimationData
    {
        [field: SerializeField] public string NormalAttack_1_StateName = "combo_01_1";
        [field: SerializeField] public string NormalAttack_2_StateName = "combo_01_2";
        [field: SerializeField] public string NormalAttack_3_StateName = "combo_01_3";
    }
}
