using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackSkills_04_State : PlayerAttackSkillsState
    {
        public PlayerAttackSkills_04_State(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // // 获取当前正在播放的动画状态信息
            // stateMachine.Player.AnimationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            // 设置技能下标 PlayerAttackSkillsState使用技能！
            currentSkillsIndex = 3;

            base.Enter();

            stateName = stateMachine.Player.AnimationData.SkillsAttack_AnimationData.SkillsAttack_04_StateName;

            StartAnimation(stateMachine.Player.AnimationData.SkillsAttack_04_ParameterHash);

            stateMachine.Player.skills[currentSkillsIndex].UseSkill(stateMachine.Player);

        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.SkillsAttack_04_ParameterHash);

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

        #region  Methods
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
