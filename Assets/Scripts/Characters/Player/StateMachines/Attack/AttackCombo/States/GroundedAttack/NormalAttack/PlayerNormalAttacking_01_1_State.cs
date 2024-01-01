using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_01_1_State : PlayerAttackComboState
    {
        public PlayerNormalAttacking_01_1_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {

            base.Enter();

            // Attack中获取当前正在播放的动画状态信息 设置当前状态对应AnimationName
            stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_01_1_StateName;

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

        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // if (stateMachine.ReusableData.ShouldLightCombo)
            // {
            // TODO：在非攻击状态 设置

            stateMachine.ChangeState(stateMachine.IdingState);
            return;
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
            // 获取当前正在播放的动画状态信息
            AnimatorStateInfo stateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            // 获取当前正在播放的动画状态信息
            // AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // 要比较的动画名称
            // string animationToCheck = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_1_StateName;

            // // 检查当前播放的动画是否与指定的动画名称匹配
            // if (stateInfo.IsName(animationToCheck))
            // {
            //     Debug.Log("当前播放的动画是：" + animationToCheck);
            //     // 在这里执行你想要的操作
            // }
            // else
            // {
            //     Debug.Log("当前没有播放指定的动画：" + animationToCheck);
            // }

            // // 获取当前播放动画的名称
            // string currentAnimationName = stateInfo.IsName(stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_1_StateName) ? "YourAnimationNameHere" : "Not Playing";
            // // string currentAnimationName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_1_StateName;
            // Debug.Log("当前播放的动画名称：" + currentAnimationName);
            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.1f)
            {
                Debug.Log("<color=yellow>  连击22222222222222222</color>");
                OnLAttack();
                return;
            }

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

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.1f)
            {
                Debug.Log("<color=green>  轻连击1111 回 重·1111111</color>");
                OnRAttack();
                return;
            }
        }
        #endregion


    }
}
