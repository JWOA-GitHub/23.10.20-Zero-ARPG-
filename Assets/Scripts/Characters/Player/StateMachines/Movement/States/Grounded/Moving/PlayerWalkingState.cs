using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerWalkingState : PlayerGroundedState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            // 移动速度缓慢，类似走路
            speedModeifier = 0.225f;
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
        /// <summary> 按下使行走状态切换到奔跑状态
        /// </summary>
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.RunningState);
        }

        // /// <summary> 松开移动按键恢复到空闲状态
        // /// </summary>
        // protected void OnMovementCanceled(InputAction.CallbackContext context)
        // {
        //     stateMachine.ChangeState(stateMachine.IdingState);
        // }
        #endregion
    }
}
