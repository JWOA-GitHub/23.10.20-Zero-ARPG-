
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData dashData;
        /// <summary> 输入"上一个"冲刺的开始时间
        /// <see cref="startTime"/>
        /// </summary>
        private float startTime;
        /// <summary> 已连续冲刺的次数
        /// <see cref="consecutiveDashesUsed"/>
        /// </summary>
        private int consecutiveDashesUsed;

        /// <summary>判断进入该状态时是否在移动（即快速按下移动后按下冲刺，尚未旋转到对应方向则继续旋转！！）
        /// <see cref="shouldKeepRotating"/>
        /// </summary>
        private bool shouldKeepRotating;

        public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            dashData = movementData.DashData;
        }

        #region IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModeifier;

            base.Enter();

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

            // 默认旋转时间为0,14s，在Jump和Dash中旋转只需0.02s
            stateMachine.ReusableData.RotationData = dashData.RotationData;

            AddForceOnTransitionFromStationaryState();

            // 判断进入该状态时是否在移动（若尚未旋转到对应方向则继续旋转！！）
            shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

            UpdateConsecutiveDashes();

            startTime = Time.time;
        }
        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!shouldKeepRotating)
            {
                return;
            }

            // 仅在移动时更新转向！！
            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitionEvent()
        {
            // base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                // TODO： 动画某一帧触发！ 冲刺后若没有按键输入则会进入硬停止状态
                // stateMachine.ChangeState(stateMachine.IdingState);
                stateMachine.ChangeState(stateMachine.HardStoppingState);

                return;
            }
            stateMachine.ChangeState(stateMachine.SprintingState);
        }

        #endregion

        #region Main Methods
        /// <summary> 对于停止状态的处理：增加从静止状态过渡的力
        /// </summary>
        private void AddForceOnTransitionFromStationaryState()
        {
            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            // 力：添加到玩家面向的方向乘以移动速度

            // 获取水平旋转方向
            Vector3 characterRotationDirection = stateMachine.Player.transform.forward;

            // MARKER： 防止在冲刺过程中移动！
            characterRotationDirection.y = 0f;

            // MARKER： 更新旋转方向！！！（在“移动”旋转未完成时 开始冲刺后没有输入“移动”的情况！！ 需要调整旋转方向 否则不会自动完成旋转
            UpdateTargetRotation(characterRotationDirection, false);

            // TODO：character
            stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
        }

        /// <summary> 更新连续冲刺次数consecutiveDashesUsed  
        /// </summary> 
        private void UpdateConsecutiveDashes()
        {
            if (!IsConsecutive())
            {
                consecutiveDashesUsed = 0;
            }

            ++consecutiveDashesUsed;

            if (consecutiveDashesUsed == dashData.ConsecutiveDashesLimitAmount)
            {
                // 重置已使用的冲刺参数
                consecutiveDashesUsed = 0;

                // 禁用按键 冲刺
                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, dashData.DashLimitReachedCooldown);
            }

        }


        /// <summary> 判断本次冲刺是否为连续冲刺（两次冲刺间隔时间小于dashData.TimeToBeConsideredConsecutive 则属于连续冲刺）
        /// </summary>
        /// <returns>返回是否连续冲刺的结果</returns>
        private bool IsConsecutive()
        {
            return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
        }
        #endregion

        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
        }
        #endregion

        #region Input Methods
        // protected override void OnMovementCanceled(InputAction.CallbackContext context)
        // {
        //     // 留空，这样如果按下并释放移动输入键，则不会进入“待机状态”
        // }

        /// <summary> 解决在冲刺中间按下并松开“移动”键读取是否完成旋转！冲刺结束后自动往对应方向完成旋转
        /// </summary>
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            shouldKeepRotating = true;
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {

        }
        #endregion

    }
}
