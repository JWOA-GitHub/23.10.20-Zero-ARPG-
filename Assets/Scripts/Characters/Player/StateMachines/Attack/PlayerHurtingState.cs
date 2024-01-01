using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerHurtingState : PlayerStoppingState
    {
        public PlayerHurtingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region Input Methods
        public override void Enter()
        {
            Debug.Log("<color=red>          受伤状态</color>");
            stateMachine.ReusableData.MovementSpeedModifier = 0f;
            stateMachine.Player.IsHurting = true;

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.HurtParameterHash);
            // stateMachine.Player.Animator.Play("GetHit");
            Debug.Log("     播放受伤动画");
            // 禁用“移动”按键输入！！ 在特定帧或结束时开启！
            stateMachine.Player.Input.PlayerActions.Disable();
            Debug.Log("关闭按键输入");

            ResetVelocity();
            Debug.Log("停止移动");
        }

        public override void Exit()
        {
            Debug.Log("<color=red>          退出    受伤状态</color>");
            stateMachine.Player.IsHurting = false;

            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.HurtParameterHash);

            stateMachine.Player.Input.PlayerActions.Enable();
        }

        // public override void PhysicsUpdate()
        // {
        //     base.PhysicsUpdate();
        //     Debug.Log("             正在受伤Update");
        //     if (!IsMovingHorizontally())
        //     {
        //         return;
        //     }

        //     // 正在移动则移除速度，防止滑行
        //     ResetVelocity();
        // }

        // public override void OnAnimationExitEvent()
        // {
        //     stateMachine.Player.Input.PlayerActions.Movement.Enable();
        // }

        public override void OnAnimationTransitionEvent()
        {
            Debug.Log("         受伤 自动切换到 idle");
            stateMachine.ChangeState(stateMachine.IdingState);
            return;
        }
        #endregion

        #region Resusable Methods
        // protected override void AddInputActionsCallbacks()
        // {
        //     base.AddInputActionsCallbacks();

        //     stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        // }

        // protected override void RemoveInputActionsCallbacks()
        // {
        //     base.RemoveInputActionsCallbacks();

        //     stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        // }

        // // “受伤状态” 输入“移动”按键 只能过渡到 “跑步”状态！
        // protected override void OnMove()
        // {
        //     // "受伤"无法过渡到“行走” ！！ 只能过渡到“跑步”
        //     if (stateMachine.ReusableData.ShouldWalk)
        //     {
        //         return;
        //     }

        //     stateMachine.ChangeState(stateMachine.RunningState);
        //     return;
        // }
        #endregion

        #region Input Methods
        // protected override void OnJumpStarted(InputAction.CallbackContext context)
        // {
        //     // MARKER: “受伤”"硬着陆"与“滚动”状态无法过渡到“跳跃”状态！
        //     // base.OnJumpStarted(context);
        // }
        // private void OnMovementStarted(InputAction.CallbackContext context)
        // {
        //     OnMove();
        // }
        #endregion
    }
}
