using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class EnemyStates : IEnemyState
    {
        protected Animator animator;
        protected EnemyStateMachine enemyStateMachine;
        protected CharactersBase charactersBase;

        [SerializeField, Header("Animator状态名")] string stateName;  // 存储每个状态名
        int stateHash;
        [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;

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
            animator.CrossFade(stateHash, transitionDuration);

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