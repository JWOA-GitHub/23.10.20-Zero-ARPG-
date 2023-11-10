using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            // base.OnAnimationTransitionEvent();
            // TODO： 动画事件过渡到Idle状态！
            stateMachine.ChangeState(stateMachine.IdingState);
        }
        #endregion

        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started += ONMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started -= ONMovementStarted;
        }


        #endregion

        #region Input Methods
        // 无法在按下“移动”输入键的情况下进入“停止”状态（如“跳跃”状态按住移动键，而回调不会被调用，因为输入已被按下
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }

        private void ONMovementStarted(InputAction.CallbackContext context)
        {
            // 负责切换到移动状态
            OnMove();
        }
        #endregion
    }
}
