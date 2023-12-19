using JetBrains.Annotations;
using UnityEngine;

namespace JWOAGameSystem
{
    [RequireComponent(typeof(Blackboard))]
    public abstract class BehaviorTree : MonoBehaviour
    {
        private Node _root = null;
        public Node Root
        {
            get => _root;
            protected set => value = _root;
        }

        private Blackboard _blackboard;
        public Blackboard Blackboard
        {
            get => _blackboard;
            set => value = _blackboard;
        }

        private void Start()
        {
            // _root = SetupTree();

            _blackboard = GetComponent<Blackboard>();

            _root = OnSetupTree();
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate(gameObject.transform, _blackboard);
            // _root?.Evaluate(gameObject.transform, _blackboard);
        }

        // protected abstract Node SetupTree();
        protected abstract Node OnSetupTree();
    }
}
