using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerAttackComboState : PlayerAttackState
    {
        // [SerializeField] protected string stateName = "combo_01_1";
        // protected bool isLightComboCache = false;
        // protected bool isHeavyComboCache = false;
        public PlayerAttackComboState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            // // 获取当前正在播放的动画状态信息
            // animationData.animatorStateInfo = stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0);

            base.Enter();

            StartAnimation(animationData.AttackComboParameterHash);

            stateMachine.ReusableData.isComboing = true;

            // StartAnimation(animationData.AttackComboParameterHash);

            // // StopAnimation(animationData.NormalAttack_1_ParameterHash);

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

            StopAnimation(animationData.AttackComboParameterHash);

            stateMachine.ReusableData.isComboing = false;
            // StopAnimation(animationData.AttackComboParameterHash);

            // ResetCombo();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // Debug.Log($"<color=red>   ID:   {stateMachine.Player.Animator.GetFloat(animationData.animationMoveID)}</color>");
            // stateMachine.Player.CharacterMoveInterface(stateMachine.Player.transform.forward, stateMachine.Player.Animator.GetFloat(animationData.animationMoveID));

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
            string scriptName = GetType().Name;

            // Debug.Log("<color=yellow>  Exit  return前        Script name: " + scriptName + "</color>");

            if (stateMachine.ReusableData.isLightComboCache || stateMachine.ReusableData.isHeavyComboCache)
                return;

            // Debug.Log("<color=yellow>  Exit  Return   后         Script name: " + scriptName + "</color>");
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
