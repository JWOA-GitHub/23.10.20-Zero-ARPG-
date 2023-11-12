using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerJumpData
    {
        /// <summary> 设置玩家跳跃期间旋转所需的时间！！
        /// <see cref = "RotationData"/>
        /// </summary>
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        /// <summary> 射线判断跳跃状态到地面的距离！！默认2f
        /// <see cref = "JumpToGroundRayDistance"/>
        /// </summary>
        [field: SerializeField][field: Range(0f, 5f)] public float JumpToGroundRayDistance { get; private set; } = 2f;
        /// <summary> 静止力
        /// <see cref = "StationaryForce"/>
        /// </summary>
        [field: SerializeField] public Vector3 StationaryForce { get; private set; }
        /// <summary> 弱力度
        /// <see cref = "WeakForce"/>
        /// </summary>
        [field: SerializeField] public Vector3 WeakForce { get; private set; }
        /// <summary> 中力度
        /// <see cref = "MediumForce"/>
        /// </summary>
        [field: SerializeField] public Vector3 MediumForce { get; private set; }
        /// <summary> 强力度
        /// <see cref = "StrongForce"/>
        /// </summary>
        [field: SerializeField] public Vector3 StrongForce { get; private set; }
    }
}
