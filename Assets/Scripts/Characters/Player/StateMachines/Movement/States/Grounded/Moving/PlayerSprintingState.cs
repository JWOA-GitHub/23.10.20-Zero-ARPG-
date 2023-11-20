using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerSprintingState : PlayerMovingState //PlayerMovementState
    {
        private PlayerSprintData sprintData;

        /// <summary>状态开始时间，判断在过渡之前可以在这里停留多长时间
        /// <see cref="startTime"/>
        /// </summary>
        private float startTime;

        /// <summary> 判断按住一定时间后才持续冲刺状态，否则松开手就退出
        /// <see cref="keepSprinting"/>
        /// </summary>
        private bool keepSprinting;
        /// <summary> 判断是否重置疾跑状态！ 
        /// <see cref="shouldResetSprintState"/>
        /// </summary>
        private bool shouldResetSprintState;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModeifier;

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

            shouldResetSprintState = true;

            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

            // 若不重置“疾跑”状态，则一旦“着陆”就开始“疾跑”
            if (shouldResetSprintState)
            {
                keepSprinting = false;

                stateMachine.ReusableData.ShouldSprint = false;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (keepSprinting)
            {
                return;
            }

            // 判断时间是否足够进入跑步状态 （在没有持续按住
            if (Time.time < startTime + sprintData.SprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }
        #endregion

        #region Main Methods
        /// <summary> 若没有按下移动则切换到“跑步”状态或“硬停止”状态
        /// </summary>
        private void StopSprinting()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                // TODO：尚未有停止状态，因此转换到空闲状态
                stateMachine.ChangeState(stateMachine.IdingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }
        #endregion

        #region Reusable Methods 可复用方法
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
        }
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
        }

        protected override void OnFall()
        {
            // MARKER：确保只有按住一定时间后才会打开持续疾跑
            shouldResetSprintState = false;

            base.OnFall();
        }
        #endregion

        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingState);

            base.OnMovementCanceled(context);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            // 在currentState.Exit前调用！！
            shouldResetSprintState = false;

            base.OnJumpStarted(context);
        }

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            keepSprinting = true;

            // 在“跳跃”和“着陆”后保留“疾跑”！
            stateMachine.ReusableData.ShouldSprint = true;
        }
        #endregion
    }
}
