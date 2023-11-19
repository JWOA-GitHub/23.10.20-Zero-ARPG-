using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerTriggerColliderData
    {
        /// <summary> 玩家脚底触地检测方块碰撞体
        /// <see cref="GroundCheckCollider"/>
        /// </summary>
        [field: SerializeField] public BoxCollider GroundCheckCollider { get; private set; }

        public Vector3 GroundCheckColliderExtents { get; private set; }

        public void Initialize()
        {
            GroundCheckColliderExtents = GroundCheckCollider.bounds.extents;
        }
    }
}
