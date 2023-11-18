using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private PlayerFallData fallData;
        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airborneData.FallData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVerticalVelocity();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }
        #endregion

        #region Reusable Methods
        protected override void ResetSprintState()
        {
            // 坠落状态不需要重置shouldSprint
            // base.ResetSprintState();
        }
        #endregion

        #region Main Methods
        /// <summary> 通过限制垂直方向的速度来防止下降时重力过大而触地时无法检测到碰撞的速度穿过地面层！
        /// </summary>
        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            // 即 y轴速度 >= -15!! 不能超过 -14！
            if (stateMachine.Player.Rigidbody.velocity.y >= -fallData.FallSpeedLimit)
            {
                return;
            }

            // 获取限制差值
            Vector3 limitedVelocity = new Vector3(0f, -fallData.FallSpeedLimit - stateMachine.Player.Rigidbody.velocity.y, 0f);

            // TODO：character
            stateMachine.Player.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);  // 无视质量无视时间变化
        }
        #endregion

    }
}
