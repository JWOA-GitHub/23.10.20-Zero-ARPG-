using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckEnemyInFOVRange_State : Condition
    {
        // private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private EnemyStateMachine _enemyStateMachine;
        private NavMeshAgent _navMeshAgent;
        private CharactersBase _charactersBase;
        private bool _isWaitingForAnimation = false;    // 判断是否需要等待动画播放完毕
        private bool _hasSearchedOnce = false;      // 判断是否已查找到目标

        public CheckEnemyInFOVRange_State(Transform transform)
        {
            _transform = transform;
            _enemyStateMachine = transform.GetComponent<EnemyStateMachine>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _charactersBase = transform.GetComponent<CharactersBase>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            object t = blackboard.Get<Transform>("target");
            if (_charactersBase.IsDead)
            {
                State = NodeState.FAILURE;
                return State;
            }
            // Debug.Log($"<color=red> {_enemyStateMachine.GetEnemyState(typeof(WalkForwardState)).StateName}  </color>");
            // Debug.Log($"<color=red> {_enemyStateMachine.GetEnemeyStatesFromIEnemyState(_enemyStateMachine.GetCurrentState()).StateName}  </color>");

            // Debug.Log("<color=red>检查巡逻范围 CheckEnemyInFOVRange       t.GetType().Name</color>");
            if (t == null || !_hasSearchedOnce)
            {
                // Debug.Log("<color=yellow> 检查范围中 搜索target 范围" + blackboard.Get<float>("fovRange") + " 搜索layermash " + blackboard.Get<int>("enemyLayerMask") + " </color>");
                Collider[] colliders = Physics.OverlapSphere(_transform.position, blackboard.Get<float>("fovRange"), blackboard.Get<int>("enemyLayerMask"));

                // Debug.Log("<color=yellow>检测到几个  " + colliders.Length + "</color>");

                if (colliders.Length > 0)
                {
                    Debug.Log("<color=yellow>检测到几个  " + colliders.Length + "</color>");
                    foreach (var i in colliders)
                    {
                        Debug.Log("<color=greeen>检测到de  " + i.name + "</color>");
                    }
                    blackboard.Add<Transform>("target", colliders[0].transform);
                    _enemyStateMachine.ChangeState(typeof(TwoHandsSmashAttackState));

                    _navMeshAgent.ResetPath();  //取消导航移动
                    _hasSearchedOnce = true; // 设置标志以表明已进行了一次搜索
                                             // 设置标志以等待动画播放完毕
                    _isWaitingForAnimation = true;

                    State = NodeState.RUNNING;
                    return State;
                }

                State = NodeState.FAILURE;
                return State;
            }

            Transform target = (Transform)t;
            if (Vector3.Distance(_transform.position, target.position) > blackboard.Get<float>("fovRange") && t != null)
            {
                blackboard.Remove("target");
                _hasSearchedOnce = false;
                State = NodeState.FAILURE;
                return State;
            }

            // 如果正在等待动画播放完毕
            if (_isWaitingForAnimation)
            {
                // 获取当前动画状态信息
                AnimatorStateInfo stateInfo = _enemyStateMachine.Animator.GetCurrentAnimatorStateInfo(0);
                // Debug.Log("            等待搜索动画播放完毕");
                // 如果当前动画播放时间大于等于动画长度（normalizedTime 大于等于 1），表示动画播放完毕

                if (stateInfo.IsName(_enemyStateMachine.GetEnemeyStatesFromIEnemyState(_enemyStateMachine.GetCurrentState()).StateName) && stateInfo.normalizedTime >= 0.9f)
                {
                    _isWaitingForAnimation = false;
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
            else if (_hasSearchedOnce && !_isWaitingForAnimation)
            {
                // Debug.Log("           已经有过目标了");

                Vector3 lookPos = target.position - agent.position;
                lookPos.y = 0; // 如果希望物体只在水平方向上看向目标，可以将y分量设置为0
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                agent.rotation = rotation;

                State = NodeState.SUCCESS;
                return State;
            }

            Debug.Log("             动画还没播放完毕，，，");
            State = NodeState.FAILURE;
            return State;

            // State = NodeState.SUCCESS;
            // return State;
        }
    }
}
