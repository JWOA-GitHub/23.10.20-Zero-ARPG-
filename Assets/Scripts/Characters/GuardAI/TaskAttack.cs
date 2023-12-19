using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class TaskAttack : Node
    {
        private Animator _animator;

        private Transform _lastTarget;
        // private EnemyManager _enemyManager;
        private PlayerHealth _playerHealth;

        private float _attackTime = 1f;
        private float _attackCounter = 0f;

        public TaskAttack(Transform transform)
        {
            // _animator = transform.GetComponent<Animator>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            Transform target = blackboard.Get<Transform>("target");
            if (target != _lastTarget)
            {
                // _enemyManager = target.GetComponent<EnemyManager>();
                _playerHealth = target.GetComponent<PlayerHealth>();
                _lastTarget = target;
            }

            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                // TODO: 敌人受到伤害
                bool enemyIsDead = _playerHealth.TakeDamage(blackboard.Get<int>("attackPower"));
                if (enemyIsDead)
                {
                    blackboard.Remove("target");
                    // _animator.SetBool("Attacking", false);
                    // _animator.SetBool("Walking", true);
                }
                else
                {
                    _attackCounter = 0f;
                }
            }

            State = NodeState.RUNNING;
            return State;
        }
    }
}