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

        [Tooltip("玩家轻着陆状态")] public PlayerLightLandingState LightLandingState { get; }
        [Tooltip("玩家滚动状态（中着陆")] public PlayerRollingState RollingState { get; }
        [Tooltip("玩家硬着陆状态")] public PlayerHardLandingState HardLandingState { get; }

        [Tooltip("玩家跳跃状态")] public PlayerJumpingState JumpingState { get; }
        [Tooltip("玩家坠落状态")] public PlayerFallingState FallingState { get; }

        [Tooltip("玩家普通攻击1状态")] public PlayerNormalAttacking_01_1_State NormalAttacking_01_1_State { get; }
        [Tooltip("玩家普通攻击2状态")] public PlayerNormalAttacking_01_2_State NormalAttacking_01_2_State { get; }
        [Tooltip("玩家普通攻击3状态")] public PlayerNormalAttacking_01_3_State NormalAttacking_01_3_State { get; }

        [Tooltip("玩家攻击1状态")] public PlayerNormalAttacking_02_1_State NormalAttacking_02_1_State { get; }
        [Tooltip("玩家攻击2状态")] public PlayerNormalAttacking_02_2_State NormalAttacking_02_2_State { get; }
        [Tooltip("玩家攻击3状态")] public PlayerNormalAttacking_02_3_State NormalAttacking_02_3_State { get; }
        [Tooltip("玩家攻击4状态")] public PlayerNormalAttacking_02_4_State NormalAttacking_02_4_State { get; }
        [Tooltip("玩家攻击5状态")] public PlayerNormalAttacking_02_5_State NormalAttacking_02_5_State { get; }

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

            LightLandingState = new PlayerLightLandingState(this);
            RollingState = new PlayerRollingState(this);
            HardLandingState = new PlayerHardLandingState(this);

            JumpingState = new PlayerJumpingState(this);
            FallingState = new PlayerFallingState(this);

            NormalAttacking_01_1_State = new PlayerNormalAttacking_01_1_State(this);
            NormalAttacking_01_2_State = new PlayerNormalAttacking_01_2_State(this);
            NormalAttacking_01_3_State = new PlayerNormalAttacking_01_3_State(this);

            NormalAttacking_02_1_State = new PlayerNormalAttacking_02_1_State(this);
            NormalAttacking_02_2_State = new PlayerNormalAttacking_02_2_State(this);
            NormalAttacking_02_3_State = new PlayerNormalAttacking_02_3_State(this);
            NormalAttacking_02_4_State = new PlayerNormalAttacking_02_4_State(this);
            NormalAttacking_02_5_State = new PlayerNormalAttacking_02_5_State(this);
        }

    }
}
