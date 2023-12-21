using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CheckHurt : Condition
    {
        private Transform _transform;
        private Animator _animator;
        private Health _health;
        private bool isHurting;
        private float _hurtTime;
        private float _hurtCounter = 0;

        public CheckHurt(Transform transform)
        {
            _transform = transform;

            _health = transform.GetComponent<Health>();
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
