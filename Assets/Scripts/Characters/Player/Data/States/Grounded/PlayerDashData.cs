using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField][field: Range(1f, 3f)] public float SpeedModeifier { get; private set; } = 2f;
        /// <summary> 默认旋转时间为0,14s，在Jump和Dash中旋转只需0.02s
        /// <see cref="RotationData"/>
        /// </summary>
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        /// <summary> 是否到达可连续冲刺时间！
        /// <see cref="TimeToBeConsideredConsecutive"/>
        /// </summary>
        [field: SerializeField][field: Range(0f, 2f)] public float TimeToBeConsideredConsecutive { get; private set; } = 1f;
        /// <summary> 可连续冲刺次数！
        /// <see cref="ConsecutiveDashesLimitAmount"/>
        /// </summary>
        [field: SerializeField][field: Range(1, 10)] public int ConsecutiveDashesLimitAmount { get; private set; } = 2;
        /// <summary> 达到冲刺冷却限制时间！
        /// <see cref="DashLimitReachedCooldown"/>
        /// </summary>
        [field: SerializeField][field: Range(0f, 5f)] public float DashLimitReachedCooldown { get; private set; } = 1.75f;

    }
}
