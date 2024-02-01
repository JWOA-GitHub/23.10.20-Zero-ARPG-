using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>攻击选区的接口（目前有扇形、矩形）
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary> 搜索攻击目标
        /// </summary>
        /// <param name="data">技能数据</param>
        /// <param name="skillTF">技能所在物体的变换组件</param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData data, Transform skillTF);

    }
}
