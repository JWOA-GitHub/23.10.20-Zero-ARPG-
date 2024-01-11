using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public enum WeaponType
    {
        Normal,
        Skill,
    }

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

        [SerializeField] private CharactersBase charactersBase;

        [SerializeField] private WeaponType weaponType= WeaponType.Normal;
        [SerializeField] private float skillDamageModifier = 2f;


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
                        if(weaponType == WeaponType.Normal)
                        {
                            hit.GetComponent<AgentHitBox>().GetDamage(charactersBase.AttackDamage, transform.position);
                            Debug.Log( "            attack伤害 "+charactersBase.AttackDamage);
                        }
                        else
                        {
                            hit.GetComponent<AgentHitBox>().GetDamage(charactersBase.SkillDamage * skillDamageModifier, transform.position);
                            Debug.Log( "            skill伤害 "+charactersBase.SkillDamage * skillDamageModifier);
                        }
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
