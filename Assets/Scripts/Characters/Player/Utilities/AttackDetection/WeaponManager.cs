using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class WeaponManager : MonoBehaviour
    {
        // 武器数据
        // public WeaponConfig config;
        /// <summary> 攻击检测组件
        /// <see cref="detections"/>
        /// </summary>
        public List<Detection> detections = new List<Detection>();
        /// <summary> 当前是否正在进行攻击检测
        /// <see cref="isOnDetection"/>
        /// </summary>
        public bool isOnDetection;


        private void Update()
        {
            HandleDetection();
        }

        private void HandleDetection()
        {
            if (isOnDetection)
            {
                foreach (var item in detections)
                {
                    foreach (var hit in item.GetDetection())
                    {
                        hit.GetComponent<AgentHitBox>().GetDamage(10, transform.position);
                        Debug.Log(hit.name + "      被武器攻击到  受伤了！！！！！！！");
                    }
                }
            }
        }

        /// <summary> 用于开启和关闭碰撞检测
        /// </summary>
        /// <param name="value"></param>
        public void ToggleDetection(bool value)
        {
            isOnDetection = value;

            if (value)
                HandleDetection();
            else
            {
                foreach (var item in detections)
                {
                    item.ClearWasHit();
                }
            }
        }
    }
}
