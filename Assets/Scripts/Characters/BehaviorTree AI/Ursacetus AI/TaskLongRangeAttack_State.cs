using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskLongRangeAttack_State : Action
    {
        private Transform _transform;
        private Transform _lastTarget;
        private EnemyStateMachine _enemyStateMachine;
        private NavMeshAgent _navMeshAgent;

        private CharactersBase _player;

        public TaskLongRangeAttack_State(Transform transform)
        {
            _transform = transform;
            _enemyStateMachine = transform.GetComponent<EnemyStateMachine>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            Transform target = blackboard.Get<Transform>("target");
            if (target != _lastTarget)
            {
                _player = target.GetComponent<CharactersBase>();
                _lastTarget = target;
                Debug.Log("         更换了攻击目标为" + _lastTarget.name);
            }

            if (!EnemyAI.isShortRangeAttacking)
            {
                _navMeshAgent.ResetPath();  //取消导航移动

                Vector3 lookPos = target.position - agent.position;
                lookPos.y = 0; // 如果希望物体只在水平方向上看向目标，可以将y分量设置为0
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                agent.rotation = rotation;

                // MARKER: 敌人在动画事件中开启攻击检测
                int attackWay = Random.Range(0, 4);
                switch (attackWay)
                {
                    case 0:
                        _enemyStateMachine.ChangeState(typeof(LeftHandSmashAttackState));
                        break;
                    case 1:
                        _enemyStateMachine.ChangeState(typeof(RightHandSmashAttackState));
                        break;
                    case 2:
                        _enemyStateMachine.ChangeState(typeof(TwoHandsSmashAttackState));
                        break;
                    case 3:
                        _enemyStateMachine.ChangeState(typeof(TwoHitComboAttackForwardState));
                        break;
                    default:
                        _enemyStateMachine.ChangeState(typeof(TwoHitComboAttackForwardState));
                        break;
                }


                EnemyAI.isShortRangeAttacking = true;

                if (_player.IsDead)
                {
                    Debug.Log("      <color=red>            死亡 移除了攻击目标</color>");
                    blackboard.Remove("target");

                    State = NodeState.FAILURE;
                    return State;
                }
            }

            // 如果正在等待动画播放完毕
            if (EnemyAI.isShortRangeAttacking)
            {
                // 获取当前动画状态信息
                AnimatorStateInfo stateInfo = _enemyStateMachine.Animator.GetCurrentAnimatorStateInfo(0);
                // Debug.Log("            等待搜索动画播放完毕");

                if (stateInfo.IsName(_enemyStateMachine.GetEnemeyStatesFromIEnemyState(_enemyStateMachine.GetCurrentState()).StateName) && stateInfo.normalizedTime >= 0.9f)
                {
                    EnemyAI.isShortRangeAttacking = false;
                    EnemyAI.isAttacking = false;
                    // Debug.Log("            退出 搜索");
                    State = NodeState.SUCCESS;
                    return State;
                }
                else
                {
                    // Debug.Log("还没退出搜索");
                    State = NodeState.RUNNING;
                    return State;
                }
            }

            State = NodeState.RUNNING;
            return State;
        }
    }

}

