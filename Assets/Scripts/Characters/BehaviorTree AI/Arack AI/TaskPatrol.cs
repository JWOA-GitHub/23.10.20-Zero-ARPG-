using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskPatrol : Action
    {
        private Transform _transform;
        private EnemyAnimatorController _animatorController;
        private NavMeshAgent _navMeshAgent;
        private Transform[] _waypoints;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f;
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();

            _waypoints = waypoints;

            // 设置巡逻起始点
            // _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
            // _animationHash = Animator.StringToHash(_animationName);
            // _animator.CrossFade(_animationHash, _transitionDuration);
            // _navMeshAgent.enabled = true;

        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            // Debug.Log("         巡逻··························");
            _animatorController.EnemyState = EnemyState.Patrol;
            // 巡逻过程中判断敌人是否进入视野范围
            // Transform target = (Transform)GetData("target");
            // if (Vector3.Distance(_transform.position, target.position) <= GuardBT.fovRange)
            // {
            //     state = NodeState.FAILURE;
            //     return state;
            // }

            float speed = blackboard.Get<float>("speed");
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                    _waiting = false;
            }
            else
            {
                // _navMeshAgent.enabled = true;
                Transform wp = _waypoints[_currentWaypointIndex];
                // if (Vector3.Distance(agent.position, wp.position) < 0.01f)
                // {
                //     agent.position = wp.position;
                //     _waitCounter = 0f;
                //     _waiting = true;

                //     _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                // }
                // else
                // {
                //     agent.position = Vector3.MoveTowards(agent.position, wp.position, speed * Time.deltaTime);
                //     agent.LookAt(wp.position);
                // }
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
                }
            }

            State = NodeState.RUNNING;
            return State;
        }
    }
}
