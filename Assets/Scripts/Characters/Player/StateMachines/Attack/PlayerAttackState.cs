using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackState : PlayerBaseGroundedState
    {
        [SerializeField] protected string stateName = "combo_01_1";
        protected bool isEffecting = false;
        public PlayerAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // 获取当前正在播放的动画状态信息
            animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            StartAnimation(animationData.AttackParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVelocity();

            ResetCombo();

            SetAnimationMoveBase(Vector3.zero, 1);
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.AttackParameterHash);

            ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // 动画中的Curve曲线 决定动画的移动位置！
            stateMachine.Player.CharacterMoveInterface(animationData.animationMoveDir, stateMachine.Player.Animator.GetFloat(animationData.animationMoveID) * animationData.animationMoveSpeedModifier);

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

            if (stateMachine.ReusableData.isLightComboCache || stateMachine.ReusableData.isHeavyComboCache || stateMachine.ReusableData.isSkillingCache)
                return;
        }
        #endregion

        #region Reusable Methods

        /// <summary> 开启攻击检测
        /// </summary>
        public void EnableDetection()
        {

        }

        /// <summary> 关闭攻击检测
        /// </summary>
        public void DisableDetection()
        {

        }

        protected void SetAnimationMoveBase(Vector3 moveDir, float moveSpeedModeifier)
        {
            animationData.animationMoveDir = moveDir;
            animationData.animationMoveSpeedModifier = moveSpeedModeifier;
        }

        protected void ResetCombo()
        {
            stateMachine.ReusableData.ShouldLightCombo = false;
            stateMachine.ReusableData.ShouldHeavyCombo = false;

            stateMachine.ReusableData.isLightComboCache = false;
            stateMachine.ReusableData.isHeavyComboCache = false;

            isEffecting = true;
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
