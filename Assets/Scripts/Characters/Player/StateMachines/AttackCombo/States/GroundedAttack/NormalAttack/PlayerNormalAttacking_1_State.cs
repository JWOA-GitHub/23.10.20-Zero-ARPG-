using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_1_State : PlayerAttackState
    {
        public PlayerNormalAttacking_1_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            // stateMachine.Player.Animator.Play(stateName);
            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
        }

        public override void LogicUpdate()
        {
            // base.LogicUpdate();

            // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

        }

        public override void PhysicsUpdate()
        {
            // if (stateMachine.Player.AnimationData.animatorStateInfo.IsTag("combo_01_1"))
            //     stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_1_StateName;
            base.PhysicsUpdate();
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                // stateMachine.ChangeState(stateMachine.NormalAttacking_2_State);
                Debug.Log(11);
                return;
            }

            stateMachine.ChangeState(stateMachine.IdingState);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                ResetCombo();
                stateMachine.ChangeState(stateMachine.NormalAttacking_2_State);
            }
        }
        #endregion

        #region Input Methods
        protected override void OnAttackComboStarted(InputAction.CallbackContext context)
        {
            // stateMachine.ReusableData.ShouldLightCombo = true;
            base.OnAttackComboStarted(context);
            // Debug.Log("attack  1111111");
            // if (stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.7f)
            // Debug.Log(stateMachine.Player.AnimationData.animatorStateInfo.IsTag("combo_01_1") && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.5f);
            // if (stateMachine.Player.AnimationData.animatorStateInfo.IsTag("combo_01_1") && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.5f)
            // // {
            // //     // StartAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
            // {
            //     Debug.Log("1111111111111111111111111111111             ");
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_2_State);
            // }

            //     return;
            // }

        }
        #endregion


    }
}
