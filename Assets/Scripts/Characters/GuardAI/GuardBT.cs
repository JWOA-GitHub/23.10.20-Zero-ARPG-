using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace JWOAGameSystem
{
    public class GuardBT : BehaviorTree
    {
        [SerializeField] private Transform[] waypoints;

        [SerializeField] private float speed = 2f;
        [SerializeField] private float fovRange = 6f;  // 6f
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private LayerMask enemyLayerMask;

        protected override Node OnSetupTree()
        {
            // Debug.Log((int)enemyLayerMask);
            Blackboard.Add<float>("speed", speed);
            Blackboard.Add<float>("fovRange", fovRange);
            Blackboard.Add<float>("attackRange", attackRange);
            Blackboard.Add<int>("attackDamage", attackDamage);
            Blackboard.Add<int>("enemyLayerMask", enemyLayerMask);

            Node Root = new Selector(new List<Node>{
                new CheckHurt(transform),
                new Sequence(new List<Node>{
                    new CheckEnemyInAttackRange(transform),
                    new TaskAttack(transform),
                }),
                new Sequence(new List<Node>{
                    new CheckEnemyInFOVRange(transform),
                    new TaskGoToTarget(transform),
                }),
                new TaskPatrol(transform, waypoints),
            });
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
