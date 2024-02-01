using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>
    /// 消耗法力值
    /// </summary>
    public class CostMPImpact : IImpactEffect
    {
        // 依赖注入  控制反转
        public void Execute(SkillReleaser releaser)
        {
            var status = releaser.SkillData.Owner.GetComponent<CharactersBase>();
            status.Mp -= releaser.SkillData.CostMP;
            Debug.Log("     技能" + releaser.SkillData.Name + "使用MP：    " + releaser.SkillData.CostMP);
        }
    }
}
