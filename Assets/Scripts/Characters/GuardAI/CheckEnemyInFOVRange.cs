using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CheckEnemyInFOVRange : Node
    {
        private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private Animator _animator;

        public CheckEnemyInFOVRange(Transform transform)
        {
            _transform = transform;
            // _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            object t = GetData("target");
            Debug.Log("<color=red>检查巡逻范围 CheckEnemyInFOVRange       t.GetType().Name</color>");
            if (t == null)
            {
                Debug.Log("<color=yellow> 检查范围中 搜索target</color>");
                Collider[] colliders = Physics.OverlapSphere(_transform.position, GuardBT.fovRange, _enemyLayerMask);

                if (colliders.Length > 0)
                {
                    // 根目录 root 结点 比 它 高两层
                    parent.parent.SetData("target", colliders[0].transform);
                    // _animator.SetBool("Walking",true); 
                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position, GuardBT.fovRange);
        }
    }
}
