using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerStateReusableData ReusableData { get; }
        [Tooltip("玩家空闲状态")] public PlayerIdlingState IdingState { get; }
        [Tooltip("玩家冲刺状态")] public PlayerDashingState DashingState { get; }
        [Tooltip("玩家步行状态")] public PlayerWalkingState WalkingState { get; }
        [Tooltip("玩家跑步状态")] public PlayerRunningState RunningState { get; }
        [Tooltip("玩家短跑状态")] public PlayerSprintingState SprintingState { get; }


        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();

            IdingState = new PlayerIdlingState(this);
            DashingState = new PlayerDashingState(this);

            WalkingState = new PlayerWalkingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
        }

    }
}
