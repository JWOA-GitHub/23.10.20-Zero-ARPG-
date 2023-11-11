using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerJumpData
    {
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
