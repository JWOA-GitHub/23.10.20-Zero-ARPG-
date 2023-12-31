using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackSkills_02_State : PlayerAttackSkillsState
    {
        public PlayerAttackSkills_02_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // // 获取当前正在播放的动画状态信息
            // animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            // 设置技能下标 PlayerAttackSkillsState使用技能！
            currentSkillsIndex = 1;

            base.Enter();

            stateName = animationData.SkillsAttack_AnimationData.SkillsAttack_02_StateName;

            StartAnimation(animationData.SkillsAttack_02_ParameterHash);

            // stateMachine.Player.skills[currentSkillsIndex].UseSkill(stateMachine.Player);
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.SkillsAttack_02_ParameterHash);
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
