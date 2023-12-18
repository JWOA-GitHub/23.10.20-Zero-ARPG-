using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{   
    /// <summary>
    /// 序列是一个复合结点，只有所有结点成功时才像结束逻辑门一样工作，相当于一个“与”逻辑门(当且仅当全部成功时返回成功，碰到一个失败的结点则返回，不判断后面的结点)
    /// </summary>
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        /// <summary> 遍历孩子结点并检查评估后的状态，如果任何孩子失败，我们可以在那里停下来并返回一个失败状态，否则我们需要继续处理子结点 并最终检查是否有一些正在运行，因此也会阻止我们处于运行状态，或者如果所有结点都成功了，
        /// </summary>
        /// <returns></returns>
        public override NodeState Evaluate()
        {
            bool anyChildIsRuning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRuning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRuning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }
}
