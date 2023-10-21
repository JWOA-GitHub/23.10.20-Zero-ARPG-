using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        public Rigidbody Rigidbody { get;  private set; }
        [Tooltip("玩家输入动作表管理")]public PlayerInput Input { get; private set; }
        [Tooltip("玩家移动状态机")]private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            Input = GetComponent<PlayerInput>();

            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start()
        {
            
            movementStateMachine.ChangeState(movementStateMachine.IdingState);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.LogicUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
