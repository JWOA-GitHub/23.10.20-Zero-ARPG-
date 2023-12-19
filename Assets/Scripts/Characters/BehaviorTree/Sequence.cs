using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>
    /// 序列是一个复合结点，只有所有结点成功时才像结束逻辑门一样工作，相当于一个“与”逻辑门(当且仅当全部成功时返回成功，碰到一个失败的结点则返回，不判断后面的结点)
    /// </summary>
    public class Sequence : Composer
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        /// <summary> 遍历孩子结点并检查评估后的状态，如果任何孩子失败，我们可以在那里停下来并返回一个失败状态，否则我们需要继续处理子结点 并最终检查是否有一些正在运行，因此也会阻止我们处于运行状态，或者如果所有结点都成功了，
        /// </summary>
        /// <returns></returns>
        protected override NodeState OnEvaluate(Transform agent, Blackboard blackboard)
        {
            // bool anyChildIsRuning = false;

            // foreach (Node node in children)
            // {
            //     switch (node.Evaluate(agent, blackboard))
            //     {
            //         case NodeState.FAILURE:
            //             State = NodeState.FAILURE;
            //             return State;
            //         case NodeState.SUCCESS:
            //             continue;
            //         case NodeState.RUNNING:
            //             anyChildIsRuning = true;
            //             continue;
            //         default:
            //             State = NodeState.SUCCESS;
            //             return State;
            //     }
            // }
            // State = anyChildIsRuning ? NodeState.RUNNING : NodeState.SUCCESS;
            // return State;

            #region  OR
            bool isRunning = false;
            bool success = children.All((child) =>
            {
                State = child.Evaluate(agent, blackboard);
                switch (State)
                {
                    case NodeState.FAILURE:
                        return false;
                    case NodeState.RUNNING:
                        isRunning = true;
                        break;
                }
                return State == NodeState.SUCCESS;
            });

            return isRunning ? NodeState.RUNNING : success ? NodeState.SUCCESS : NodeState.FAILURE;
            #endregion
        }

    }
}
