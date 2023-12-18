using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class TaskGoToTarget : Node
    {
        private Transform _transform;

        public TaskGoToTarget(Transform transform)
        {
            _transform = transform;
        }

        public static float speed = 2f;

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            if (Vector3.Distance(_transform.position, target.position) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(target.position);
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}
