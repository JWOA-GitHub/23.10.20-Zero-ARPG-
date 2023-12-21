using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class TaskGoToTarget : Action
    {
        private Transform _transform;

        public TaskGoToTarget(Transform transform)
        {
            _transform = transform;
        }

        public static float speed = 2f;

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            Transform target = blackboard.Get<Transform>("target");
            Debug.Log("<color=pink> 在追踪》》？  </color>");
            if (Vector3.Distance(_transform.position, target.position) > 0.01f)
            {
                Debug.Log("<color=green> goto Target </color>");
                _transform.position = Vector3.MoveTowards(_transform.position, target.position, blackboard.Get<float>("speed") * Time.deltaTime);
                _transform.LookAt(target.position);
            }

            State = NodeState.RUNNING;
            return State;
        }
    }
}
