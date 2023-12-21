using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CheckEnemyInFOVRange : Condition
    {
        private static int _enemyLayerMask = 1 << 9;

        private Transform _transform;
        private Animator _animator;

        public CheckEnemyInFOVRange(Transform transform)
        {
            _transform = transform;
            // _animator = transform.GetComponent<Animator>();
        }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            object t = blackboard.Get<Transform>("target");
            Debug.Log("<color=red>检查巡逻范围 CheckEnemyInFOVRange       t.GetType().Name</color>");
            if (t == null)
            {
                Debug.Log("<color=yellow> 检查范围中 搜索target  </color>" + blackboard.Get<float>("fovRange"));
                Collider[] colliders = Physics.OverlapSphere(_transform.position, blackboard.Get<float>("fovRange"), _enemyLayerMask);

                if (colliders.Length > 0)
                {
                    // 根目录 root 结点 比 它 高两层
                    Debug.Log("             碰撞到了        " + parent);
                    // parent.parent.SetData("target", colliders[0].transform);
                    blackboard.Add<Transform>("target", colliders[0].transform);
                    // _animator.SetBool("Walking",true); 
                    State = NodeState.SUCCESS;
                    return State;
                }

                State = NodeState.FAILURE;
                return State;
            }

            State = NodeState.SUCCESS;
            return State;
        }
    }
}
