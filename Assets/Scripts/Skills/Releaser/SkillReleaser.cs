using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public abstract class SkillReleaser : MonoBehaviour
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
        private IImpactEffect[] impactArray;

        //初始化释放器
        private void InitReleaser()
        {

            // 创建算法对象

            #region 选区对象算法 影响算法 已封装到 （释放器配置工厂：提供创建释放器各种算法对象的功能
            // #region 选取对象算法 skillData.SelectorType
            // // skillData.SelectorType
            // // 选区对象命名规则：
            // // JWOAGameSystem.Skill. + 枚举名+AttackSelector
            // // 例如扇形选区：JWOAGameSystem.Skill.SectorAttackSelector

            // // 反射：获取类型  判断对象  返回成员

            // // Type.GetType("JWOAGameSystem.Skill." + skillData.SelectorType + "AttackSelector");
            // string classNameSelector = string.Format("JWOAGameSystem.Skill.{0}AttackSelector", skillData.SelectorType);

            // // Type type = Type.GetType(className);
            // // selector = Activator.CreateInstance(type) as IAttackSelector;
            // selector = CreateObject<IAttackSelector>(classNameSelector);
            // #endregion

            // #region 影响效果算法 skillData.ImpactType
            // // 影响算法
            // // 影响效果命名规范：
            // // JWOAGameSystem.Skill. + impactType[?]+ Impact
            // // 例如消耗法力：JWOAGameSystem.Skill.CostSPImpact
            // for (int i = 0; i < skillData.ImpactType.Length; i++)
            // {
            //     string classNameImpact = string.Format("JWOAGameSystem.Skill.{0}Impact", skillData.ImpactType[i]);
            //     impactArray[i] = CreateObject<IImpactEffect>(classNameImpact);
            // }
            // #endregion
            #endregion

            //选区
            selector = ReleaserConfigFactory.CreateAttackSelector(SkillData); // 释放器的配置工厂类

            // 影响
            impactArray = ReleaserConfigFactory.CreateAttackImpact(SkillData);
        }


        #region CreateObject<T>(string className) 已封装到 （释放器配置工厂：提供创建释放器各种算法对象的功能
        // /// <summary>  使用默认构造函数创建指定类型的实例
        // /// </summary>
        // /// <typeparam name="T">获取具有指定名称的 Type 对象</typeparam>
        // /// <param name="className">传入相应的类型名字</param>
        // /// <returns></returns>
        // private T CreateObject<T>(string className) where T : class
        //     {
        //         Type type = Type.GetType(className);
        //         return Activator.CreateInstance(type) as T; //使用默认构造函数创建指定类型的实例
        //     }
        #endregion

        // 执行算法对象
        // 选区
        public void CalculateTargets()
        {
            skillData.AttackTargets = selector.SelectTarget(skillData, transform);

            /*******测试********/
            foreach (var item in skillData.AttackTargets)
            {
                print("攻击到了 " + item);
            }
        }

        // 影响


        // 释放方式
        // 供技能管理器调用，由子类实现，定义具体释放策略
        public abstract void ReleaserSkill();
    }
}
