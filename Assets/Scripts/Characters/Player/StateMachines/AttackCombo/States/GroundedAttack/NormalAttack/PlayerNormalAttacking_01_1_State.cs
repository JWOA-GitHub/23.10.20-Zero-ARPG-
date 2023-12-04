using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_01_1_State : PlayerAttackState
    {
        public PlayerNormalAttacking_01_1_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            // Debug.LogError(stateMachine.ReusableData.ShouldLightCombo);
            // stateMachine.Player.Animator.Play(stateName);
            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_01_1_ParameterHash);

            // ResetCombo();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_01_1_ParameterHash);

            // ResetCombo();
        }

        // public override void LogicUpdate()
        // {
        //     base.LogicUpdate();

        //     // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

        // }

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
            // if (stateMachine.ReusableData.ShouldLightCombo)
            // {
            Debug.Log("                             攻击1 Exit");
            stateMachine.ChangeState(stateMachine.IdingState);
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_2_State);
            // }
        }
        #endregion

        #region Reusable Methods
        protected override void OnLAttack()
        {
            // base.OnAttack();
            stateMachine.ChangeState(stateMachine.NormalAttacking_01_2_State);

        }

        protected override void OnRAttack()
        {
            // base.OnAttack();
            stateMachine.ChangeState(stateMachine.NormalAttacking_02_1_State);

        }
        #endregion

        #region Input Methods
        protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            // stateMachine.ReusableData.ShouldLightCombo = true;
            base.OnLAttackComboStarted(context);
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

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);
        }
        #endregion


    }
}
