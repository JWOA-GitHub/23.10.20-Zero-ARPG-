using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public enum NodeState
    {
        FAILURE = 0,
        SUCCESS,
        RUNNING
    }
    public abstract class Node
    {
        private NodeState state;

        public NodeState State
        {
            get => state;
            protected set => state = value;
        }

        protected Node parent;
        // protected List<Node> children = new List<Node>();
        protected List<Node> children = new();

        // private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        /// <summary> 连接子结点
        /// </summary>
        /// <param name="node">子结点</param>
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }


        /// <summary> 判断当前结点状态（默认为FAILURE）
        /// </summary>
        /// <returns></returns>
        // public virtual NodeState Evaluate() => NodeState.FAILURE;
        public NodeState Evaluate(Transform agent, Blackboard blackboard)
        {
            Debug.Log($"{GetType().Name} - Entered...");
            state = OnEvaluate(agent, blackboard);
            Debug.Log($"{GetType().Name} - {state}");
            Debug.Log($"{GetType().Name} - Exited...");

            return State;
        }

        protected virtual NodeState OnEvaluate(Transform agent, Blackboard blackboard) => NodeState.FAILURE;

        // #region  OLD  
        // /// <summary> 设置字典的一个键，命名变量与任何类型的字符串的映射
        // /// </summary>
        // /// <param name="key"> 命名变量key </param>
        // /// <param name="value"> 任何类型的字符串object</param>
        // public void SetData(string key, object value)
        // {
        //     _dataContext[key] = value;
        // }

        // /// <summary> 获取 该key字符串对应的 键
        // /// </summary>
        // /// <param name="key">字符串key</param>
        // /// <returns>返回字典中所对应的键</returns>
        // public object GetData(string key)
        // {
        //     object value = null;
        //     if (_dataContext.TryGetValue(key, out value))
        //         return value;

        //     Node node = parent;
        //     while (node != null)
        //     {
        //         value = node.GetData(key);
        //         if (value != null)
        //             return value;
        //         node = node.parent;
        //     }
        //     return null;
        // }

        // /// <summary> 删除对应key字符串的 键
        // /// </summary>
        // /// <param name="key">字符串key</param>
        // /// <returns>递归搜索达到根结点，判断是否清除成功，成功true，否则false</returns>
        // public bool ClearData(string key)
        // {
        //     if (_dataContext.ContainsKey(key))
        //     {
        //         _dataContext.Remove(key);
        //         return true;
        //     }

        //     Node node = parent;
        //     while (node != null)
        //     {
        //         bool cleared = node.ClearData(key);
        //         if (cleared)
        //             return true;
        //         node = node.parent;
        //     }
        //     return false;
        // }

        // #endregion

    }
}
