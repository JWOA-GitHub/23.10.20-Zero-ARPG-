using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;

        // private void Start()
        // {
        //     weapon = GetComponentInParent<WeaponManager>();
        // }

        #region 武器攻击 动画事件（攻击检测
        private void EnableDetection()
        {
            Debug.Log($"<color=red>   {weaponManager.gameObject.gameObject.name} 开启攻击检测！！</color>");
            weaponManager.ToggleDetection(true);
        }

        private void DisableDetection()
        {
            weaponManager.ToggleDetection(false);
            Debug.Log($"<color=green>   {weaponManager.gameObject.name} 关闭攻击检测！！</color>");
        }
        #endregion
    }
}
