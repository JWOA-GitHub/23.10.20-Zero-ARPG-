using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region  IState Methods
        public override void Enter()
        {
            base.Enter();

            speedModeifier = 0f;

            ResetVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (movementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }

        // private void OnMove()
        // {
        //     // 判断 Walk Toggle On 则过渡到“步行状态”，否则，过渡到“跑步”状态
        //     if (shouldWalk)
        //     {
        //         stateMachine.ChangeState(stateMachine.WalkingState);

        //         return;
        //     }

        //     stateMachine.ChangeState(stateMachine.RunningState);
        // }
        #endregion
    }
}
