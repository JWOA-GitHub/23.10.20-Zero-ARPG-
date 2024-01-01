using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class testCollider : MonoBehaviour
    {
        private void OnCollisionStay(Collision other)
        {
            Debug.Log("     攻击碰到 " + other.collider.name);
        }
    }
}
