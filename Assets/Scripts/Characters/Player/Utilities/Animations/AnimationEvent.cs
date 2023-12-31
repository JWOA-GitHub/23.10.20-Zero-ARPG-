using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class AnimationEvent : MonoBehaviour
    {
        private WeaponManager weapon;

        private void Start()
        {
            weapon = GetComponentInParent<WeaponManager>();
        }

        #region 武器攻击 动画事件（攻击检测
        private void EnableDetection()
        {
            weapon.ToggleDetection(true);
        }

        private void DisableDetection()
        {
            weapon.ToggleDetection(false);
        }
        #endregion
    }
}
