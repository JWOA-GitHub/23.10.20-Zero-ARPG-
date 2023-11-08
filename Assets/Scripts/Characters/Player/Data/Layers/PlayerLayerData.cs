using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerLayerData
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    }
}
