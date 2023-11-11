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

            stateMachine.ReusableData.MovementSpeedModifier = movementData.BaseSpeed;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }
        #endregion
    }
}
