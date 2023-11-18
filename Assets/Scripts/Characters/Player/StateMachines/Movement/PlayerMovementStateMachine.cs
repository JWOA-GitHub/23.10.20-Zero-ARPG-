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
        [Tooltip("玩家疾跑状态")] public PlayerSprintingState SprintingState { get; }

        [Tooltip("玩家轻停止状态")] public PlayerLightStoppingState LightStoppingState { get; }
        [Tooltip("玩家中停止状态")] public PlayerMediumStoppingState MediumStoppingState { get; }
        [Tooltip("玩家硬停止状态")] public PlayerHardStoppingState HardStoppingState { get; }
        [Tooltip("玩家跳跃状态")] public PlayerJumpingState JumpingState { get; }
        [Tooltip("玩家坠落状态")] public PlayerFallingState FallingState { get; }

        //TODO: 重构状态为字典类型
        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();

            IdingState = new PlayerIdlingState(this);
            DashingState = new PlayerDashingState(this);

            WalkingState = new PlayerWalkingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);

            LightStoppingState = new PlayerLightStoppingState(this);
            MediumStoppingState = new PlayerMediumStoppingState(this);
            HardStoppingState = new PlayerHardStoppingState(this);

            JumpingState = new PlayerJumpingState(this);
            FallingState = new PlayerFallingState(this);
        }

    }
}
