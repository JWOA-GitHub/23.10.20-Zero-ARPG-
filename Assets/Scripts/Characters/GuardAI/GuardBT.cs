using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class GuardBT : BehaviorTree
    {
        [SerializeField] private Transform[] waypoints;

        [SerializeField] private float speed = 2f;
        private float fovRange = 3f;  // 6f
        private float attackRange = 1f;
        private int attackPower = 10;

        protected override Node OnSetupTree()
        {
            // Blackboard.Set<float>("speed", speed);
            Debug.Log("setup");
            Node Root = new TaskPatrol(transform, waypoints);
            return Root;

            #region OLD
            // Node root = new TaskPatrol(transform, waypoints);

            // Node root = new Selector(new List<Node>
            // {
            //     new Sequence(new List<Node>
            //     {
            //         new CheckEnemyInAttackRange(transform),
            //         new TaskAttack(transform),
            //     }),
            //     new Sequence(new List<Node>
            //     {
            //         new CheckEnemyInFOVRange(transform),
            //         new TaskGoToTarget(transform),
            //     }),
            //     new TaskPatrol(transform,waypoints),
            // });

            // return root;

            #endregion
        }
    }
}
