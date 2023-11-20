using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class DefaultColliderData
    {
        [field: SerializeField] public float Height { get; private set; } = 1.5f;      //1.8f  2.48f;  
        [field: SerializeField] public float CenterY { get; private set; } = 0.77f;     //0.9f  1.24f;
        [field: SerializeField] public float Radius { get; private set; } = 0.15f;       //0.2f   0.3f;

    }
}
