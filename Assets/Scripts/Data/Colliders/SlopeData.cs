using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    [Tooltip("斜率数据")]
    public class SlopeData
    {
        [Tooltip("步长高度")]
        [field: SerializeField][field: Range(0f, 1f)] public float StepHeightPercentage { get; private set; } = 0.25f;
        [Tooltip("浮动胶囊射线检测距离！（略高于capsule高度的一半")]
        [field: SerializeField][field: Range(0f, 5f)] public float FloatRayDistance { get; private set; } = 0.8f;     // 2f 略高于capsule高度的一半
        [Tooltip("步进力")]
        [field: SerializeField][field: Range(0f, 50f)] public float StepReachForce { get; private set; } = 50f;
    }
}
