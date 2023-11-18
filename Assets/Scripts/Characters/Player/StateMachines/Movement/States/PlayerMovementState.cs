using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerMovementState : IState
    {
        [Tooltip("移动输入状态机：空闲、步行、跑步、冲刺")]
        protected PlayerMovementStateMachine stateMachine;

        protected PlayerGroundedData movementData;
        protected PlayerAirborneData airborneData;

        // // /// <summary> Vector2 按键输入！！！
        // // /// <see cref="movementInput"/>
        // // /// </summary>
        // // [Tooltip("移动按键输入")] protected Vector2 movementInput;
        // [Tooltip("基础速度")] protected float baseSpeed = 5f;
        // [Tooltip("速度调整器(倍率)")] protected float speedModeifier = 1f;

        // MARKER： 创建Vector3 是因为在后面的“滑动系统”中，需要“x”和“z”轴的值！！！！
        // /// <summary>当前旋转目标角度
        // /// <see cref="currentTargetRotation"/>
        // /// </summary>
        // [Tooltip("当前旋转目标角度")] protected Vector3 currentTargetRotation;

        // /// <summary>达到目标角度所需的时间
        // /// <see cref="timeToReachTargetRotation"/>
        // /// </summary>
        // [Tooltip("达到目标角度所需的时间")] protected Vector3 timeToReachTargetRotation;

        // /// <summary>平滑玩家旋转角度的速度
        // /// <see cref="dampedTargetRotationCurrentVelocity"/>
        // /// </summary>
        // [Tooltip("平滑玩家旋转角度的速度")] protected Vector3 dampedTargetRotationCurrentVelocity;

        // /// <summary>阻尼目标旋转经过时间
        // /// <see cref="dampedTargetRotationPassedTime"/>
        // /// </summary>
        // [Tooltip("阻尼目标旋转经过时间")] protected Vector3 dampedTargetRotationPassedTime;


        // [Tooltip("判断移动是否开启步行，不开启则为跑步状态")] protected bool shouldWalk;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            movementData = stateMachine.Player.Data.GroundedData;
            airborneData = stateMachine.Player.Data.AirborneData;

            SetBaseCameraRecenteringData();

            InitializeData();
        }


        private void InitializeData()
        {
            // 初始化到达目标旋转角度的时间
            // stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
            // MARKER: 设置为方法，因为需要在 退出“冲刺”状态时重新设置旋转数据！（冲刺状态的旋转时间与基本的不同！！
            SetBaseRotationData();
        }

        #region IState Mathods
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);

            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }


        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            Move();

            // 传参movementInput为Vector2！！ 旋转已加在Move中
            // Rotate(movementInput);
        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {

        }

        public virtual void OnAnimationTransitionEvent()
        {

        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);

                return;
            }
        }

        public virtual void OnTriggerExit(Collider collider)
        {
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExited(collider);

                return;
            }
        }

        #endregion


        #region Main Methods
        private void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f)
            {
                return;
            }

            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            // MARKER: 刚体移动（需减去当前已有速度，否则持续按下会越来越快！
            // stateMachine.Player.Rigidbody.AddForce(movementSpeed * movementDirection - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
            // TODO：character
            stateMachine.Player.Rigidbody.AddForce(movementSpeed * targetRotationDirection - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }


        // MARKER: 获取的玩家输入MovementInput 为Vector2 ！！！！！
        /// <summary> 获取玩家相对于摄像机的旋转角度，即更改为相对于摄像机的相对移动方向！
        /// </summary>
        /// <param name="direction">移动按键的输入方向</param>
        /// <returns></returns>
        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            // Debug.Log("目标角度 " + directionAngle);
            RotateTowardsTargetRotation();

            // float directionAngle = direction.y;

            return directionAngle;
        }



        /// <summary> 更新当前目标旋转角度
        /// </summary>
        /// <param name="targetAngle">目标角度</param>
        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

            // 重置旋转到目标所需的时间
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        /// <summary> 获取移动输入方向的角度（获取的输入为Vector2！！！！
        /// </summary>
        /// <param name="direction">移动按键的输入方向</param>
        /// <returns></returns>
        private static float GetDirectionAngle(Vector3 direction)
        {
            // 更改为相对于z轴的角度 （z轴为↑  x轴为→）   Mathf.Atan2值域为（-180,180）
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // Debug.Log(directionAngle);

            if (directionAngle < 0)
            {
                directionAngle += 360f;
            }

            // Debug.Log("角度" + directionAngle + "  <0+360 " + (directionAngle < 0 ? directionAngle + 360 : directionAngle));
            return directionAngle;
        }

        /// <summary> 玩家移动增加摄像机旋转角度，即更改为相对于摄像机的移动方向
        /// </summary>
        /// <param name="angle">当前移动方向的角度</param>
        /// <returns></returns>
        private float AddCameraRotationToAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
            // Debug.Log(stateMachine.Player.MainCameraTransform.eulerAngles.y + "   " + stateMachine.Player.MainCameraTransform.eulerAngles.y % 360 + "  ang " + angle);

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }


        #endregion

        #region Reusable Methods 可复用方法
        /// <summary> 设置摄像机重新居中基本数据
        /// </summary>
        protected void SetBaseCameraRecenteringData()
        {
            stateMachine.ReusableData.BackwardsCameraRecenteringData = movementData.BackwardsCameraRecenteringData;
            stateMachine.ReusableData.SidewaysCameraRecenteringData = movementData.SidewaysCameraRecenteringData;
        }

        /// <summary> 设置复原基本旋转数据（旋转所需时间等，后面其他状态可能更改旋转时间等
        /// </summary>
        protected void SetBaseRotationData()
        {
            stateMachine.ReusableData.RotationData = movementData.BaseRotationData;

            // 初始化到达目标旋转角度的时间
            stateMachine.ReusableData.TimeToReachTargetRotation = stateMachine.ReusableData.RotationData.TargetRotationReachTime;
        }

        /// <summary>  增加按键绑定： 如："切换行走奔跑状态"、"移动取消输入"按键
        /// </summary>
        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkeToggle.started += OnWalkToggleStarted;

            stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;

            stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        }


        /// <summary> 移除按键绑定： 如："切换行走奔跑状态"、"移动取消输入"按键
        /// </summary>
        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkeToggle.started -= OnWalkToggleStarted;

            stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;

            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        }


        /// <summary> 获取移动输入方向
        /// </summary>
        /// <returns>获取移动输入</returns>
        protected Vector3 GetMovementInputDirection()
        {
            // Debug.Log(movementInput);
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0, stateMachine.ReusableData.MovementInput.y);
        }

        /// <summary> 获取玩家移动速度（基本速度*速度调整速率数）
        /// </summary>
        /// <returns>获取玩家移动速度</returns>
        protected float GetMovementSpeed()
        {
            return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier * stateMachine.ReusableData.MovementOnSlopesSpeedModifier;
        }

        /// <summary> 获取玩家垂直方向的力
        /// </summary>
        /// <returns>返回玩家垂直方向的力</returns>
        protected Vector3 GetPlayerVerticalVelocity()
        {
            // TODO：character
            return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
        }


        /// <summary> 获取玩家当前水平速度
        /// </summary>
        /// <returns>获取玩家当前水平速度</returns>
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            // TODO：character
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }


        /// <summary> 玩家向目标角度旋转
        /// </summary>
        protected void RotateTowardsTargetRotation()
        {
            // TODO：character
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            // Debug.Log(currentYAngle + "      wanjia ");
            if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDamp(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y,
            ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);

            // MARKER： 因此此方法是在FixedUpdate方法中调用的，所以使用 Time.deltaTime 变量时，unity会自动返回 fixedDeltaTime
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            // Debug.Log(targetRotation.eulerAngles);
            // TODO：character
            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        /// <summary> 更新目标旋转角度
        /// </summary>
        /// <param name="direction">移动按键的输入方向</param>
        /// <param name="shuouldConsiderCameraRotation">是否考虑摄像机旋转（默认true</param>
        /// <returns></returns>
        protected float UpdateTargetRotation(Vector3 direction, bool shuouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shuouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        /// <summary> 获取目标绕y轴的旋转角度方向
        /// </summary>
        /// <param name="targetAngle">玩家相对于摄像机的旋转角度（Y轴）</param>
        /// <returns></returns>
        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        /// <summary>重置移动速度为0
        /// </summary>
        protected void ResetVelocity()
        {
            // TODO：character
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }

        /// <summary>重置y轴移动速度为0
        /// </summary>
        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            // TODO：character
            stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
        }


        /// <summary> 水平缓慢减速，在移动后滑动效果（往反方向缓慢加速！！）！
        /// </summary>
        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration); // 忽略质量的加速度，此ForceMode会乘以deltaTime
        }

        /// <summary> 垂直缓慢减速即增加垂直向上的力（往反方向缓慢加速！！）！
        /// </summary>
        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            stateMachine.Player.Rigidbody.AddForce(-playerVerticalVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration); // 忽略质量的加速度，此ForceMode会乘以deltaTime
        }

        /// <summary> 通过检查玩家水平（x与z轴）速度的大小判断玩家是否正在移动(不检查y轴)
        /// </summary>
        /// <param name="minimumMagnitude">判断玩家是否移动的最小移动变量模大小！</param>
        /// <returns>判断玩家是否在移动：当速度变量的模为0或接近于0时返回true，否则返回false</returns>
        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            // 该水平速度包含 y 速度为0的值 会受到影响
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);

            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }

        /// <summary> 判断是否在往上方移动（如上坡移动！）
        /// </summary>
        /// <param name="minimumVelocity">默认最小y轴速度判断 0.1f</param>
        /// <returns>返回当前y轴速度是否大于minimumVelocity=0.1</returns>
        protected bool IsMovingUp(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y > minimumVelocity;
        }

        /// <summary> 判断是否在往下方移动（如下坡移动！）
        /// </summary>
        /// <param name="minimumVelocity">默认最小y轴速度判断 0.1f</param>
        /// <returns>返回当前y轴速度是否小于-minimumVelocity=0.1</returns>
        protected bool IsMovingDown(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y < -minimumVelocity;
        }

        /// <summary> 与地面接触后触发的调用！！
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnContactWithGround(Collider collider)
        {

        }

        /// <summary> 与地面退出接触时的调用！！
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnContactWithGroundExited(Collider collider)
        {

        }

        /// <summary> 根据相机角度和运动输入禁用或启用“水平居中”选项，有一种情况会禁用“重新定位”，即当停止移动时，实际是在OnMovementCanceled中执行“重新定位”操作！
        /// </summary>
        /// <param name="movementInput"></param>
        protected void UpdateCameraRecenteringState(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero)
            {
                return;
            }

            // 往前时取消摄像机居中更新
            if (movementInput == Vector2.up)
            {
                DisableCameraRecentering();

                return;
            }

            //MARKER： 获取相机垂直角度 （判断范围是否在“-90”到“90”之间，因为向上看与向下看具有相同的效果，但是我们只检查列表中的正角度
            float cameraVerticalAngle = stateMachine.Player.MainCameraTransform.eulerAngles.x;

            // eulerAngles 仅返回正值，即“-90”会返回“270”，当为正值时，我们需要将其设为“90”而不是“270”
            if (cameraVerticalAngle >= 270f)
            {
                cameraVerticalAngle -= 360f;
            }

            // 因此使用绝对值转换为正值（判断范围是否在“-90”到“90”之间，因为向上看与向下看具有相同的效果，但是我们只检查列表中的正角度
            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);
            // 向后时需获取相机角度 迭代“向后”列表，查看是否在范围内，如果不在范围内则禁用
            if (movementInput == Vector2.down)
            {
                // MARKER： 设置摄像机重新居中！
                SetCameraRecenteringState(cameraVerticalAngle, stateMachine.ReusableData.BackwardsCameraRecenteringData);

                return;
            }

            // 剩下的横向移动的设置，且对角线使用与侧向方向相同的设置
            SetCameraRecenteringState(cameraVerticalAngle, stateMachine.ReusableData.SidewaysCameraRecenteringData);

        }

        /// <summary> 设置摄像机重新居中状态！
        /// </summary>
        /// <param name="cameraVerticalAngle"></param>
        /// <param name="cameraRecenteringData"></param>
        protected void SetCameraRecenteringState(float cameraVerticalAngle, List<PlayerCameraRecenteringData> cameraRecenteringData)
        {
            foreach (PlayerCameraRecenteringData recenteringData in cameraRecenteringData)
            {
                if (!recenteringData.IsWithingRange(cameraVerticalAngle))
                {
                    continue;
                }

                EnableCameraRecentering(recenteringData.WaitTime, recenteringData.RecenteringTime);

                return;
            }

            DisableCameraRecentering();
        }

        protected void EnableCameraRecentering(float waitTime = -1f, float recenteringTime = -1f)
        {
            float movementSpeed = GetMovementSpeed();

            if (movementSpeed == 0f)
            {
                // 防止通过移动速度获取相机水平居中时间时，除以0！！
                movementSpeed = movementData.BaseSpeed;
            }

            // 增加移动速度，为不同的速度设置不同的相机水平居中更新时间！
            stateMachine.Player.CameraUtility.EnableRecentering(waitTime, recenteringTime, movementData.BaseSpeed, movementSpeed);
        }

        protected void DisableCameraRecentering()
        {
            stateMachine.Player.CameraUtility.DisableRecentering();
        }


        #endregion

        #region Input Methods
        /// <summary> 按键判断是否切换行走/跑步状态
        /// </summary>
        /// <param name="context">输入动作表</param>
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        }

        /// <summary> 调用摄像机取消居中方法！！（原为私有方法，不会与“GroundedState”方法冲突！！ 后删除GroundState中 virtual 停止移动方法！
        /// </summary>
        /// <param name="context"></param>
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            DisableCameraRecentering();
        }

        private void OnMouseMovementStarted(InputAction.CallbackContext context)
        {
            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            // MARKER: 使用Vector2作为回调的主要原因：按住移动按键时，读取MovementInput在“Update”方法中读取的“移动输入”尚未更新为在此回调中拥有的值，因为此回调是在设置值之前调用的，因此要实时更新context变量中更新后的值
            // UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
            UpdateCameraRecenteringState(context.ReadValue<Vector2>());
        }
        #endregion
    }
}
