
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttacking_02_1_State : PlayerAttackComboState
    {
        public PlayerNormalAttacking_02_1_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            // Attack中获取当前正在播放的动画状态信息 设置当前状态对应AnimationName
            stateName = stateMachine.Player.AnimationData.NormalAttack_AnimationData.NormalAttack_02_1_StateName;

            StartAnimation(stateMachine.Player.AnimationData.NormalAttack_02_1_ParameterHash);

            stateMachine.Player.Input.PlayerActions.Movement.Disable();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.NormalAttack_02_1_ParameterHash);

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
            // TODO：在非攻击状态 设置
            stateMachine.ReusableData.isComboing = false;
            stateMachine.ChangeState(stateMachine.IdingState);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            stateMachine.ChangeState(stateMachine.IdingState);
        }
        #endregion

        #region Reusable Methods
        protected override void OnLAttack()
        {
            stateMachine.ChangeState(stateMachine.NormalAttacking_01_1_State);
        }

        protected override void OnRAttack()
        {
            stateMachine.ChangeState(stateMachine.NormalAttacking_02_2_State);
        }
        #endregion

        #region Input Methods
        protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnLAttackComboStarted(context);

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.1f)
            {
                Debug.Log("<color=green>  重连击11111回 ·1111111</color>");
                OnLAttack();
                return;
            }
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.3f)
            {
                Debug.Log("<color=green>  重连击1111111 连·重2222222222</color>");
                OnRAttack();
                return;
            }
        }
        #endregion


    }
}
