using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class SlopeData
    {
        [Tooltip("步长高度")]
        [SerializeField][field: Range(0f, 1f)] public float StepHeightPercentage { get; private set; } = 0.25f;
    }
}
