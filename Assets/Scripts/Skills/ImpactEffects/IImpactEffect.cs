using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>影响效果算法接口
    /// </summary>
    public interface IImpactEffect
    {
        // 伤害生命 
        void Execute(SkillReleaser releaser);
    }
}
