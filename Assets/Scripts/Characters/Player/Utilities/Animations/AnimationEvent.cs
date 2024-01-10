using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField] private WeaponManager[] weaponManagers;

        // private void Start()
        // {
        //     weapon = GetComponentInParent<WeaponManager>();
        // }

        #region 武器攻击 动画事件（攻击检测
        private void EnableDetection()
        {
            foreach (WeaponManager weaponManager in weaponManagers)
            {

                weaponManager.ToggleDetection(true);
                Debug.Log($"<color=red>   {weaponManager.gameObject.gameObject.name} 开启攻击检测！！</color>");
            }
        }

        private void DisableDetection()
        {
            foreach (WeaponManager weaponManager in weaponManagers)
            {
                weaponManager.ToggleDetection(false);
                Debug.Log($"<color=green>   {weaponManager.gameObject.name} 关闭攻击检测！！</color>");
            }
        }
        #endregion
    }
}
