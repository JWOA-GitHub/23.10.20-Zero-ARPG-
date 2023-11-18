using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerFallData
    {
        /// <summary> 坠落速度限制（1-15f）默认15f，防止坠落重力过大而地面检测不到！
        /// <see cref="FallSpeedLimit"/>
        /// </summary>
        [field: SerializeField][field: Range(1f, 15f)] public float FallSpeedLimit { get; private set; } = 15f;
        /// <summary> 判断是否为重着陆的最小考虑距离！ 默认为3f 
        /// <see cref="MinimumDisatanceToBeConsideredHardFall"/>
        /// </summary>
        [field: SerializeField][field: Range(1f, 10f)] public float MinimumDisatanceToBeConsideredHardFall { get; private set; } = 3f;
    }
}
