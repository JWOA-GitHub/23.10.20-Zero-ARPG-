using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class Player_AnimationEvent : MonoBehaviour
    {
        [SerializeField] private WeaponManager Skill1_Detection;
        [SerializeField] private WeaponManager Skill2_Detection;
        [SerializeField] private WeaponManager Skill3_Detection;
        [SerializeField] private WeaponManager Skill4_Detection;

        #region 武器攻击 动画事件（攻击检测
        private void EnableSkill1_Detection()
        {
            Skill1_Detection.ToggleDetection(true);
        }

        private void EnableSkill2_Detection()
        {
            Skill2_Detection.ToggleDetection(true);
        }

        private void EnableSkill3_Detection()
        {
            Skill3_Detection.ToggleDetection(true);
        }

        private void EnableSkill4_Detection()
        {
            Skill4_Detection.ToggleDetection(true);
        }

        private void EnableAllDetection()
        {
            Skill1_Detection.ToggleDetection(true);
            Skill2_Detection.ToggleDetection(true);
            Skill3_Detection.ToggleDetection(true);
            Skill4_Detection.ToggleDetection(true);
        }


        private void DisableSkill1_Detection()
        {
            Skill1_Detection.ToggleDetection(false);
        }

        private void DisableSkill2_Detection()
        {
            Skill2_Detection.ToggleDetection(false);
        }

        private void DisableSkill3_Detection()
        {
            Skill3_Detection.ToggleDetection(false);
        }

        private void DisableSkill4_Detection()
        {
            Skill4_Detection.ToggleDetection(false);
        }

        private void DisableAllDetection()
        {
            Skill1_Detection.ToggleDetection(false);
            Skill2_Detection.ToggleDetection(false);
            Skill3_Detection.ToggleDetection(false);
            Skill4_Detection.ToggleDetection(false);
        }
        #endregion
    }
}
