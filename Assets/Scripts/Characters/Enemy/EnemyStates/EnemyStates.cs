using System;
using UnityEngine;

namespace JWOAGameSystem
{

    public class EnemyStates : ScriptableObject, IEnemyState
    {
        protected Animator animator;
        protected EnemyStateMachine enemyStateMachine;
        protected CharactersBase charactersBase;

        [SerializeField, Header("Animator状态名")] string stateName;  // 存储每个状态名
        public string StateName
        {
            get => stateName;
        }

        int stateHash;
        public int StateHash
        {
            get => stateHash;
        }

        [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;

        protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
        float stateStartTime;
        protected float StateDuration => Time.time - stateStartTime;

        #region AnimationMove
        [HideInInspector] public int animationMoveID;
        [HideInInspector] public Vector3 animationMoveDir = Vector3.zero;
        public float animationMoveSpeedModifier = 1f;
        private LayerMask GroundLayer = 1 << 6;  // Environment层
        #endregion

        private void OnEnable()
        {
            stateHash = Animator.StringToHash(stateName);
        }

        public void Initialize(Animator animator, EnemyStateMachine enemyStateMachine, CharactersBase charactersBase)
        {
            this.animator = animator;
            this.enemyStateMachine = enemyStateMachine;
            this.charactersBase = charactersBase;

            if (Animator.StringToHash("AnimationMove") != 0)
            {
                animationMoveID = Animator.StringToHash("AnimationMove");
            }
        }

        public virtual void Enter()
        {
            // animator.CrossFade(stateHash, 0);
            // animator.Play(stateHash, (int)transitionDuration, 0f); // 使用动态的动画哈希值和过渡时间
            animator.Play(stateHash);
            Debug.Log($"进入 {GetType().Name}  Enter   +  动画名{stateName}  id {stateHash}");
            stateStartTime = Time.time;

            SetAnimationMoveBase(Vector3.zero, 0);
        }

        public virtual void Exit()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            if (animationMoveID != 0 && animationMoveDir != Vector3.zero)
            {
                // 动画中的Curve曲线 决定动画的移动位置！
                CharacterMoveInterface(animationMoveDir,
                enemyStateMachine.Animator.GetFloat(animationMoveID) * animationMoveSpeedModifier);
            }
        }

        public void SetAnimationMoveBase(Vector3 moveDir, float moveSpeedModeifier)
        {
            animationMoveDir = moveDir;
            animationMoveSpeedModifier = moveSpeedModeifier;
        }

        /// <summary>
        /// 检测移动方向是否有碰撞
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        protected bool CanAnimationMotion(Vector3 dir)
        {
            return Physics.Raycast(enemyStateMachine.transform.position + enemyStateMachine.transform.up * .5f, dir.normalized * enemyStateMachine.Animator.GetFloat(animationMoveID), out var hit, 1f, GroundLayer);
        }

        /// <summary>
        /// Animation动作位移
        /// </summary>
        /// <param name="moveDirection"></param>
        /// <param name="moveSpeed"></param>
        public void CharacterMoveInterface(Vector3 moveDirection, float moveSpeed)
        {
            if (!CanAnimationMotion(moveDirection))
            {
                Vector3 movementDirection = moveDirection.normalized;

                enemyStateMachine.CharacterController.Move(moveSpeed * Time.deltaTime * movementDirection);

                if (moveSpeed * Time.deltaTime * movementDirection != Vector3.zero)
                    Debug.Log(enemyStateMachine.gameObject.name + "         动画位移力度  " + moveSpeed + "  " + (moveSpeed * Time.deltaTime * movementDirection));
            }
        }

    }
}