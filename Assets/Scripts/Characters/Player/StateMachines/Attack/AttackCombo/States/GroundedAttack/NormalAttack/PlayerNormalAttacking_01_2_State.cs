using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_01_2_State : PlayerAttackComboState
    {
        public PlayerNormalAttacking_01_2_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            // Attack中获取当前正在播放的动画状态信息 设置当前状态对应AnimationName
            stateName = animationData.NormalAttack_AnimationData.NormalAttack_01_2_StateName;

            SetAnimationMoveBase(stateMachine.Player.transform.forward, 1.5f);

            // stateMachine.Player.effectManager.SpawnEffect("Combo2", stateMachine.Player.effectManager.effects[1].prefab.transform);

            StartAnimation(animationData.NormalAttack_01_2_ParameterHash);

        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.NormalAttack_01_2_ParameterHash);

            ResetCombo();
        }

        // public override void LogicUpdate()
        // {
        //     base.LogicUpdate();

        //     // animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);
        //     // stateName = animationData.NormalAttack_AnimationData.NormalAttack_1_StateName;

        // }

        public override void PhysicsUpdate()
        {
            // if (animationData.animatorStateInfo.IsTag("combo_01_2"))
            //     stateName = animationData.NormalAttack_AnimationData.NormalAttack_2_StateName;
            base.PhysicsUpdate();

            if (isEffecting && animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.1f)
            {
                stateMachine.Player.effectManager.SpawnEffect("Combo2", stateMachine.Player.effectManager.effects[1].prefab.transform);
                isEffecting = false;
            }
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                // stateMachine.ChangeState(stateMachine.NormalAttacking_3_State);
                OnLAttack();
                return;
            }

            if (stateMachine.ReusableData.ShouldHeavyCombo)
            {
                OnRAttack();
                return;
            }


        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            // stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
            return;
            // if (stateMachine.ReusableData.ShouldLightCombo)
            // {
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_3_State);
            // }
        }
        #endregion

        #region Reusable Methods
        protected override void OnLAttack()
        {
            // base.OnAttack();
            // if (stateMachine.ReusableData.shouldLightCombo)
            stateMachine.ChangeState(stateMachine.NormalAttacking_01_3_State);
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
            base.OnLAttackComboStarted(context);

            if (animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.1f)
            {
                Debug.Log("<color=yellow>  连击33333333</color>");
                OnLAttack();
                return;
            }
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);

            if (animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.1f)
            {
                Debug.Log("<color=green>  轻连击22222222 回 重·1111111</color>");
                OnRAttack();
                return;
            }
        }
        #endregion


    }
}
