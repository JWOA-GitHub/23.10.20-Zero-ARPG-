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
        private bool _isWaitingForAnimation = false;

        public CheckEnemyInFOVRange(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            // 禁用 NavMeshAgent 组件
            _navMeshAgent.SetDestination(transform.position);
            _navMeshAgent.enabled = false;
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            object t = blackboard.Get<Transform>("target");
            // Debug.Log("<color=red>检查巡逻范围 CheckEnemyInFOVRange       t.GetType().Name</color>");
            if (t == null)
            {
                Debug.Log("<color=yellow> 检查范围中 搜索target 范围 </color>" + blackboard.Get<float>("fovRange") + " 搜索layermash " + blackboard.Get<int>("enemyLayerMask"));
                Collider[] colliders = Physics.OverlapSphere(_transform.position, blackboard.Get<float>("fovRange"), blackboard.Get<int>("enemyLayerMask"));

                Debug.Log("<color=yellow>检测到几个  " + colliders.Length + "</color>");

                if (colliders.Length > 0)
                {

                    // 根目录 root 结点 比 它 高两层
                    // parent.parent.SetData("target", colliders[0].transform);
                    blackboard.Add<Transform>("target", colliders[0].transform);
                    // _animator.SetBool("Walking",true); 

                    _animatorController.EnemyState = EnemyState.Search;
                    // agent.GetComponent<MonoBehaviour>().StartCoroutine(DelayedSuccess());

                    // 设置标志以等待动画播放完毕
                    _isWaitingForAnimation = true;
                    Debug.LogError("            isWaitingForAnimation = true");
                    State = NodeState.RUNNING;
                    return State;
                }

                State = NodeState.FAILURE;
                return State;
            }

            // 如果正在等待动画播放完毕
            if (_isWaitingForAnimation)
            {
                // 获取当前动画状态信息
                AnimatorStateInfo stateInfo = _animatorController._animator.GetCurrentAnimatorStateInfo(0);
                // Debug.LogError("            等待搜索动画播放完毕");
                // 如果当前动画播放时间大于等于动画长度（normalizedTime 大于等于 1），表示动画播放完毕
                if (stateInfo.IsName(_animatorController._stateToAnimationState[_animatorController.EnemyState].animationName) && stateInfo.normalizedTime >= 0.9f)
                {
                    _isWaitingForAnimation = false;

                    // Debug.LogError(" 退出！！！");
                    State = NodeState.SUCCESS;
                    return State;
                }
                else
                {
                    State = NodeState.RUNNING;
                    return State;
                }
            }
            else
            {
                Debug.Log("             已经找到了，，，");
                State = NodeState.FAILURE;
                return State;
            }

            // State = NodeState.SUCCESS;
            // return State;
        }

        // IEnumerator DelayedSuccess()
        // {
        //     yield return new WaitForSeconds(3f);

        //     State = NodeState.SUCCESS;
        // }
    }
}
