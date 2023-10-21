using System;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerMovementState : IState
    {
        [Tooltip("移动输入状态机：空闲、步行、跑步、冲刺")]
        protected PlayerMovementStateMachine stateMachine;
        [Tooltip("移动按键输入")]protected Vector2 movementInput;
        [Tooltip("基础速度")] protected float baseSpeed = 5f;
        [Tooltip("速度调整器(倍率)")]protected float speedModeifier = 1f;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;
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
        }

        #endregion


        #region Main Methods
        private void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if(movementInput == Vector2.zero || speedModeifier == 0f)
            {
                return;
            }

            Vector3 movementDirection = GetMovementInputDirection();

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            // MARKER: 刚体移动（需减去当前已有速度，否则持续按下会越来越快！
            stateMachine.Player.Rigidbody.AddForce( movementSpeed * movementDirection - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        #endregion

        #region Reusable Methods 可复用方法
        /// <summary>
        /// 获取移动输入方向
        /// </summary>
        /// <returns>获取移动输入</returns>
        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(movementInput.x, 0, movementInput.y);
        }

        /// <summary>
        /// 获取玩家移动速度（基本速度*速度调整速率数）
        /// </summary>
        /// <returns>获取玩家移动速度</returns>
        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModeifier;
        }


        /// <summary>
        /// 获取玩家当前水平速度
        /// </summary>
        /// <returns>获取玩家当前水平速度</returns>
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }
        #endregion
    }
}
