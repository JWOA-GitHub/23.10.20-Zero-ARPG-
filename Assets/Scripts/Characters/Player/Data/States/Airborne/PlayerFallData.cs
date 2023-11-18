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
    }
}
