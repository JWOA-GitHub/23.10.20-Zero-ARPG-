using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerWalkingState : PlayerMovingState //PlayerGroundedState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            // 移动速度缓慢，类似走路
            stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
        }
        #endregion


        #region Input Methods
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
