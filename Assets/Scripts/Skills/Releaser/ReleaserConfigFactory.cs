using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>   释放器配置工厂：提供创建释放器各种算法对象的功能
    /// 作用：将对象的创建 与 使用分离。
    /// </summary>
    public class ReleaserConfigFactory
    {
        public static IAttackSelector CreateAttaackSelector(SkillData data)
        {
            #region 选取对象算法 skillData.SelectorType
            // skillData.SelectorType
            // 选区对象命名规则：
            // JWOAGameSystem.Skill. + 枚举名+AttackSelector
            // 例如扇形选区：JWOAGameSystem.Skill.SectorAttackSelector
            string classNameSelector = string.Format("JWOAGameSystem.Skill.{0}AttackSelector", data.SelectorType);
            return CreateObject<IAttackSelector>(classNameSelector);
            #endregion
        }

        public static IImpactEffect[] CreateAttaackImpact(SkillData data)
        {
            #region 影响效果算法 skillData.ImpactType
            IImpactEffect[] impacts = new IImpactEffect[data.ImpactType.Length];
            // 影响效果算法
            // 影响效果命名规范：
            // JWOAGameSystem.Skill. + impactType[?]+ Impact
            // 例如消耗法力：JWOAGameSystem.Skill.CostSPImpact
            for (int i = 0; i < data.ImpactType.Length; i++)
            {
                string classNameImpact = string.Format("JWOAGameSystem.Skill.{0}Impact", data.ImpactType[i]);
                impacts[i] = CreateObject<IImpactEffect>(classNameImpact);
            }

            return impacts;
            #endregion

        }

        /// <summary>  使用默认构造函数创建指定类型的实例
        /// </summary>
        /// <typeparam name="T">获取具有指定名称的 Type 对象</typeparam>
        /// <param name="className">传入相应的类型名字</param>
        /// <returns></returns>
        private static T CreateObject<T>(string className) where T : class
        {
            Type type = Type.GetType(className);
            return Activator.CreateInstance(type) as T; //使用默认构造函数创建指定类型的实例
        }
    }
}
