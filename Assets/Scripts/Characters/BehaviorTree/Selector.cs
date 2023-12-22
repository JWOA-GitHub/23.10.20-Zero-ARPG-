using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary> 选择器，相当于一个 “或” 逻辑门（当且仅当全部失败时返回失败 （OR
    /// </summary>
    public class Selector : Composer
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate(agent, blackboard))
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        State = NodeState.SUCCESS;
                        return State;
                    case NodeState.RUNNING:
                        State = NodeState.SUCCESS;
                        return State;
                    default:
                        continue;
                }
            }
            State = NodeState.FAILURE;
            return State;

            #region OR                  
            // bool isRunning = false;
            // bool failed = children.All((child) =>
            // {
            //     NodeState status = child.Evaluate(agent, blackboard);
            //     if (status == NodeState.RUNNING) isRunning = true;
            //     return status == NodeState.FAILURE;
            // });

            // return isRunning ? NodeState.RUNNING : failed ? NodeState.FAILURE : NodeState.SUCCESS;
            #endregion
        }

    }
}
