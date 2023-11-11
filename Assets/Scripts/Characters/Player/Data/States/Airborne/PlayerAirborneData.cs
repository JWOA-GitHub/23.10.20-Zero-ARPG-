using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerAirborneData
    {
        [field: SerializeField] public PlayerJumpData JumpData { get; private set; }
    }
}
