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

        private bool keepSprinting;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModeifier;

            startTime = Time.time;
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


        public override void Exit()
        {
            base.Exit();

            keepSprinting = false;
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
        #endregion

        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingState);
        }
        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            keepSprinting = true;
        }
        #endregion
    }
}
