using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            movementStateMachine = new PlayerMovementStateMachine();
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
