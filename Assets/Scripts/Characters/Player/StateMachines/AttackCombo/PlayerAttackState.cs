using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackState : PlayerBaseGroundedState
    {
        [SerializeField] protected string stateName = "combo_01_1";
        public PlayerAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            // StopAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);

            // stateMachine.Player.Animator.Play(stateName);
            // Debug.Log(stateName);

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            stateMachine.ReusableData.isComboing = true;

            ResetVelocity();

            ResetCombo();
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // 当进入“停止”状态时，即使没有按下移动键，也会完成自动旋转！！
            RotateTowardsTargetRotation();
        }
        #endregion

        #region Reusable Methods
        protected void ResetCombo()
        {
            stateMachine.ReusableData.ShouldLightCombo = false;
            stateMachine.ReusableData.ShouldHeavyCombo = false;
        }

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
        #endregion

        #region Input Methods
        // protected override void OnAttackComboStarted(InputAction.CallbackContext context)
        // {
        //     base.OnAttackComboStarted(context);

        //     // stateMachine.ReusableData.ShouldLightCombo = true;
        //     // OnAttack();
        // }
        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            // 负责切换到移动状态
            OnMove();
        }

        protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldLightCombo = true;
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldHeavyCombo = true;
        }
        #endregion
    }
}
