using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);

            // TODO: 跳跃是否能攻击
            stateMachine.Player.Input.PlayerActions.LAttack.Disable();
            stateMachine.Player.Input.PlayerActions.RAttack.Disable();

            // 每次进入“空中”状态时，ShouldSprint都重置为false
            ResetSprintState();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);

            stateMachine.Player.Input.PlayerActions.LAttack.Enable();
            stateMachine.Player.Input.PlayerActions.RAttack.Enable();
        }
        #endregion

        #region Reusable Methods
        protected override void OnContactWithGround(Collider collider)
        {
            // stateMachine.ChangeState(stateMachine.IdingState);
            stateMachine.ChangeState(stateMachine.LightLandingState);
        }

        /// <summary> 重置ShouldSprint为false！
        /// </summary>
        protected virtual void ResetSprintState()
        {
            stateMachine.ReusableData.ShouldSprint = false;
        }
        #endregion
    }
}
