// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// namespace JWOAGameSystem
// {
//     public class RandomAttack : Action
//     {
//         private Transform _transform;
//         private Transform _lastTarget;
//         private EnemyAnimatorController _animatorController;
//         private NavMeshAgent _navMeshAgent;
//         private CharactersBase _player;

//         private float _attackTime = 1f;
//         private float _attackCounter = 0f;

//         public RandomAttack(Transform transform)
//         {
//             _transform = transform;
//             _animatorController = transform.GetComponent<EnemyAnimatorController>();
//             _navMeshAgent = transform.GetComponent<NavMeshAgent>();

//         }

//         protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
//         {
//             Transform target = blackboard.Get<Transform>("target");

//             if (target != _lastTarget)
//             {
//                 _player = target.GetComponent<CharactersBase>();
//                 _lastTarget = target;

//                 Debug.Log("         更换了攻击目标");
//             }

//             int randomAttack = Random.Range(0, 2); // 假设有种攻击方式
//             switch (randomAttack)
//             {
//                 case 0:
//                     // 直接攻击
//                     _animatorController.EnemyState = EnemyState.Attack;
//                     break;
//                 case 1:
//                     // 执行第二种攻击方式的逻辑
//                     // 比如释放技能
//                     // _animatorController.EnemyState = en
//                     break;
//                 case 2:
//                     // 执行第三种攻击方式的逻辑
//                     // 比如投掷武器
//                     // ThrowWeapon();
//                     break;
//             }

//             // Debug.Log(" _attackCounter      " + _attackCounter);
//             _attackCounter += Time.deltaTime;
//             if (_attackCounter >= _attackTime)
//             {
//                 _navMeshAgent.ResetPath(); //取消导航移动

//                 _animatorController.EnemyState = EnemyState.Attack;

//                 // TODO: 敌人受到伤害
//                 _player.TakeDamage(blackboard.Get<int>("attackDamage"));
//                 Debug.Log("      <color=red>   正在攻击了</color>" + Vector3.Distance(_transform.position, target.position));
//                 if (_player.IsDead)
//                 {
//                     Debug.Log("      <color=red>            死亡 移除了攻击目标</color>");
//                     blackboard.Remove("target");
//                     // _animator.SetBool("Attacking", false);
//                     // _animator.SetBool("Walking", true);
//                 }
//                 else
//                 {
//                     _attackCounter = 0f;
//                 }
//             }

//             State = NodeState.RUNNING;
//             return State;
//         }
//     }
// }
