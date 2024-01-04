using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskPatrol_State : Action
    {
        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private EnemyStateMachine _enemyStateMachine;
        private Transform[] _waypoints;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f;
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public TaskPatrol_State(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _enemyStateMachine = transform.GetComponent<EnemyStateMachine>();

            _waypoints = waypoints;
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {

            float speed = blackboard.Get<float>("speed");
            if (_waiting)
            {
                // _enemyStateMachine.ChangeState(typeof(FreeState));
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                    _waitCounter = 0;
                }
            }
            else
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _navMeshAgent.speed = speed;

                    _enemyStateMachine.ChangeState(typeof(WalkForwardState));

                    _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].position);

                    _waiting = true;
                }
            }

            State = NodeState.RUNNING;
            return State;
        }
    }

}

