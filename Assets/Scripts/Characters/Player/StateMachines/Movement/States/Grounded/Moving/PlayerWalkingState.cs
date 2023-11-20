using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerWalkingState : PlayerMovingState //PlayerGroundedState
    {
        private PlayerWalkData walkData;

        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            walkData = movementData.WalkData;
        }

        #region IState Methods
        public override void Enter()
        {
            // 移动速度缓慢，类似走路
            stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;

            stateMachine.ReusableData.BackwardsCameraRecenteringData = walkData.BackwardsCameraRecenteringData;

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

            SetBaseCameraRecenteringData();
        }
        #endregion


        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.LightStoppingState);

            base.OnMovementCanceled(context);
        }

        /// <summary> 按下使行走状态切换到奔跑状态
        /// </summary>
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.RunningState);
        }

        #endregion
    }
}
