using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JWOAGameSystem
{
    public class PlayerMovementState : IState
    {
        [Tooltip("移动输入状态机：空闲、步行、跑步、冲刺")]
        protected PlayerMovementStateMachine stateMachine;
        /// <summary> Vector2 按键输入！！！
        /// <see cref="movementInput"/>
        /// </summary>
        [Tooltip("移动按键输入")] protected Vector2 movementInput;
        [Tooltip("基础速度")] protected float baseSpeed = 5f;
        [Tooltip("速度调整器(倍率)")] protected float speedModeifier = 1f;

        // MARKER： 创建Vector3 是因为在后面的“滑动系统”中，需要“x”和“z”轴的值！！！！
        /// <summary>当前旋转目标角度
        /// <see cref="currentTargetRotation"/>
        /// </summary>
        [Tooltip("当前旋转目标角度")] protected Vector3 currentTargetRotation;

        /// <summary>达到目标角度所需的时间
        /// <see cref="timeToReachTargetRotation"/>
        /// </summary>
        [Tooltip("达到目标角度所需的时间")] protected Vector3 timeToReachTargetRotation;

        /// <summary>平滑玩家旋转角度的速度
        /// <see cref="dampedTargetRotationCurrentVelocity"/>
        /// </summary>
        [Tooltip("平滑玩家旋转角度的速度")] protected Vector3 dampedTargetRotationCurrentVelocity;

        /// <summary>阻尼目标旋转经过时间
        /// <see cref="dampedTargetRotationPassedTime"/>
        /// </summary>
        [Tooltip("阻尼目标旋转经过时间")] protected Vector3 dampedTargetRotationPassedTime;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            InitializeData();
        }

        private void InitializeData()
        {
            // 初始化到达目标旋转角度的时间
            timeToReachTargetRotation.y = 0.14f;
        }

        #region IState Mathods
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }
        public virtual void Exit()
        {

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
            // 传参movementInput为Vector2！！
            // Rotate(movementInput);
        }

        #endregion


        #region Main Methods
        private void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (movementInput == Vector2.zero || speedModeifier == 0f)
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
            currentTargetRotation.y = targetAngle;

            // 重置旋转到目标所需的时间
            dampedTargetRotationPassedTime.y = 0f;
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
        /// <summary> 获取移动输入方向
        /// </summary>
        /// <returns>获取移动输入</returns>
        protected Vector3 GetMovementInputDirection()
        {
            Debug.Log(movementInput);
            return new Vector3(movementInput.x, 0, movementInput.y);
        }

        /// <summary> 获取玩家移动速度（基本速度*速度调整速率数）
        /// </summary>
        /// <returns>获取玩家移动速度</returns>
        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModeifier;
        }


        /// <summary> 获取玩家当前水平速度
        /// </summary>
        /// <returns>获取玩家当前水平速度</returns>
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }


        /// <summary> 玩家向目标角度旋转
        /// </summary>
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            // Debug.Log(currentYAngle + "      wanjia ");
            if (currentYAngle == currentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDamp(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCurrentVelocity.y, timeToReachTargetRotation.y - dampedTargetRotationPassedTime.y);

            // MARKER： 因此此方法是在FixedUpdate方法中调用的，所以使用 Time.deltaTime 变量时，unity会自动返回 fixedDeltaTime
            dampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            // Debug.Log(targetRotation.eulerAngles);
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

            if (directionAngle != currentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        /// <summary> 获取目标旋转角度方向
        /// </summary>
        /// <param name="targetAngle">玩家相对于摄像机的旋转角度（Y轴）</param>
        /// <returns></returns>
        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        #endregion
    }
}
