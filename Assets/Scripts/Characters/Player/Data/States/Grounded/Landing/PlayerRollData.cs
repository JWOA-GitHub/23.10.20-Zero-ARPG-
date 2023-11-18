using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerRollData
    {
        [field: SerializeField][field: Range(0f, 3f)] public float SpeedModeifier { get; private set; } = 1f;
    }
}
