using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CapsuleColliderData
    {
        public CapsuleCollider Collider { get; private set; }
        /// <summary>Collider.bounds.center碰撞体在本地空间的中心点
        /// <see cref = "ColliderCenterInLocalSpace"/>
        /// </summary>
        public Vector3 ColliderCenterInLocalSpace { get; private set; }
        /// <summary>Collider.bounds.extents获取碰撞体大小的一半
        /// <see cref = "ColliderVerticalExtents"/>
        /// </summary>
        public Vector3 ColliderVerticalExtents { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if (Collider != null)
            {
                return;
            }

            Collider = gameObject.GetComponent<CapsuleCollider>();

            UpdateColliderData();
        }

        public void UpdateColliderData()
        {
            ColliderCenterInLocalSpace = Collider.center;

            ColliderVerticalExtents = new Vector3(0f, Collider.bounds.extents.y, 0f);
        }
    }
}
