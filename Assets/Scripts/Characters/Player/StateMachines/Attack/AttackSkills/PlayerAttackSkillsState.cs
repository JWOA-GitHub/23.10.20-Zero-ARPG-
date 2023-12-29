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

            if (!stateMachine.Player.skills[currentSkillsIndex].UseSkill(stateMachine.Player))
            {
                // stateMachine.ChangeState(stateMachine.IdingState);
                return;
            }

            StartAnimation(stateMachine.Player.AnimationData.AttackSkillsParameterHash);

            stateMachine.ReusableData.isSkilling = true;
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.AttackSkillsParameterHash);

            stateMachine.ReusableData.isSkilling = false;

            // StopAnimation(stateMachine.Player.AnimationData.AttackComboParameterHash);

            // ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // 当进入“停止”状态时，即使没有按下移动键，也会完成自动旋转！！
            // RotateTowardsTargetRotation();
        }

        // public override void OnAnimationEnterEvent()
        // {
        //     base.OnAnimationEnterEvent();

        // }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();


        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            // 在没有攻击和技能cache的时候 默认返回idle
            stateMachine.ChangeState(stateMachine.IdingState);
            return;
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

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.7f)
            {
                Debug.Log("<color=yellow>  技能 连击回 1111</color>");
                OnLAttack();
                return;
            }
        }

        protected override void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            base.OnRAttackComboStarted(context);

            if (stateMachine.Player.AnimationData.animatorStateInfo.IsName(stateName) && stateMachine.Player.AnimationData.animatorStateInfo.normalizedTime >= 0.7f)
            {
                Debug.Log("<color=green>  技能 连击4444444 回 重·1111111</color>");
                OnRAttack();
                return;
            }
        }
        #endregion
    }
}
