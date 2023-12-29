using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckEnemyInFOVRange : Condition
    {
        // private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private EnemyAnimatorController _animatorController;
        private NavMeshAgent _navMeshAgent;
        private CharactersBase _charactersBase;
        private bool _isWaitingForAnimation = false;    // 判断是否需要等待动画播放完毕
        private bool _hasSearchedOnce = false;      // 判断是否已查找到目标

        public CheckEnemyInFOVRange(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _charactersBase = transform.GetComponent<CharactersBase>();
            // 禁用 NavMeshAgent 组件

            // _navMeshAgent.enabled = false;
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            object t = blackboard.Get<Transform>("target");
            if (_charactersBase.IsDead)
            {
                State = NodeState.FAILURE;
                return State;
            }
            // _navMeshAgent.enabled = true;
            _navMeshAgent.ResetPath();
            // Debug.Log("<color=red>检查巡逻范围 CheckEnemyInFOVRange       t.GetType().Name</color>");
            if (t == null || !_hasSearchedOnce)
            {
                // Debug.Log("<color=yellow> 检查范围中 搜索target 范围" + blackboard.Get<float>("fovRange") + " 搜索layermash " + blackboard.Get<int>("enemyLayerMask") + " </color>");
                Collider[] colliders = Physics.OverlapSphere(_transform.position, blackboard.Get<float>("fovRange"), blackboard.Get<int>("enemyLayerMask"));

                // Debug.Log("<color=yellow>检测到几个  " + colliders.Length + "</color>");

                if (colliders.Length > 0)
                {
                    Debug.Log("<color=yellow>检测到几个  " + colliders.Length + "</color>");

                    // 根目录 root 结点 比 它 高两层
                    // parent.parent.SetData("target", colliders[0].transform);
                    blackboard.Add<Transform>("target", colliders[0].transform);
                    // _animator.SetBool("Walking",true); 
                    _animatorController.EnemyState = EnemyState.Search;

                    _navMeshAgent.ResetPath();
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
            if(Vector3.Distance(_transform.position, target.position) > blackboard.Get<float>("fovRange") && t!= null)
            {
                blackboard.Remove("target");
                _hasSearchedOnce = false;
            }

            // 如果正在等待动画播放完毕
            if (_isWaitingForAnimation)
            {
                // 获取当前动画状态信息
                AnimatorStateInfo stateInfo = _animatorController._animator.GetCurrentAnimatorStateInfo(0);
                // Debug.Log("            等待搜索动画播放完毕");
                // 如果当前动画播放时间大于等于动画长度（normalizedTime 大于等于 1），表示动画播放完毕

                if (stateInfo.IsName(_animatorController._stateToAnimationState[_animatorController.EnemyState].animationName) && stateInfo.normalizedTime >= 0.9f)
                {
                    _isWaitingForAnimation = false;
                    // Debug.Log("            退出 搜索");
                    State = NodeState.SUCCESS;
                    return State;
                }
                else
                {
                    agent.LookAt(target);
                    // Debug.Log("还没退出搜索");
                    State = NodeState.RUNNING;
                    return State;
                }
            }
            else if (_hasSearchedOnce && !_isWaitingForAnimation)
            {
                // Debug.Log("           已经有过目标了");
                agent.LookAt(target);
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
