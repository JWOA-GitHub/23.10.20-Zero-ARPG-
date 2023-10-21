using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public PlayerIdlingState IdingState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerSprintingState SprintingState { get; }

        public PlayerMovementStateMachine()
        {
            IdingState = new PlayerIdlingState();

            WalkingState = new PlayerWalkingState();
            RunningState = new PlayerRunningState();
            SprintingState = new PlayerSprintingState();
        }
    }
}
