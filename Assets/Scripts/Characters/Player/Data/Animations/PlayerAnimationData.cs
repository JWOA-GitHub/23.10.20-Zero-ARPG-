using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerAnimationData
    {
        [Header("State Group Paramter Names")]
        [SerializeField] private string groundedParameterName = "Grounded";
        [SerializeField] private string movingParameterName = "Moving";
        [SerializeField] private string stoppingParameterName = "Stopping";
        [SerializeField] private string landingParameterName = "Landing";
        [SerializeField] private string airborneParameterName = "Airborne";
        [SerializeField] private string attackComboParameterName = "AttackCombo";

        [Header("Grounded Parameter Names")] // 默认为run
        [SerializeField] private string idleParameterName = "isIdling";
        [SerializeField] private string dashParameterName = "isDashing";
        [SerializeField] private string walkParameterName = "isWalking";
        [SerializeField] private string runParameterName = "isRunning";
        [SerializeField] private string sprintParameterName = "isSprinting";
        [SerializeField] private string mediumStopParameterName = "isMediumStopping";
        [SerializeField] private string hardStopParameterName = "isHardStopping";
        [SerializeField] private string rollParameterName = "isRolling";
        [SerializeField] private string hardLandParameterName = "isHardLanding";

        [Header("Airborne Parameter Names")] // 默认为Jump
        [SerializeField] private string fallParameterName = "isFalling";

        [Header("AttackCombo Parameter Names")]
        [SerializeField] private string normalAttack_1_ParameterName = "isLeftButton";
        [SerializeField] private string normalAttack_2_ParameterName = "isNormalAttacking_01_2";
        [SerializeField] private string normalAttack_3_ParameterName = "isNormalAttacking_01_3";

        [Header("Animation Clip Name")]
        [SerializeField] private PlayerNormalAttack_AnimationData normalAttack_AnimationData;


        public AnimatorStateInfo animatorStateInfo;

        public PlayerNormalAttack_AnimationData NormalAttack_AnimationData { get => normalAttack_AnimationData; }

        public int GroundedParameterHash { get; private set; }
        public int MovingParameterHash { get; private set; }
        public int StoppingParameterHash { get; private set; }
        public int LandingParameterHash { get; private set; }
        public int AirborneParameterHash { get; private set; }
        public int AttackComboParameterHash { get; private set; }

        public int IdleParameterHash { get; private set; }
        public int DashParameterHash { get; private set; }
        public int WalkParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int SprintParameterHash { get; private set; }
        public int MediumStopParameterHash { get; private set; }
        public int HardStopParameterHash { get; private set; }
        public int RollParameterHash { get; private set; }
        public int HardLandParameterHash { get; private set; }

        public int FallParameterHash { get; private set; }

        public int NormalAttack_1_ParameterHash { get; private set; }
        public int NormalAttack_2_ParameterHash { get; private set; }
        public int NormalAttack_3_ParameterHash { get; private set; }

        public void Initialize()
        {
            GroundedParameterHash = Animator.StringToHash(groundedParameterName);
            MovingParameterHash = Animator.StringToHash(movingParameterName);
            StoppingParameterHash = Animator.StringToHash(stoppingParameterName);
            LandingParameterHash = Animator.StringToHash(landingParameterName);
            AirborneParameterHash = Animator.StringToHash(airborneParameterName);
            AttackComboParameterHash = Animator.StringToHash(attackComboParameterName);

            IdleParameterHash = Animator.StringToHash(idleParameterName);
            DashParameterHash = Animator.StringToHash(dashParameterName);
            WalkParameterHash = Animator.StringToHash(walkParameterName);
            RunParameterHash = Animator.StringToHash(runParameterName);
            SprintParameterHash = Animator.StringToHash(sprintParameterName);
            MediumStopParameterHash = Animator.StringToHash(mediumStopParameterName);
            HardStopParameterHash = Animator.StringToHash(hardStopParameterName);
            RollParameterHash = Animator.StringToHash(rollParameterName);
            HardLandParameterHash = Animator.StringToHash(hardLandParameterName);

            FallParameterHash = Animator.StringToHash(fallParameterName);

            NormalAttack_1_ParameterHash = Animator.StringToHash(normalAttack_1_ParameterName);
            NormalAttack_2_ParameterHash = Animator.StringToHash(normalAttack_2_ParameterName);
            NormalAttack_3_ParameterHash = Animator.StringToHash(normalAttack_3_ParameterName);
        }


    }
}
