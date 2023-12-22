using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckHurt : Condition
    {
        private Transform _transform;
        private EnemyAnimatorController _animatorController;
        private NavMeshAgent _navMeshAgent;

        private Health _health;
        private bool isHurting;
        private float _hurtTime;
        private float _hurtCounter = 0;

        public CheckHurt(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            
            // 禁用 NavMeshAgent 组件
            _navMeshAgent.enabled = false;

            // _health = transform.GetComponent<Health>();
            // _animationHash = Animator.StringToHash(_animationName);
            // _animator.CrossFade(_animationHash, _transitionDuration);
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            bool isDead = _health.HealthPoints <= 0;
            if (isDead)
            {
                State = NodeState.FAILURE;
                return State;
            }

            if (isHurting)
            {
                // _animator.SetBool("");
                _hurtCounter += Time.deltaTime;
                if (_hurtCounter >= _hurtTime)
                {
                    isHurting = false;
                    _hurtCounter = 0;
                }
                State = NodeState.SUCCESS;
            }
            else
            {
                State = NodeState.FAILURE;
            }

            return State;
        }
    }
}
