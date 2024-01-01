using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CapsuleDetection : Detection
    {
        public Transform startPoint;
        public Transform endPoint;
        public float radius;

        public bool debug;

        private void OnDrawGizmos()
        {
            if (debug && startPoint != null && endPoint != null)
            {
                Vector3 direction = endPoint.position - startPoint.position;
                float length = direction.magnitude;
                direction.Normalize();

                if (length > 0)
                {
                    Gizmos.color = Color.yellow;

                    // 绘制两个圆
                    Gizmos.DrawWireSphere(startPoint.position, radius);
                    Gizmos.DrawWireSphere(endPoint.position, radius);

                    // 获取四根线段 
                    // 用于获取垂直于坐标系y轴和direction的方向向量
                    Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;

                    Gizmos.DrawLine(startPoint.position + perpendicular * radius, endPoint.position + perpendicular * radius);
                    Gizmos.DrawLine(startPoint.position - perpendicular * radius, endPoint.position - perpendicular * radius);

                    // 绘制于垂直轴平行的字段
                    perpendicular = Vector3.Cross(perpendicular, direction).normalized;

                    Gizmos.DrawLine(startPoint.position + perpendicular * radius, endPoint.position + perpendicular * radius);
                    Gizmos.DrawLine(startPoint.position - perpendicular * radius, endPoint.position - perpendicular * radius);
                }
            }
        }

        public override List<Collider> GetDetection()
        {
            List<Collider> result = new List<Collider>();
            Collider[] hits = Physics.OverlapCapsule(startPoint.position, endPoint.position, radius);

            foreach (var item in hits)
            {
                // 将要施加攻击的目标
                AgentHitBox hitBox;
                if (item.TryGetComponent(out hitBox) && hitBox.agent && targetTags.Contains(hitBox.agent.tag) && !wasHit.Contains(hitBox.agent))
                {
                    Debug.Log($"<color=yellow>{gameObject.name}   击中了 {hitBox.gameObject.name}</color>");
                    wasHit.Add(hitBox.agent);
                    result.Add(item);
                }
            }

            return result;
        }

    }

}
