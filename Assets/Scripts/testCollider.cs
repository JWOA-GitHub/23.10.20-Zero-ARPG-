using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class testCollider : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("玩家标签是否：  " + transform.GetComponent<CapsuleDetection>().targetTags[0].CompareTo(other.tag));

                Debug.Log($"<color=yellos>     攻击碰到  + {other.name}  </color>");
                other.GetComponentInChildren<WeaponManager>().ToggleDetection(true);

                // other.GetComponentInChildren<WeaponManager>().

            }
        }
        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                Debug.Log($"<color=yellos>     攻击碰到  + {other.collider.name}  </color>");
                other.collider.GetComponentInChildren<WeaponManager>().ToggleDetection(true);
            }
        }
    }
}
