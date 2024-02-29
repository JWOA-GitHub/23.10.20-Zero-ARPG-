using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerBaseGroundedState : PlayerMovementState
    {
        private SlopeData slopeData;
        public PlayerBaseGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            slopeData = stateMachine.Player.ColliderUtility.SlopeData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            // StartAnimation(animationData.GroundedParameterHash);

            stateMachine.ReusableData.isComboing = false;

            UpdateShouldSprintState();

            // MARKER: 更改状态时，不会更新相机水平居中重新定位时间，因此每次切换状态都更新！ 计算基础移动速度时，还需计算当前状态的速度修改器！！ 因此需要将每个状态更新的速度修改器  在 base.Enter前调用！！！
            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        public override void Exit()
        {
            base.Exit();

            // StopAnimation(animationData.GroundedParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Float();
        }
        #endregion

        #region  Main Methods
        /// <summary> 若ShouldSprint属性为true且没有按下movement，则将shouldSprint设为flase！；判断接地状态是否继续“疾跑”！！ 更新ShouldSprint
        /// </summary>
        private void UpdateShouldSprintState()
        {
            if (!stateMachine.ReusableData.ShouldSprint)
            {
                return;
            }

            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            stateMachine.ReusableData.ShouldSprint = false;
        }


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

            // MARKER： “进入”和“退出”斜坡，需要更新速度修改器，即地面角度更新时，需要确保将相机 水平居中“重新定位时间” 更新为新的玩家速度！！
            if (stateMachine.ReusableData.MovementSpeedModifier != slopeSpeedModifier)
            {
                stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

                UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
            }

            stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }

        // MARKER: 判断脚底碰撞器是否接近地面
        /// <summary> 判断脚底碰撞器是否接近地面
        /// </summary>
        /// <returns>脚底碰撞器接触地面layer的数量overlappedGroundColliders.Length > 0</returns>
        private bool IsThereGroundUnderneath()
        {
            BoxCollider groundCheckCollider = stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckCollider;
            Vector3 groundColliderCenterInWorldSpace = groundCheckCollider.bounds.center;

            Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckColliderExtents, groundCheckCollider.transform.rotation, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

            return overlappedGroundColliders.Length > 0;
        }

        #endregion

        #region Reusable Methods 可复用方法
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            // stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;

            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;

            stateMachine.Player.Input.PlayerActions.LAttack.started += OnLAttackComboStarted;
            stateMachine.Player.Input.PlayerActions.RAttack.started += OnRAttackComboStarted;

            // 技能按键
            stateMachine.Player.Input.PlayerActions.SkillAttack1.started += OnSkillAttack1Started;
            stateMachine.Player.Input.PlayerActions.SkillAttack2.started += OnSkillAttack2Started;
            stateMachine.Player.Input.PlayerActions.SkillAttack3.started += OnSkillAttack3Started;
            stateMachine.Player.Input.PlayerActions.SkillAttack4.started += OnSkillAttack4Started;


            // 每个接地状态都可切换到跳跃状态！！SkillAttack1
            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            // stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;

            stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;

            stateMachine.Player.Input.PlayerActions.LAttack.started -= OnLAttackComboStarted;
            stateMachine.Player.Input.PlayerActions.RAttack.started -= OnRAttackComboStarted;

            stateMachine.Player.Input.PlayerActions.SkillAttack1.started -= OnSkillAttack1Started;
            stateMachine.Player.Input.PlayerActions.SkillAttack2.started -= OnSkillAttack2Started;
            stateMachine.Player.Input.PlayerActions.SkillAttack3.started -= OnSkillAttack3Started;
            stateMachine.Player.Input.PlayerActions.SkillAttack4.started -= OnSkillAttack4Started;

            stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
        }

        /// <summary> 判断是否切换到左键攻击状态，默认切换到combo1
        /// </summary>
        protected virtual void OnLAttack()
        {

        }

        /// <summary> 判断是否切换到右键攻击状态，默认切换到combo1
        /// </summary>
        protected virtual void OnRAttack()
        {

        }

        protected virtual void OnMove()
        {
            // MARKER：判断如“疾跑”状态“跳跃”后是否继续“疾跑”
            if (stateMachine.ReusableData.ShouldSprint)
            {
                stateMachine.ChangeState(stateMachine.SprintingState);

                return;
            }

            // 判断 Walk Toggle On 则过渡到“步行状态”，否则，过渡到“跑步”状态
            if (stateMachine.ReusableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }

        protected override void OnContactWithGroundExited(Collider collider)
        {
            base.OnContactWithGroundExited(collider);

            // 即判断脚底碰撞器是否已离开地面！
            if (IsThereGroundUnderneath())
            {
                return;
            }

            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            // 射线检测距离：获取玩家碰撞体底部！（碰撞体中心-碰撞体y轴一半的聚财）
            Ray downwardsRayFromCapusuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);

            if (!Physics.Raycast(downwardsRayFromCapusuleBottom, out _, movementData.GroundToFallRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore)) // 忽略“地面触发碰撞器”效果
            {
                OnFall();
            }
        }

        /// <summary> 切换到坠落状态！
        /// </summary>
        protected virtual void OnFall()
        {
            stateMachine.ChangeState(stateMachine.FallingState);
        }
        #endregion

        #region  Input Methods
        // /// <summary> 当前状态 停止移动！即松开移动按键时 触发的事！base默认为切换到Idle状态！
        // /// </summary>
        // protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        // {
        //     stateMachine.ChangeState(stateMachine.IdingState);
        // }

        /// <summary> 进入冲刺状态
        /// </summary>
        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashingState);
        }


        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.JumpingState);
        }

        protected virtual void OnLAttackComboStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.isComboing)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.NormalAttacking_01_1_State);
        }

        protected virtual void OnRAttackComboStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.isComboing)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.NormalAttacking_02_1_State);
        }

        protected virtual void OnSkillAttack1Started(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.isSkilling)
            {
                return;
            }

            // //技能准备（判断条件 能量 cd等）
            // SkillData data = stateMachine.Player.SkillManager.PrepareSkill(1001);

            // if (data != null)
            //     stateMachine.Player.SkillManager.GenerateSkill(data);

            // TODO: 封装为技能系统
            //stateMachine.Player.SkillSystem.AttackUseSkill(1001);
            // 进入技能状态
            stateMachine.ChangeState(stateMachine.AttackSkills_01_State);
        }

        protected virtual void OnSkillAttack2Started(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.isSkilling)
            {
                return;
            }

            // TODO: 封装为技能系统
            stateMachine.Player.SkillSystem.AttackUseSkill(1002);
            // 进入技能状态
            stateMachine.ChangeState(stateMachine.AttackSkills_02_State);
        }

        protected virtual void OnSkillAttack3Started(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.isSkilling)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.AttackSkills_03_State);
        }

        protected virtual void OnSkillAttack4Started(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.isSkilling)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.AttackSkills_04_State);
        }
        #endregion
    }
}
