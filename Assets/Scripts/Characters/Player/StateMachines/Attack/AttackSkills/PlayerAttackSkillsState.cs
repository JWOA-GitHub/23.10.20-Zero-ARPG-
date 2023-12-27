using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackSkillsState : PlayerAttackState
    {
        // [SerializeField] protected string stateName = "combo_01_1";
        // protected bool isLightComboCache = false;
        // protected bool isHeavyComboCache = false;
        protected int currentSkillsIndex = 0;
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

            stateMachine.Player.skills[currentSkillsIndex].UseSkill(stateMachine.Player);

            stateMachine.ReusableData.isSkilling = true;
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AttackSkillsParameterHash);

            stateMachine.ReusableData.isSkilling = false;

            stateMachine.ChangeState(stateMachine.IdingState);
            // StopAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            // ResetCombo();
        }

        // public override void PhysicsUpdate()
        // {
        //     base.PhysicsUpdate();

        //     // 当进入“停止”状态时，即使没有按下移动键，也会完成自动旋转！！
        //     // RotateTowardsTargetRotation();
        // }

        // public override void OnAnimationEnterEvent()
        // {
        //     base.OnAnimationEnterEvent();

        // }

        // public override void OnAnimationExitEvent()
        // {
        //     base.OnAnimationExitEvent();

        //     // if (isLightComboCache || isHeavyComboCache)
        //     //     return;
        // }
        #endregion

        #region Reusable Methods


        // protected void ResetCombo()
        // {   
        //     stateMachine.ReusableData.ShouldLightCombo = false;
        //     stateMachine.ReusableData.ShouldHeavyCombo = false;

        //     isLightComboCache = false;
        //     isHeavyComboCache = false;
        // }

        // protected override void AddActionsCallbacks()
        // {
        //     base.AddActionsCallbacks();

        //     stateMachine.Player..PlayerActions.Movement.started += OnMovementStarted;
        // }

        // protected override void RemoveActionsCallbacks()
        // {
        //     base.RemoveActionsCallbacks();

        //     stateMachine.Player..PlayerActions.Movement.started -= OnMovementStarted;
        // }
        // #endregion

        // #region  Methods
        // // protected override void OnAttackComboStarted(Action.CallbackContext context)
        // // {
        // //     base.OnAttackComboStarted(context);

        // //     // stateMachine.ReusableData.ShouldLightCombo = true;
        // //     // OnAttack();
        // // }
        // private void OnMovementStarted(Action.CallbackContext context)
        // {
        //     // 负责切换到移动状态
        //     OnMove();
        // }

        // protected override void OnLAttackComboStarted(Action.CallbackContext context)
        // {
        //     stateMachine.ReusableData.ShouldLightCombo = true;

        //     isLightComboCache = true;

        // }

        // protected override void OnRAttackComboStarted(Action.CallbackContext context)
        // {
        //     stateMachine.ReusableData.ShouldHeavyCombo = true;

        //     isHeavyComboCache = true;
        // }
        #endregion
    }
}
