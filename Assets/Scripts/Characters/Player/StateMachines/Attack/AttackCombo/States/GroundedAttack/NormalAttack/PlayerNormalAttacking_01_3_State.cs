using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_01_3_State : PlayerAttackComboState
    {
        public PlayerNormalAttacking_01_3_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // // 获取当前正在播放的动画状态信息
            // animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            // Attack中获取当前正在播放的动画状态信息 设置当前状态对应AnimationName
            stateName = animationData.NormalAttack_AnimationData.NormalAttack_01_3_StateName;

            SetAnimationMoveBase(stateMachine.Player.transform.forward, 1.5f);

            // stateMachine.Player.effectManager.SpawnEffect("Combo3", stateMachine.Player.effectManager.effects[2].prefab.transform);


            StartAnimation(animationData.NormalAttack_01_3_ParameterHash);

        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.NormalAttack_01_3_ParameterHash);

            // stateMachine.Player.Input.PlayerActions.LAttack.Enable();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (isEffecting && animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.1f && animationData.animatorStateInfo.normalizedTime <= 0.15f)
            {
                SoundManger.Instance.PlayAudio(Globals.S_Combo01_3);
                stateMachine.Player.effectManager.SpawnEffect("Combo3", stateMachine.Player.effectManager.effects[2].prefab.transform.position, stateMachine.Player.effectManager.effects[2].prefab.transform.rotation);
                Debug.Log(33333333333333);
                isEffecting = false;
            }

            if (animationData.animatorStateInfo.normalizedTime >= 0.15f && animationData.animatorStateInfo.normalizedTime < 0.16f)
            {
                isEffecting = true;
            }

            if (isEffecting && animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.15f)
            {
                Debug.Log(3333333333333222);
                SoundManger.Instance.PlayAudio(Globals.S_Combo01_4);
                stateMachine.Player.effectManager.SpawnEffect("Combo4", stateMachine.Player.effectManager.effects[3].prefab.transform.position, stateMachine.Player.effectManager.effects[3].prefab.transform.rotation);
                isEffecting = false;
            }
        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.Player.Input.PlayerActions.LAttack.Enable();
            // Debug.Log("<color= #ffff1111> trans前   </color>" + stateMachine.ReusableData.ShouldLightCombo);
            // base.OnAnimationTransitionEvent();
            if (stateMachine.ReusableData.ShouldLightCombo)
            {
                // Debug.Log("<color= #ffff1111>  轻击   </color>");
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
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // if (stateMachine.ReusableData.ShouldLightCombo)
            // {
            // stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
            return;
            //     stateMachine.ChangeState(stateMachine.NormalAttacking_1_State);
            // }
        }
        #endregion

        #region Reusable Methods
        protected override void OnLAttack()
        {
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

            if (animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.3f)
            {
                Debug.Log("<color=yellow>  连击444444</color>");
                OnLAttack();
                return;
            }
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);

            if (animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.3f)
            {
                Debug.Log("<color=green>  轻连击333333333 回 重·1111111</color>");
                OnRAttack();
                return;
            }
        }
        #endregion


    }
}
