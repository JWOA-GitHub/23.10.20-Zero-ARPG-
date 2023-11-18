using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerRunningState : PlayerMovingState  //PlayerGroundedState
    {
        private PlayerSprintData sprintData;
        private float startTime;
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

            base.Enter();

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;

            startTime = Time.time;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // 判断为行走状态或跑步状态
            if (!stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            // 若未达到 过渡到行走状态 时间
            if (Time.time < startTime + sprintData.RunToWalkTime)
            {
                return;
            }

            StopRunning();
        }

        #endregion

        #region Main Methods
        private void StopRunning()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                // TODO：若未按下移动输入，则切换为 “中停止”状态 未有状态因此切换为Idle
                stateMachine.ChangeState(stateMachine.IdingState);

                return;
            }

            // 若输入移动则切换到行走状态
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
        #endregion

        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.MediumStoppingState);

            base.OnMovementCanceled(context);
        }

        /// <summary> 按下使奔跑状态切换到行走状态
        /// </summary>
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.WalkingState);
        }

        #endregion
    }
}
