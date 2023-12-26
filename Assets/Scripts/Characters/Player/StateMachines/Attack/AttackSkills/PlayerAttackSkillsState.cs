using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackSkillsState : PlayerAttackState
    {
        // [SerializeField] protected string stateName = "combo_01_1";
        // protected bool isLightComboCache = false;
        // protected bool isHeavyComboCache = false;
        public PlayerAttackSkillsState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // // 获取当前正在播放的动画状态信息
            // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.AttackSkillsParameterHash);

            // StartAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            // // StopAnimation(stateMachine.Player.AnimationData.NormalAttack_1_ParameterHash);

            // // stateMachine.Player.Animator.Play(stateName);
            // // Debug.Log(stateName);

            // stateMachine.ReusableData.MovementSpeedModifier = 0f;

            // stateMachine.ReusableData.isComboing = true;

            // ResetVelocity();

            // ResetCombo();
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AttackSkillsParameterHash);

            // StopAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            // ResetCombo();
        }

        // public override void PhysicsUpdate()
        // {
        //     base.PhysicsUpdate();

        //     // 当进入“停止”状态时，即使没有按下移动键，也会完成自动旋转！！
        //     // RotateTowardsTargetRotation();
        // }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();

        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            // if (isLightComboCache || isHeavyComboCache)
            //     return;
        }
        #endregion

        #region Reusable Methods
        // protected void ResetCombo()
        // {   
        //     stateMachine.ReusableData.ShouldLightCombo = false;
        //     stateMachine.ReusableData.ShouldHeavyCombo = false;

        //     isLightComboCache = false;
        //     isHeavyComboCache = false;
        // }

        // protected override void AddInputActionsCallbacks()
        // {
        //     base.AddInputActionsCallbacks();

        //     stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        // }

        // protected override void RemoveInputActionsCallbacks()
        // {
        //     base.RemoveInputActionsCallbacks();

        //     stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        // }
        // #endregion

        // #region Input Methods
        // // protected override void OnAttackComboStarted(InputAction.CallbackContext context)
        // // {
        // //     base.OnAttackComboStarted(context);

        // //     // stateMachine.ReusableData.ShouldLightCombo = true;
        // //     // OnAttack();
        // // }
        // private void OnMovementStarted(InputAction.CallbackContext context)
        // {
        //     // 负责切换到移动状态
        //     OnMove();
        // }

        // protected override void OnLAttackComboStarted(InputAction.CallbackContext context)
        // {
        //     stateMachine.ReusableData.ShouldLightCombo = true;

        //     isLightComboCache = true;

        // }

        // protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        // {
        //     stateMachine.ReusableData.ShouldHeavyCombo = true;

        //     isHeavyComboCache = true;
        // }
        #endregion
    }
}
