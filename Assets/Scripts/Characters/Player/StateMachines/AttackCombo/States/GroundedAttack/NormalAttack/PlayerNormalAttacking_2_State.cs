using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_2_State : PlayerAttackState
    {
        public PlayerNormalAttacking_2_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_2_StateName;
            base.Enter();

            // stateMachine.Player.Animator.Play(stateName);

            // StopAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_2_ParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_2_ParameterHash);
        }

        // public override void LogicUpdate()
        // {
        //     base.LogicUpdate();

        //     // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);
        //     // stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_1_StateName;

        // }

        public override void PhysicsUpdate()
        {
            // if (stateMachine.Player.AnimationData.animatorStateInfo.IsTag("combo_01_2"))
            //     stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_2_StateName;
            base.PhysicsUpdate();
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                // stateMachine.ChangeState(stateMachine.NormalAttacking_3_State);
                Debug.Log(22);
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
                stateMachine.ChangeState(stateMachine.NormalAttacking_3_State);
            }
        }
        #endregion

        #region Input Methods
        protected override void OnAttackComboStarted(InputAction.CallbackContext context)
        {
            // stateMachine.ReusableData.ShouldLightCombo = true;
            base.OnAttackComboStarted(context);

            // if (stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.7f)
            // if (stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.7f && stateMachine.Player.AnimationData.animatorStateInfo.IsTag("combo_01_2"))
            // {
            //     Debug.Log("                 3333333333333333      ");
            //     // StartAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_3_State);

            //     return;
            // }

            // StartAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);

            // stateMachine.ChangeState(stateMachine.NormalAttacking_3_State);
        }
        #endregion


    }
}
