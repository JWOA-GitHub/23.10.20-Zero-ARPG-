using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_01_3_State : PlayerAttackState
    {
        public PlayerNormalAttacking_01_3_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            // stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_3_StateName;

            base.Enter();
            // stateMachine.Player.Animator.Play(stateName);
            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_01_3_ParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_01_3_ParameterHash);
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
                // stateMachine.ChangeState(stateMachine.NormalAttacking_1_State);
                OnAttack();
                return;
            }

            stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // if (stateMachine.ReusableData.ShouldLightCombo)
            // {
            ResetCombo();
            stateMachine.ChangeState(stateMachine.IdingState);
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_1_State);
            // }
        }
        #endregion

        #region Reusable Methods
        protected override void OnAttack()
        {
            // base.OnAttack();
            // if (stateMachine.ReusableData.shouldLightCombo)
            stateMachine.ChangeState(stateMachine.NormalAttacking_01_1_State);
        }
        #endregion

        #region Input Methods
        protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldLightCombo = true;
            // base.OnAttackComboStarted(context);

            // if (stateMachine.Player.Animator.GetAnimatorTransitionInfo(0).userNameHash.ToString() != stateName)
            // {
            //     return;
            // }

            // if (stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.9f)
            // {
            //     // StartAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_1_State);

            //     return;
            // }

        }
        #endregion


    }
}
