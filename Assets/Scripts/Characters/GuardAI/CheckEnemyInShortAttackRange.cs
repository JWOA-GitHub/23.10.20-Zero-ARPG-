using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckEnemyInShortAttackRange : Condition
    {
        // private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private NavMeshAgent _navMeshAgent;
        private EnemyAnimatorController _animatorController;

        public CheckEnemyInShortAttackRange(Transform transform)
        {
            _transform = transform;
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            // 禁用 NavMeshAgent 组件
            // _navMeshAgent.SetDestination(transform.position);
            // _navMeshAgent.enabled = false;

        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            object t = blackboard.Get<Transform>("target");
            if (t == null)
            {
                State = NodeState.FAILURE;
                return State;
            }

            Transform target = (Transform)t;
            // Debug.Log("检查攻击距离  CheckEnemyInAttackRange" + Vector3.Distance(_transform.position, target.position) + " 攻击与否 " + (Vector3.Distance(_transform.position, target.position) <= blackboard.Get<float>("attackRange")));
            if (Vector3.Distance(_transform.position, target.position) <= blackboard.Get<float>("shortAttackRange"))
            {
                _navMeshAgent.ResetPath();

                // Debug.Log("             找到攻击目标                        ···");
                // _navMeshAgent.enabled = false;
                // _animator.SetBool("Attacking", true);
                // _animator.SetBool("Walking", false);

                State = NodeState.SUCCESS;
                return State;
            }

            State = NodeState.FAILURE;
            return State;
        }
    }
}
