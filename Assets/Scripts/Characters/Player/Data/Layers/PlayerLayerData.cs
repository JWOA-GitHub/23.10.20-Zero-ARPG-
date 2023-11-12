using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerLayerData
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }

        /// <summary> 判断“layer”是否为“LayerMask”的一部分！ LayerMask可包含多个Layer
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public bool ContaionsLayer(LayerMask layerMask, int layer)
        {
            return (1 << layer & layerMask) != 0;
        }

        /// <summary> 判断“layer”是否为GroundLayer，即判断是否触地！
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public bool IsGroundLayer(int layer)
        {
            return ContaionsLayer(GroundLayer, layer);
        }
    }
}
