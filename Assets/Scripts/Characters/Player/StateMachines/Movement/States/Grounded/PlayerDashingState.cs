
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

        public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            dashData = movementData.DashData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModeifier;

            AddForceOnTransitionFromStationaryState();

            UpdateConsecutiveDashes();

            startTime = Time.time;
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdingState);

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

            stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
        }

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

        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            // 留空，这样如果按下并释放移动输入键，则不会进入“待机状态”
        }
        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            
        }
        #endregion

    }
}
