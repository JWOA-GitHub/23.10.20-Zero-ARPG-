using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private SlopeData slopeData;
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            slopeData = stateMachine.Player.ColliderUtility.SlopeData;
        }

        #region IState Methods
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Float();
        }
        #endregion

        #region  Main Methods
        /// <summary> 浮动胶囊，漂浮碰撞体
        /// </summary>
        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                // 若从碰撞体中心到地面的距离足以被Raycast检测，会快速到达浮动点！！ 因此增加“坠落”
                // 当在阶梯时，hit.normal为垂直于平面指向斜上方的方向, 中心检测方向为向下，因此+负号
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                // 获取斜坡移动速度调节器（根据所在斜坡角度 倾斜越大，速度越慢  防止漂浮在角度太高的地面上，使得无法行走 而是会跌落或滑动
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

                if (slopeSpeedModifier == 0f)
                {
                    return;
                }

                //若缩放游戏玩家该碰撞体 将不会漂浮在对应位置，（通过碰撞中心y * 玩家y解决）   若比例过大，距离将超过“2”，射线将找不到东西，玩家将进入“地面”（通过增加距离值解决：从射线命中中减去距离，即-hit.distance

                // 获取碰撞体中心到地面的距离
                float distanceToFloatingPoint = stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y
                            * stateMachine.Player.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                {
                    return;
                }

                //将该值与之前的额外力相乘，并删除当前的垂直速度
                // 计算出碰撞体中心点到地面 浮动的力度！！！！
                float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

                // TODO：character
                stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);    // 即时的速度变化，忽略质量
            }
        }

        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            float slopeSpeedModifier = movementData.SlopeSpeedAngles.Evaluate(angle);

            stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }
        #endregion

        #region Reusable Methods 可复用方法
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;

            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;

            // 每个接地状态都可切换到跳跃状态！！
            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;

            stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;

            stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
        }

        protected virtual void OnMove()
        {
            // 判断 Walk Toggle On 则过渡到“步行状态”，否则，过渡到“跑步”状态
            if (stateMachine.ReusableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }
        #endregion

        #region  Input Methods
        /// <summary> 当前状态 停止移动！即松开移动按键时 触发的事！base默认为切换到Idle状态！
        /// </summary>
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdingState);
        }

        /// <summary> 进入冲刺状态
        /// </summary>
        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashingState);
        }


        protected void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.JumpingState);
        }
        #endregion
    }
}
