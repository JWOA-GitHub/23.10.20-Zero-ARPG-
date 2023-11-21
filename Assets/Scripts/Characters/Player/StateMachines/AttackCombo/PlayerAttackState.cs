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

            ResetVelocity();
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            // ResetCombo();
            // StopAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
        }


        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
        }
        #endregion

        #region Reusable Methods
        protected void ResetCombo()
        {
            stateMachine.ReusableData.ShouldLightCombo = false;
        }
        // protected override void AddInputActionsCallbacks()
        // {
        //     base.AddInputActionsCallbacks();
        // }

        // protected override void RemoveInputActionsCallbacks()
        // {
        //     base.RemoveInputActionsCallbacks();
        // }
        #endregion

        #region Input Methods
        protected override void OnAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnAttackComboStarted(context);

            stateMachine.ReusableData.ShouldLightCombo = true;
        }
        #endregion
    }
}
