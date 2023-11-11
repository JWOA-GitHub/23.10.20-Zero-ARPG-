using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            Jump();
        }
        #endregion

        #region Main Methods
        private void Jump()
        {
            // MARKER： 跳跃力 需要根据倾斜角度和跳跃方向进行更改！
            Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

            Vector3 paleyrForward = stateMachine.Player.transform.forward;

            jumpForce.x *= paleyrForward.x;
            jumpForce.z *= paleyrForward.z;

            // 跳跃前移除速度，防止影响“跳跃”力
            ResetVelocity();

            stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange); // 与时间质量无关
        }
        #endregion

    }
}
