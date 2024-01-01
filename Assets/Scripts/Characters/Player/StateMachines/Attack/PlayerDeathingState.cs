using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerDeathingState : PlayerStoppingState
    {
        public PlayerDeathingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region Input Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            base.Enter();

            StartAnimation(animationData.DeathParameterHash);

            // 禁用“移动”按键输入！！ 在特定帧或结束时开启！
            stateMachine.Player.Input.PlayerActions.Movement.Disable();

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.DeathParameterHash);

            stateMachine.Player.Input.PlayerActions.Movement.Enable();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            // 正在移动则移除速度，防止滑行
            ResetVelocity();
        }

        public override void OnAnimationExitEvent()
        {
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.ChangeState(stateMachine.IdingState);
            return;
        }
        #endregion

        #region Resusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        }

        // “硬着陆状态” 输入“移动”按键 只能过渡到 “跑步”状态！
        protected override void OnMove()
        {
            // "硬着陆"无法过渡到“行走” ！！ 只能过渡到“跑步”
            if (stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
            return;
        }
        #endregion

        #region Input Methods
        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            // MARKER: “硬着陆”与“滚动”状态无法过渡到“跳跃”状态！
            // base.OnJumpStarted(context);
        }
        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }
        #endregion
    }
}
