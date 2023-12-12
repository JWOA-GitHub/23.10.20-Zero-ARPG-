
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_02_3_State : PlayerAttackState
    {
        public PlayerNormalAttacking_02_3_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_02_3_ParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_02_3_ParameterHash);

            ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                OnLAttack();
                return;
            }

            if (stateMachine.ReusableData.ShouldHeavyCombo)
            {
                OnRAttack();
                return;
            }

            // TODO：在非攻击状态 设置
            stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
        }
        #endregion

        #region Reusable Methods
        protected override void OnLAttack()
        {
            stateMachine.ChangeState(stateMachine.NormalAttacking_01_1_State);
        }

        protected override void OnRAttack()
        {
            stateMachine.ChangeState(stateMachine.NormalAttacking_02_1_State);
        }
        #endregion

        #region Input Methods
        protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnLAttackComboStarted(context);
            // stateMachine.ReusableData.ShouldLightCombo = true;
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);
        }
        #endregion


    }
}
