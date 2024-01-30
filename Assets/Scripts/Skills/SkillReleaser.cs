using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class SkillReleaser : MonoBehaviour
    {
        private SkillData skillData;
        /// <summary>   由技能管理器提供
        /// </summary>
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                //创建算法对象
                InitReleaser();
            }
        }

        /// <summary> 选区算法对象
        /// </summary>
        IAttackSelector selector;
        /// <summary> 影响效果算法对象
        /// </summary>
        private IImpactEffect[] impact;

        //初始化释放器
        private void InitReleaser()
        {
            // 创建算法对象
            // skillData.SelectorType
            // 选区对象命名规则：
            // JWOAGameSystem.Skill. + 枚举名+AttackSelector
            // 例如扇形选区：JWOAGameSystem.Skill.SectorAttackSelector

            // 反射：获取类型  判断对象  返回成员

            // Type.GetType("JWOAGameSystem.Skill." + skillData.SelectorType + "AttackSelector");
            string classNameSelector = string.Format("JWOAGameSystem.Skill.{0}AttackSelector", skillData.SelectorType);

            // Type type = Type.GetType(className);
            // selector = Activator.CreateInstance(type) as IAttackSelector;
            selector = CreateObject<IAttackSelector>(classNameSelector);

            // 影响
            // 影响效果命名规范：
            // JWOAGameSystem.Skill. + impactType[?]+ Impact
            // 例如消耗法力：JWOAGameSystem.Skill.CostSPImpact
            for (int i = 0; i < skillData.ImpactType.Length; i++)
            {
                string classNameImpact = string.Format("JWOAGameSystem.Skill.{0}Impact", skillData.ImpactType[i]);
                impact[i] = CreateObject<IImpactEffect>(classNameImpact);
            }
            // ReleaserConfigFactory// 释放器的配置工厂类
        }

        /// <summary>  使用默认构造函数创建指定类型的实例
        /// </summary>
        /// <typeparam name="T">获取具有指定名称的 Type 对象</typeparam>
        /// <param name="className">传入相应的类型名字</param>
        /// <returns></returns>
        private T CreateObject<T>(string className) where T : class
        {
            Type type = Type.GetType(className);
            return Activator.CreateInstance(type) as T; //使用默认构造函数创建指定类型的实例
        }


        // 执行算法对象

        // 释放方式
    }
}
