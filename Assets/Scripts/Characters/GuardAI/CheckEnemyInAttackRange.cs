using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CheckEnemyInAttackRange : Node
    {
        private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private Animator _animator;

        public CheckEnemyInAttackRange(Transform transform)
        {
            _transform = transform;
            // _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            Transform target = (Transform)t;
            Debug.Log("检查攻击距离  CheckEnemyInAttackRange" + Vector3.Distance(_transform.position, target.position) + " 攻击与否 " + (Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange));
            if (Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange)
            {
                Debug.Log("             攻击                        ···");
                // _animator.SetBool("Attacking", true);
                // _animator.SetBool("Walking", false);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
