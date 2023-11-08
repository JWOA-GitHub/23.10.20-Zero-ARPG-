using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerLayerData : MonoBehaviour
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    }
}
