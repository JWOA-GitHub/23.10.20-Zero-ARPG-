using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskGoToTarget : Action
    {
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private EnemyAnimatorController _animatorController;
        private Transform _target;

        public TaskGoToTarget(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();

        }


        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            _target = blackboard.Get<Transform>("target");
            if (_target == null)
            {
                State = NodeState.FAILURE;
                return State;
            }

            // Debug.Log("<color=pink>         在gotoTarget 追踪？   ${)} </color>" + Vector3.Distance(_transform.position, _target.position));
            if (Vector3.Distance(_transform.position, _target.position) > blackboard.Get<float>("shortAttackRange"))
            {
                // Debug.Log($"<color=green> goto Target  距离{Vector3.Distance(_transform.position, _target.position)}   需要 {blackboard.Get<float>("shortAttackRange")} </color>");
                _animatorController.EnemyState = EnemyState.Chase;

                _navMeshAgent.SetDestination(_target.position);
                _navMeshAgent.speed = blackboard.Get<float>("chaseSpeed");
            }

            State = NodeState.RUNNING;
            return State;
        }
    }
}
