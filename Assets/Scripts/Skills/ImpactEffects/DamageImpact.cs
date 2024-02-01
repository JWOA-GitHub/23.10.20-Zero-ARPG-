using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>
    /// 造成伤害
    /// </summary>
    public class DamageImpact : IImpactEffect
    {
        public void Execute(SkillReleaser releaser)
        {
            var ownerStatus = releaser.SkillData.Owner.GetComponent<CharactersBase>();

            for (int i = 0; i < releaser.SkillData.AttackTargets.Length; i++)
            {
                var status = releaser.SkillData.AttackTargets[i].GetComponent<CharactersBase>();
                status.Hp -= ownerStatus.AttackDamage * releaser.SkillData.AtkRatio;
                Debug.Log("     技能" + releaser.SkillData.Name + "攻击造成了：    " + ownerStatus.AttackDamage * releaser.SkillData.AtkRatio);
            }
        }
    }
}
