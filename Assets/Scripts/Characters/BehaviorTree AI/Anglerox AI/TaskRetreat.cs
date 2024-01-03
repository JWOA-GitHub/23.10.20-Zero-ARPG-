using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskRetreat : Action
    {
        private Transform _transform;
        private EnemyAnimatorController _animatorController;
        private NavMeshAgent _navMeshAgent;

        // private EnemyManager _enemyManager;
        private CharactersBase _player;
        private CharactersBase _charactersBase;

        private bool _isWaitingForAnimation = false;    // 判断是否需要等待动画播放完毕

        public TaskRetreat(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _charactersBase = transform.GetComponent<CharactersBase>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            Transform target = blackboard.Get<Transform>("target");

            if (target == null)
            {
                State = NodeState.FAILURE;
                return State;
            }

            bool needToRetreat = Vector3.Distance(target.position, agent.position) <= blackboard.Get<float>("retreatRange");

            if (needToRetreat && !_isWaitingForAnimation)
            {
                //  (通过动画位移 移动)
                _navMeshAgent.ResetPath();  //取消导航移动

                _isWaitingForAnimation = true;

                // 后撤前 先调整方向
                Vector3 lookPos = target.position - agent.position;
                lookPos.y = 0; // 如果希望物体只在水平方向上看向目标，可以将y分量设置为0
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                agent.rotation = rotation;


                _animatorController.EnemyState = EnemyState.JumpBackwards;

                State = NodeState.SUCCESS;
                return State;
            }

            if (_isWaitingForAnimation)
            {
                // 获取当前动画状态信息
                AnimatorStateInfo stateInfo = _animatorController._animator.GetCurrentAnimatorStateInfo(0);
                // Debug.Log("            等待后退动   画播放完毕");

                if (stateInfo.IsName(_animatorController._stateToAnimationState[_animatorController.EnemyState].animationName) && stateInfo.normalizedTime >= 0.9f)
                {
                    _isWaitingForAnimation = false;
                    _charactersBase.IsAnimationMoveing = false;
                    // Debug.Log("            退出 后撤123！" + charactersBase.IsAnimationMoveing);
                    State = NodeState.FAILURE;
                    return State;
                }
                else
                {
                    // Debug.Log("还没退出后退  进度：" + stateInfo.normalizedTime);
                    State = NodeState.SUCCESS;
                    return State;
                }

            }

            State = NodeState.FAILURE;
            return State;
        }
    }
}
