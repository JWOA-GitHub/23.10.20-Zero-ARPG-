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

            // stateMachine.Player.Input.PlayerActions.LAttack.Disable();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_01_3_ParameterHash);

            // stateMachine.Player.Input.PlayerActions.LAttack.Enable();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.Player.Input.PlayerActions.LAttack.Enable();
            // Debug.Log("<color= #ffff1111> trans前   </color>" + stateMachine.ReusableData.ShouldLightCombo);
            // base.OnAnimationTransitionEvent();
            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                // Debug.Log("<color= #ffff1111>  轻击   </color>");
                // stateMachine.ChangeState(stateMachine.NormalAttacking_1_State);
                OnLAttack();
                return;
            }

            if (stateMachine.ReusableData.ShouldHeavyCombo)
            {
                // Debug.Log("             攻击3  combo2  ");
                OnRAttack();
                return;
            }
            // Debug.Log("      33333333");
            stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // if (stateMachine.ReusableData.ShouldLightCombo)
            // {
            stateMachine.ChangeState(stateMachine.IdingState);
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_1_State);
            // }
        }
        #endregion

        #region Reusable Methods
        protected override void OnLAttack()
        {
            Debug.Log("                                                 111111111111111111");
            stateMachine.ChangeState(stateMachine.NormalAttacking_01_4_State);
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
