using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyAI_AnimationEvent : MonoBehaviour
    {
        [SerializeField] private WeaponManager leftHandDetection;
        [SerializeField] private WeaponManager rightHandDetection;
        [SerializeField] private WeaponManager leftFootDetection;
        [SerializeField] private WeaponManager rightFootDetection;

        #region 武器攻击 动画事件（攻击检测
        private void EnableLeftHandDetection()
        {
            leftHandDetection.ToggleDetection(true);
        }

        private void EnableRightHandDetection()
        {
            rightHandDetection.ToggleDetection(true);
        }

        private void EnableLeftFootDetection()
        {
            leftFootDetection.ToggleDetection(true);
        }

        private void EnableRightFootDetection()
        {
            rightFootDetection.ToggleDetection(true);
        }

        private void EnableTwoHandsDetection()
        {
            leftHandDetection.ToggleDetection(true);
            rightHandDetection.ToggleDetection(true);
        }

        private void EnableTwoFootsDetection()
        {
            leftFootDetection.ToggleDetection(true);
            rightFootDetection.ToggleDetection(true);
        }

        private void EnableAllDetection()
        {
            leftHandDetection.ToggleDetection(true);
            rightHandDetection.ToggleDetection(true);
            leftFootDetection.ToggleDetection(true);
            rightFootDetection.ToggleDetection(true);
        }




        private void DisableLeftHandDetection()
        {
            leftHandDetection.ToggleDetection(false);
        }

        private void DisableRightHandDetection()
        {
            rightHandDetection.ToggleDetection(false);
        }

        private void DisableLeftFootDetection()
        {
            leftFootDetection.ToggleDetection(false);
        }

        private void DisableRightFootDetection()
        {
            rightFootDetection.ToggleDetection(false);
        }

        private void DisableTwoHandsDetection()
        {
            leftHandDetection.ToggleDetection(false);
            rightHandDetection.ToggleDetection(false);
        }

        private void DisableTwoFootsDetection()
        {
            leftFootDetection.ToggleDetection(false);
            rightFootDetection.ToggleDetection(false);
        }

        private void DisableAllDetection()
        {
            leftHandDetection.ToggleDetection(false);
            rightHandDetection.ToggleDetection(false);
            leftFootDetection.ToggleDetection(false);
            rightFootDetection.ToggleDetection(false);
        }
        #endregion
    }
}
