using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
    {
        /// <summary> 玩家触地检测方块碰撞体数据
        /// <see cref="TriggerColliderData"/>
        /// </summary>
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            TriggerColliderData.Initialize();
        }

    }
}
