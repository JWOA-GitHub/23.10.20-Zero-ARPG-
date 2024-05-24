using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class SkillData
    {
        /// <summary> 技能ID </summary>
        public int SkillID;
        /// <summary> 技能名称 </summary>
        public string Name;
        /// <summary> 技能描述 </summary>
        public string Description;
        /// <summary> 技能冷却时间 </summary>
        public int CoolTime;
        /// <summary> 技能冷却剩余时间 </summary>
        public int CoolRemainTime;
        /// <summary> 技能消耗的能量 </summary>
        public int CostMP;
        /// <summary> 技能攻击距离 </summary>
        public float AttackDistance;
        /// <summary> 技能攻击角度 </summary>
        public float AttackAngle;
        /// <summary> 技能攻击目标Tags </summary>
        public string[] AttackTargetTags = { "Enemy" };
        /// <summary> 技能攻击目标对象存放的数组 </summary>
        [HideInInspector]
        public Transform[] AttackTargets;
        /// <summary> 技能影响类型（在运行时决定） </summary>
        public string[] ImpactType = { "CostMP", "Damage" };
        /// <summary> 技能连击的下一个技能编号 </summary>
        public int NextBatterId;
        /// <summary> 技能伤害比率 </summary>
        public float AtkRatio;
        /// <summary> 技能持续时间 </summary>
        public float DurationTime;
        /// <summary> 技能伤害间隔 </summary>
        public float AtkInterval;
        /// <summary> 技能所属 </summary>
        [HideInInspector]
        public GameObject Owner;
        /// <summary> 技能预制件名称 </summary>
        public string PrefabName;
        /// <summary> 技能预制件对象 </summary>
        [HideInInspector]
        public GameObject SkillPrefab;
        /// <summary> 技能动画名称 </summary>
        public string AnimationName;
        ///<summary>受击特效名称</summary>
        public string HitFxName;
        ///<summary>受击特效预制件</summary>
        [HideInInspector]
        public GameObject HitFxPrefab;
        ///<summary>技能等级</summary>
        public int Level;
        /// <summary>技能攻击类型 单攻，群攻</summary>
        public SkillAttackType AttackType;
        ///<summary>技能选择攻击类型 扇形(圆形)，矩形</summary>
        public SelectorType SelectorType;
    }

    // public SkillData[] FindAll(Func<SkillData, bool> condition)
    // {

    // }
}
