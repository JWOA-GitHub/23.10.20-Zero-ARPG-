
namespace JWOAGameSystem
{
    public class PlayerMovingState : PlayerGroundedState
    {
        public PlayerMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(animationData.MovingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.MovingParameterHash);
        }
        #endregion
    }
}
