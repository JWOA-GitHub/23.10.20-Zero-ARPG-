using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JWOAGameSystem
{
    [RequireComponent(typeof(OwnerSkillManager))]
    /// <summary>
    /// 封装技能系统，提供简单的技能释放功能。
    /// </summary>
    public class OwnerSkillSystem : MonoBehaviour
    {
        private OwnerSkillManager skillManager;
        private Animator animator;
        private void Start()
        {
            skillManager = GetComponent<OwnerSkillManager>();
            // animator = GetComponent<Animator>();
            //GetComponentInChildren<AnimationEventBehaviour>().attackHandler += ReleaserSkill;
        }

        private void ReleaserSkill(SkillData skill)
        {
            // 生成技能
            skillManager.GenerateSkill(skill);
        }

        /// <summary>
        /// 使用技能攻击（为玩家提供
        /// </summary>
        public void AttackUseSkill(int skillID)
        {
            // 准备技能
            SkillData skill = skillManager.PrepareSkill(skillID);
            if (skill == null) return;
            // 播放动画
            // animator.SetBool(skill.AnimationName, true);
            // 生成技能
            ReleaserSkill(skill);
            // 如果单攻
            // skill.AttackType == SkillAttackType.Single;
            // -- 朝向目标
            // transform.LookAt();
            // -- 选中目标
            // 1. 选中目标，间隔指定时间后取消选中
            // 2. 选中A目标，在自动取消前，又选中B目标，则需要手动将A取消

        }

        /// <summary>
        /// 使用随机技能（为NPC提供）
        /// </summary>
        public void UseRandomSkill()
        {
            // 需求： 从 管理器 中挑选出随机的技能
            // -- 先筛选出所有可以释放的技能，再产生随机数。
            // var usableSkills = skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.skillID) != null);
            List<SkillData> usableSkills = skillManager.skills.Where(skill => skillManager.PrepareSkill(skill.SkillID) != null).ToList();
            if (usableSkills.Count == 0) return;
            int index = Random.Range(0, usableSkills.Count);
            AttackUseSkill(usableSkills[index].SkillID);
        }
    }
}
