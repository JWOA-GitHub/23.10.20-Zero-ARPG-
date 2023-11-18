using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{

    public class PlayerStateReusableData
    {
        public Vector2 MovementInput { get; set; }
        public float MovementSpeedModifier { get; set; } = 1f;
        /// <summary>斜坡移动速度调节器
        /// <see cref="MovementOnSlopesSpeedModifier"/>
        /// </summary>
        public float MovementOnSlopesSpeedModifier { get; set; } = 1f;
        /// <summary>移动速度减速力调节器
        /// <see cref="MovementDecelerationForce"/>
        /// </summary>
        public float MovementDecelerationForce { get; set; } = 1f;

        /// <summary>横向摄像机重新居中数据
        /// <see cref="SidewaysCameraRecenteringData"/>
        /// </summary>
        public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; set; }
        /// <summary>向后摄像机重新居中数据
        /// <see cref="BackwardsCameraRecenteringData"/>
        /// </summary>
        public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; set; }


        public bool ShouldWalk { get; set; }
        /// <summary>判断其他状态(如疾跑）跳跃后是否继续切换为疾跑
        /// <see cref="ShouldSprint"/>
        /// </summary>
        public bool ShouldSprint { get; set; }
        // MARKER： 创建Vector3 是因为在后面的“滑动系统”中，需要“x”和“z”轴的值！！！！
        /// <summary>当前旋转目标角度
        /// <see cref="currentTargetRotation"/>
        /// </summary>
        [Tooltip("当前旋转目标角度")] private Vector3 currentTargetRotation;

        /// <summary>达到目标角度所需的时间
        /// <see cref="timeToReachTargetRotation"/>
        /// </summary>
        [Tooltip("达到目标角度所需的时间")] private Vector3 timeToReachTargetRotation;

        /// <summary>平滑玩家旋转角度的速度
        /// <see cref="dampedTargetRotationCurrentVelocity"/>
        /// </summary>
        [Tooltip("平滑玩家旋转角度的速度")] private Vector3 dampedTargetRotationCurrentVelocity;

        /// <summary>阻尼目标旋转经过时间
        /// <see cref="dampedTargetRotationPassedTime"/>
        /// </summary>
        [Tooltip("阻尼目标旋转经过时间")] private Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }

        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }
        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }
        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }

        // 当前跳跃力
        public Vector3 CurrentJumpForce { get; set; }

        // 更改玩家旋转所需时间处用
        public PlayerRotationData RotationData { get; set; }
    }
}
