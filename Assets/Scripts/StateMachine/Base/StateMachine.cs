using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public abstract class StateMachine
    {
        protected IState currentState;
        public void InitState(IState state)
        {
            currentState = state;
        }

        public void ChangeState(IState newState)
        {
            currentState.Exit();

            currentState = newState;

            currentState.Enter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }

        public void LogicUpdate()
        {
            currentState.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            currentState.PhysicsUpdate();
        }

    }
}
