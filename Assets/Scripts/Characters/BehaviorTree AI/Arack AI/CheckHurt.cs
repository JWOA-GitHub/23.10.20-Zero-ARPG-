using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class CheckHurt : Condition
    {
        private Transform _transform;
        private EnemyAnimatorController _animatorController;
        private NavMeshAgent _navMeshAgent;

        private CharactersBase _charactersBase;
        // private float _hurtTime = 2f;
        // private float _hurtCounter = 0;

        private bool _isWaitingForAnimation = false;    // 判断是否需要等待动画播放完毕
        // private bool _hasHurtedDone = false;      // 判断是否已受击完毕

        public CheckHurt(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            _charactersBase = transform.GetComponent<CharactersBase>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            Transform target = blackboard.Get<Transform>("target");

            if (_charactersBase.IsDead || target == null)
            {
                State = NodeState.FAILURE;
                return State;
            }

            if (_charactersBase.IsHurting && !_isWaitingForAnimation)
            {
                _navMeshAgent.ResetPath();  //取消导航移动

                _isWaitingForAnimation = true;

                // Debug.Log("     受伤" + Time.time);

                Vector3 lookPos = target.position - agent.position;
                lookPos.y = 0; // 如果希望物体只在水平方向上看向目标，可以将y分量设置为0
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                agent.rotation = rotation;


                _animatorController.EnemyState = EnemyState.GetHit;

                State = NodeState.SUCCESS;
                return State;
            }
            // else
            // {
            //     State = NodeState.FAILURE;
            // }

            if (_isWaitingForAnimation)
            {
                // 获取当前动画状态信息
                AnimatorStateInfo stateInfo = _animatorController._animator.GetCurrentAnimatorStateInfo(0);
                // Debug.Log("            等待搜索动画播放完毕");
                // 如果当前动画播放时间大于等于动画长度（normalizedTime 大于等于 1），表示动画播放完毕

                if (stateInfo.IsName(_animatorController._stateToAnimationState[_animatorController.EnemyState].animationName) && stateInfo.normalizedTime >= 0.8f)
                {
                    _isWaitingForAnimation = false;
                    _charactersBase.IsHurting = false;
                    // Debug.Log("            退出 受伤123！" + charactersBase.IsHurting);
                    State = NodeState.FAILURE;
                    return State;
                }
                else
                {
                    // Debug.Log("还没退出受伤  进度：" + stateInfo.normalizedTime);
                    State = NodeState.SUCCESS;
                    return State;
                }

            }
            // Debug.Log("         受伤默认failre，，，");
            State = NodeState.FAILURE;
            return State;
        }
    }
}
