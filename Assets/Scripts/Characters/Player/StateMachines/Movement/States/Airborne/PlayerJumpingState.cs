using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        private PlayerJumpData jumpData;
        /// <summary>通过判断进入该状态期间“移动”是否为0判断是否旋转
        /// <see cref = "shouldKeepRotationg"/>
        /// </summary>
        private bool shouldKeepRotationg;
        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            jumpData = airborneData.JumpData;

            stateMachine.ReusableData.RotationData = jumpData.RotationData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            shouldKeepRotationg = stateMachine.ReusableData.MovementInput != Vector2.zero;

            Jump();
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (shouldKeepRotationg)
            {
                RotateTowardsTargetRotation();

            }
        }
        #endregion

        #region Main Methods
        private void Jump()
        {
            // MARKER： 跳跃力 需要根据倾斜角度和跳跃方向进行更改！
            Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

            Vector3 jumpDirection = stateMachine.Player.transform.forward;

            // 判断是否按下了移动键 改变跳跃方向！！！ 否则会默认往玩家面前跳！！
            if (shouldKeepRotationg)
            {
                jumpDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            // TODO: 考虑更新碰撞体
            // 判断是否在斜坡上！  默认在斜坡上跳跃会陷进去！！
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapusuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            // 斜线检测与地面的距离！！
            if (Physics.Raycast(downwardsRayFromCapusuleCenter, out RaycastHit hit, jumpData.JumpToGroundRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore)) // 忽略“触发碰撞器”
            {
                // 判断是否在斜坡上
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapusuleCenter.direction);

                // 判断处于上坡时
                if (IsMovingUp())
                {

                }

                // 判断处于下坡时
                if (IsMovingDown())
                {

                }
            }

            // 跳跃前移除速度，防止影响“跳跃”力
            ResetVelocity();

            stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange); // 与时间质量无关
        }
        #endregion

    }
}
