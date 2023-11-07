using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class DefaultColliderData
    {
        [field: SerializeField] public float Height { get; private set; } = 2.48f;      //1.8f;  
        [field: SerializeField] public float CenterY { get; private set; } = 1.24f;     //0.9f;
        [field: SerializeField] public float Radius { get; private set; } = 0.3f;       //0.2f;

    }
}
