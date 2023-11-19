using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private PlayerFallData fallData;

        /// <summary>通过判断进入状态时的距离 与 触地时的距离 差 进入不同的着陆状态   
        /// <see cref="playerPositionOnEnter"/>
        /// </summary>
        private Vector3 playerPositionOnEnter;

        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airborneData.FallData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            playerPositionOnEnter = stateMachine.Player.transform.position;

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

        protected override void OnContactWithGround(Collider collider)
        {
            // TODO： 坠落伤害？
            // base.OnContactWithGround(collider);
            // 获取进入该状态时的位置 与 触地位置的距离！！
            // float fallDistance = Mathf.Abs(playerPositionOnEnter.y - stateMachine.Player.transform.position.y);
            // 当着陆高度在起始点上方则始终过渡到“轻着陆”状态
            float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;
            

            if (fallDistance < fallData.MinimumDisatanceToBeConsideredHardFall)
            {
                stateMachine.ChangeState(stateMachine.LightLandingState);

                return;
            }

            // 在WalkToggle关闭,并且未在疾跑时 才能过渡到“硬着陆”状态！！
            if (stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint || stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardLandingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.RollingState);
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
