using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerRunningState : PlayerGroundedState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            speedModeifier = 1f;
        }
        #endregion

        // #region Reusable Methods 可复用方法
        // protected override void AddInputActionsCallbacks()
        // {
        //     base.AddInputActionsCallbacks();

        //     stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        // }

        // protected override void RemoveInputActionsCallbacks()
        // {
        //     base.RemoveInputActionsCallbacks();

        //     stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        // }
        // #endregion

        #region Input Methods
        /// <summary> 按下使奔跑状态切换到行走状态
        /// </summary>
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.WalkingState);
        }

        // /// <summary> 松开移动按键恢复到空闲状态
        // /// </summary>
        // private void OnMovementCanceled(InputAction.CallbackContext context)
        // {
        //     stateMachine.ChangeState(stateMachine.IdingState);
        // }
        #endregion
    }
}
