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
            // animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            // 设置技能下标 PlayerAttackSkillsState使用技能！
            currentSkillsIndex = 3;

            base.Enter();

            stateName = animationData.SkillsAttack_AnimationData.SkillsAttack_04_StateName;

            SetAnimationMoveBase(stateMachine.Player.transform.forward, 4);

            StartAnimation(animationData.SkillsAttack_04_ParameterHash);

            // stateMachine.Player.effectManager.SpawnEffect("Skill4", stateMachine.Player.effectManager.effects[8].prefab.transform.position, stateMachine.Player.effectManager.effects[8].prefab.transform.rotation);

            // stateMachine.Player.skills[currentSkillsIndex].UseSkill(stateMachine.Player);

            // 最后一击的僵直  无法移动,无法攻击！
            stateMachine.Player.Input.PlayerActions.Movement.Disable();
            stateMachine.Player.Input.PlayerActions.LAttack.Disable();
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.SkillsAttack_04_ParameterHash);

            stateMachine.Player.Input.PlayerActions.Movement.Enable();
            stateMachine.Player.Input.PlayerActions.LAttack.Enable();

            // StopAnimation(animationData.AttackComboParameterHash);

            // ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // 当进入“停止”状态时，即使没有按下移动键，也会完成自动旋转！！
            // RotateTowardsTargetRotation();

            if (isEffecting && animationData.animatorStateInfo.IsName(stateName) && animationData.animatorStateInfo.normalizedTime >= 0.35f)
            {
                // AudioManager.Instance.Play("NormalAttack1");
                SoundManger.Instance.PlayAudio(Globals.S_Skill1);
                stateMachine.Player.effectManager.SpawnEffect("Skill4", stateMachine.Player.effectManager.effects[8].prefab.transform.position, stateMachine.Player.effectManager.effects[8].prefab.transform.rotation);

                isEffecting = false;
            }
        }

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
