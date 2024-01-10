using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckDeath_State : Action
    {
        private Transform _transform;
        private EnemyStateMachine _enemyStateMachine;
        private NavMeshAgent _navMeshAgent;

        private CharactersBase _character;

        public CheckDeath_State(Transform transform)
        {
            _transform = transform;
            _enemyStateMachine = transform.GetComponent<EnemyStateMachine>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _character = transform.GetComponent<CharactersBase>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            if (_character.IsDead)
            {
                _navMeshAgent.ResetPath();  //取消导航移动
                _enemyStateMachine.ChangeState(typeof(DeathState));

                State = NodeState.SUCCESS;
                return State;
            }

            State = NodeState.FAILURE;
            return State;
        }
    }
}
