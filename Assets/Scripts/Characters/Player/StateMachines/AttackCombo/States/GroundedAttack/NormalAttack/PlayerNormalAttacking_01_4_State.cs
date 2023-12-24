using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_01_4_State : PlayerAttackState
    {
        public PlayerNormalAttacking_01_4_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            // Attack中获取当前正在播放的动画状态信息 设置当前状态对应AnimationName
            stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_01_4_StateName;

            // stateMachine.Player.Animator.Play(stateName);
            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_01_4_ParameterHash);

            // stateMachine.Player.Input.PlayerActions.LAttack.Disable();
            stateMachine.Player.Input.PlayerActions.Movement.Disable();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_01_4_ParameterHash);

            // stateMachine.Player.Input.PlayerActions.LAttack.Enable();
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.Player.Input.PlayerActions.LAttack.Enable();

            // base.OnAnimationTransitionEvent();
            if (stateMachine.ReusableData.ShouldLightCombo)
            {
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

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.3f)
            {
                Debug.Log("<color=yellow>  连击回 1111</color>");
                OnLAttack();
                return;
            }
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.3f)
            {
                Debug.Log("<color=green>  轻连击4444444 回 重·1111111</color>");
                OnRAttack();
                return;
            }
        }
        #endregion


    }
}
