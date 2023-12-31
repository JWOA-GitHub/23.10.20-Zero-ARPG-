using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerGroundedState : PlayerBaseGroundedState
    {
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.GroundedParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.GroundedParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        #endregion
    }
}
