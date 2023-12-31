using System;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.225f;

        /// <summary>向后摄像机重新居中数据
        /// <see cref="BackwardsCameraRecenteringData"/>
        /// </summary>
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
    }
}
