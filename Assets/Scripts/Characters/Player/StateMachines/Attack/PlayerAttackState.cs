using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackState : PlayerBaseGroundedState
    {
        [SerializeField] protected string stateName = "combo_01_1";

        public PlayerAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // 获取当前正在播放的动画状态信息
            stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier = 0f;


            ResetVelocity();

            ResetCombo();
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

            ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // 当进入“停止”状态时，即使没有按下移动键，也会完成自动旋转！！
            RotateTowardsTargetRotation();
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();

        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            if (stateMachine.ReusableData.isLightComboCache || stateMachine.ReusableData.isHeavyComboCache)
                return;
        }
        #endregion

        #region Reusable Methods
        protected void ResetCombo()
        {
            stateMachine.ReusableData.ShouldLightCombo = false;
            stateMachine.ReusableData.ShouldHeavyCombo = false;

            stateMachine.ReusableData.isLightComboCache = false;
            stateMachine.ReusableData.isHeavyComboCache = false;
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        }
        #endregion

        #region Input Methods
        // protected override void OnAttackComboStarted(InputAction.CallbackContext context)
        // {
        //     base.OnAttackComboStarted(context);

        //     // stateMachine.ReusableData.ShouldLightCombo = true;
        //     // OnAttack();
        // }
        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            // 负责切换到移动状态
            OnMove();
        }

        protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldLightCombo = true;

            stateMachine.ReusableData.isLightComboCache = true;
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldHeavyCombo = true;

            stateMachine.ReusableData.isHeavyComboCache = true;
        }

        protected override void OnSkillAttack1Started(InputAction.CallbackContext context)
        {
            base.OnSkillAttack1Started(context);
        }

        protected override void OnSkillAttack2Started(InputAction.CallbackContext context)
        {
            base.OnSkillAttack2Started(context);
        }

        protected override void OnSkillAttack3Started(InputAction.CallbackContext context)
        {
            base.OnSkillAttack3Started(context);
        }

        protected override void OnSkillAttack4Started(InputAction.CallbackContext context)
        {
            base.OnSkillAttack4Started(context);
        }
        #endregion
    }
}
