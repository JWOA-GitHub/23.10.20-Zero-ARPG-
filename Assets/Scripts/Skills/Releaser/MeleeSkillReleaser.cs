using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary> 近战技能释放器
    /// </summary>
    public class MeleeSkillReleaser : SkillReleaser
    {
        public override void ReleaserSkill()
        {
            // 执行选区算法
            CalculateTargets();

            // 执行影响算法
        }
    }
}
