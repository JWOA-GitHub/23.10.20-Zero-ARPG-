using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        private PlayerIdleData idleData;
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            idleData = movementData.IdleData;
        }

        #region  IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = movementData.BaseSpeed;

            stateMachine.ReusableData.BackwardsCameraRecenteringData = idleData.BackwardsCameraRecenteringData;

            base.Enter();

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
