using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JWOAGameSystem
{
    public class TaskAttack : Action
    {
        private Transform _transform;
        private Transform _lastTarget;
        private EnemyAnimatorController _animatorController;
        private NavMeshAgent _navMeshAgent;


        // private EnemyManager _enemyManager;
        private CharactersBase _player;

        private float _attackTime = 1f;
        private float _attackCounter = 0f;

        public TaskAttack(Transform transform)
        {
            _transform = transform;
            _animatorController = transform.GetComponent<EnemyAnimatorController>();
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            Transform target = blackboard.Get<Transform>("target");
            if (target != _lastTarget)
            {
                // _enemyManager = target.GetComponent<EnemyManager>();
                _player = target.GetComponent<CharactersBase>();
                _lastTarget = target;
                Debug.LogWarning(_player.name + "                获取成功？");
                Debug.Log("         更换了攻击目标");
            }
            // Debug.Log(" _attackCounter      " + _attackCounter);
            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                // 禁用 NavMeshAgent 组件
                _navMeshAgent.ResetPath();

                // _navMeshAgent.enabled = false;

                // TODO: 敌人在动画事件中开启攻击检测
                _animatorController.EnemyState = EnemyState.Attack;

                // _player.TakeDamage(blackboard.Get<int>("attackDamage"));
                // _player.GetDamage(blackboard.Get<int>("attackDamage"),);
                Debug.Log($"      <color=red>  {agent.gameObject.name} 正在攻击了</color>" + Vector3.Distance(_transform.position, target.position));
                if (_player.IsDead)
                {
                    Debug.Log("      <color=red>            死亡 移除了攻击目标</color>");
                    blackboard.Remove("target");
                    // _animator.SetBool("Attacking", false);
                    // _animator.SetBool("Walking", true);
                }
                else
                {
                    _attackCounter = 0f;
                }
            }

            State = NodeState.RUNNING;
            return State;
        }
    }
}
