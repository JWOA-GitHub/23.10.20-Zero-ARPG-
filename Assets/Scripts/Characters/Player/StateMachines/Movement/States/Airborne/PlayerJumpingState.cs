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

            stateMachine.ReusableData.MovementDecelerationForce = jumpData.DecelerationForce;

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

            // 玩家跳起来到达顶部需要一定时间，看起来有点漂浮，因此向玩家的“垂直轴”添加力
            if (IsMovingUp())
            {
                // 增加垂直向上的力！！！
                DecelerateVertically();
            }
        }
        #endregion

        #region Resable Methods
        protected override void ResetSprintState()
        {
            // 除了“跳跃中”状态的每个“空中”状态都会重置“ShouldSprint”属性
            // base.ResetSprintState();
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
                    float forceModifier = jumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);

                    // 上坡时移动速度变化！ 0-20°为1，20-35°为0.75,35.1-75°为0.5， 75.1-90°为1.
                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                // 判断处于下坡时
                if (IsMovingDown())
                {
                    float forceModifier = jumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);

                    // 下坡时跳跃速度变化！ 0-20°、70.1-90°为1， 20-70°为0.85.
                    jumpForce.y *= forceModifier;
                }
            }

            // 跳跃前移除速度，防止影响“跳跃”力
            ResetVelocity();

            stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange); // 与时间质量无关
        }
        #endregion

    }
}
