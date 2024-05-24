using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckEnemyInLongAttackRange_State : Condition
    {
        private Transform _transform;
        private EnemyStateMachine _enemyStateMachine;
        private NavMeshAgent _navMeshAgent;
        private CharactersBase _player;


        public CheckEnemyInLongAttackRange_State(Transform transform)
        {
            _transform = transform;
            _enemyStateMachine = transform.GetComponent<EnemyStateMachine>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
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
            // Debug.Log("检查攻击距离  CheckEnemyInAttackRange当前距离" + Vector3.Distance(_transform.position, target.position) + " 攻击与否 " + (Vector3.Distance(_transform.position, target.position) <= blackboard.Get<float>("shortAttackRange")));
            if (EnemyAI.isAttacking || Vector3.Distance(_transform.position, target.position) <= blackboard.Get<float>("longAttackRange"))
            {
                _navMeshAgent.ResetPath();  //取消导航移动

                Debug.Log("             找到攻击目标                        ···");
                State = NodeState.SUCCESS;
                return State;
            }

            State = NodeState.FAILURE;
            return State;
        }
    }

}

