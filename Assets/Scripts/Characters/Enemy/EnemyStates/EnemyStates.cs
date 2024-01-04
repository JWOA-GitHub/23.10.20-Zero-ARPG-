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

        private void OnEnable()
        {
            stateHash = Animator.StringToHash(stateName);
        }

        public void Initialize(Animator animator, EnemyStateMachine enemyStateMachine, CharactersBase charactersBase)
        {
            this.animator = animator;
            this.enemyStateMachine = enemyStateMachine;
            this.charactersBase = charactersBase;
        }

        public virtual void Enter()
        {
            // animator.CrossFade(stateHash, 0);
            // animator.Play(stateHash, (int)transitionDuration, 0f); // 使用动态的动画哈希值和过渡时间
            animator.Play(stateHash);
            Debug.Log($"进入 {GetType().Name}  Enter   +  动画名{stateName}  id {stateHash}");
            stateStartTime = Time.time;
        }

        public virtual void Exit()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
        }
    }
}