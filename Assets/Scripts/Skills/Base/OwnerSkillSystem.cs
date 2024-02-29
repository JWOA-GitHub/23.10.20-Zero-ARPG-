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
        //private Animator animator;
        private SkillData skill;
        private void Start()
        {
            skillManager = GetComponent<OwnerSkillManager>();
            // animator = GetComponent<Animator>();
            //GetComponentInChildren<AnimationEventBehaviour>().attackHandler += ReleaserSkill;
        }

        private void ReleaserSkill(SkillData skill, Vector3 position = default, Quaternion rotation = default)
        {
            // 生成技能
            skillManager.GenerateSkill(skill, position,rotation);
        }

        /// <summary>
        /// 使用技能攻击（为玩家提供
        /// </summary>
        public void AttackUseSkill(int skillID,bool isBatter = false, Vector3 position = default, Quaternion rotation = default)
        {
            // 如果连击，则从上一个释放的技能中获取连击技能编号
            if (skill != null && isBatter)
                skillID = skill.NextBatterld; 

            // 准备技能
            skill = skillManager.PrepareSkill(skillID);
            if (skill == null) return;
            // 播放动画
            // animator.SetBool(skill.AnimationName, true);
            // 生成技能
            ReleaserSkill(skill, position, rotation);
            // 如果单攻
            if (skill.AttackType != SkillAttackType.Single) return;
            // -- 查找目标
            Transform targetTF = SelectTarget();
            // -- 朝向目标
            // TODO：
            transform.LookAt(targetTF);
            Debug.Log("     12222222222222" + targetTF);
            // -- 选中目标
            // 1. 选中目标，间隔指定时间后取消选中
            // 2. 选中A目标，在自动取消前，又选中B目标，则需要手动将A取消
            //   (核心：存储上次选中的物体！)
            // 先取消上次选中的物体
            SetSelectedActiveFx(false);
            selectedTarget = targetTF;
            // 选中当前物体
            SetSelectedActiveFx(true);
            // targetTF.Find("xxx").gameObject.SetActive(true);

        }

        private Transform SelectTarget()
        {
            Transform[] target = new SectorAttackSelector().SelectTarget(skill, transform);
            return target.Length != 0 ? target[0] : null;
        }

        //选中的目标
        public Transform selectedTarget;

        private void SetSelectedActiveFx(bool state)
        {
            if(selectedTarget == null ) return;
            var selected = selectedTarget.GetComponent<CharacterSelected>();
            if (selected)
                selected.SetSelectedActive(state);
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
