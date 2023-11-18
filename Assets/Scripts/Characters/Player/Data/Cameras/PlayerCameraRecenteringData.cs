using System;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>摄像机重新居中数据
    /// </summary>
    [Serializable]
    public class PlayerCameraRecenteringData
    {
        [field: SerializeField][field: Range(0f, 360f)] public float MinimumAngle { get; private set; }
        [field: SerializeField][field: Range(0f, 360f)] public float MaximumAngle { get; private set; }
        [field: SerializeField][field: Range(-1f, 20f)] public float WaitTime { get; private set; }
        [field: SerializeField][field: Range(-1f, 20f)] public float RecenteringTime { get; private set; }


        /// <summary> 判断该角度是否在范围内（最大角度与最小角度之间）
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public bool IsWithingRange(float angle)
        {
            return angle >= MinimumAngle && angle <= MaximumAngle;
        }
    }
}
