using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskGoToTarget_State : Condition
    {
        // private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private EnemyStateMachine _enemyStateMachine;
        private NavMeshAgent _navMeshAgent;
        private Transform _target;

        public TaskGoToTarget_State(Transform transform)
        {
            _transform = transform;
            _enemyStateMachine = transform.GetComponent<EnemyStateMachine>();
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

            if (Vector3.Distance(_transform.position, _target.position) > blackboard.Get<float>("shortAttackRange"))
            {
                // Debug.Log($"<color=green> goto Target  距离{Vector3.Distance(_transform.position, _target.position)}   需要 {blackboard.Get<float>("shortAttackRange")} </color>");

                _enemyStateMachine.ChangeState(typeof(WalkForwardState));

                _navMeshAgent.SetDestination(_target.position);
                _navMeshAgent.speed = blackboard.Get<float>("chaseSpeed");
            }
            else if (Vector3.Distance(_transform.position, _target.position) <= blackboard.Get<float>("shortAttackRange"))
            {
                _navMeshAgent.ResetPath();
                State = NodeState.SUCCESS;
                return State;
            }

            State = NodeState.RUNNING;
            return State;
        }
    }
}
