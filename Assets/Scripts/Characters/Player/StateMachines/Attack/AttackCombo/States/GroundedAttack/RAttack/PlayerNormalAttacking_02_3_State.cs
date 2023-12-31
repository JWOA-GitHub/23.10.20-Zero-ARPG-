
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_02_3_State : PlayerAttackComboState
    {
        public PlayerNormalAttacking_02_3_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            // Attack中获取当前正在播放的动画状态信息 设置当前状态对应AnimationName
            stateName = animationData.NormalAttack_AnimationData.NormalAttack_02_3_StateName;

            SetAnimationMoveBase(stateMachine.Player.transform.forward, 2);

            StartAnimation(animationData.NormalAttack_02_3_ParameterHash);

            stateMachine.Player.Input.PlayerActions.Movement.Disable();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.NormalAttack_02_3_ParameterHash);

            stateMachine.Player.Input.PlayerActions.Movement.Enable();

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

        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            // TODO：在非攻击状态 设置
            stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
            return;
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

            if (animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.4f)
            {
                Debug.Log("<color=green>  重连击33333333回 ·1111111</color>");
                OnLAttack();
                return;
            }
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);

            if (animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.5f)
            {
                Debug.Log("<color=green>  重连击33333 回·重1111111</color>");
                OnRAttack();
                return;
            }
        }
        #endregion


    }
}
