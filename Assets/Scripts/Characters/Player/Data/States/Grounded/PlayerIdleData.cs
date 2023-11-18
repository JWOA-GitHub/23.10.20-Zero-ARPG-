using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerIdleData
    {
        /// <summary>向后摄像机重新居中数据
        /// <see cref="BackwardsCameraRecenteringData"/>
        /// </summary>
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
    }
}
