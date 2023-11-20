using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);

            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);
        }
        #endregion

        #region Reusable Methods
        protected override void OnMove()
        {
            // 默认会切换回空闲状态！无需操作，但无法切换回行走状态，可切换回跑步状态！！
            if (stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }
        #endregion
    }
}
