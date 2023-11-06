using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; set; }
        public Rigidbody Rigidbody { get; private set; }
        [Tooltip("玩家输入动作表管理")] public PlayerInput Input { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        [Tooltip("玩家移动状态机")] private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();

            MainCameraTransform = Camera.main.transform;

            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start()
        {
            movementStateMachine.InitState(movementStateMachine.IdingState);
            Debug.Log(movementStateMachine.IdingState);
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
