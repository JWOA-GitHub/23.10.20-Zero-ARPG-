using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
        /// <summary>斜坡不同角度对应不同移动速度曲线
        /// <see cref="SlopeSpeedAngles"/>
        /// </summary>
        [field: SerializeField] public AnimationCurve SlopeSpeedAngles { get; private set; }
        // 曲线效果：当与斜坡的角度越大时，则斜坡速度调节器为值越小

        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
        [field: SerializeField] public PlayerSprintData SprintData { get; private set; }

        [field: SerializeField] public PlayerDashData DashData { get; private set; }
    }
}
