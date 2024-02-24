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
        private SkillData data;
        public void Execute(SkillReleaser releaser)
        {
            // releaser.SkillData.attackTargets ---> CharactersBase.Hp
            data = releaser.SkillData;

            releaser.StartCoroutine(RepeatDamage(releaser));

            #region Old Test
            // var ownerStatus = releaser.SkillData.Owner.GetComponent<CharactersBase>();

            // for (int i = 0; i < releaser.SkillData.AttackTargets.Length; i++)
            // {
            //     var status = releaser.SkillData.AttackTargets[i].GetComponent<CharactersBase>();
            //     status.Hp -= ownerStatus.AttackDamage * releaser.SkillData.AtkRatio;
            //     Debug.Log("     技能" + releaser.SkillData.Name + "攻击造成了：    " + ownerStatus.AttackDamage * releaser.SkillData.AtkRatio);
            // }
            #endregion
        }

        // 重复伤害
        private IEnumerator RepeatDamage(SkillReleaser releaser)
        {
            float atkTime = 0;
            do
            {
                // 伤害目标生命
                OnceDamage();
                yield return new WaitForSeconds(data.AtkInterval);
                atkTime += data.AtkInterval;
                releaser.CalculateTargets();        // 重新计算
            } while (atkTime < data.DurationTime); // 攻击时间未结束
        }

        // 单次伤害
        private void OnceDamage()
        {
            // releaser.SkillData.attackTargets ---> CahractersBase Hp
            // 技能攻击力 ：攻击比率 * 基础攻击力
            float atk = data.AtkRatio * data.Owner.GetComponent<CharactersBase>().AttackDamage;
            for (int i = 0; i < data.AttackTargets.Length; i++)
            {
                var status = data.AttackTargets[i].GetComponent<CharactersBase>();
                status.GetDamage(atk);
            }

            // TODO：创建攻击特效
        }
    }
}
