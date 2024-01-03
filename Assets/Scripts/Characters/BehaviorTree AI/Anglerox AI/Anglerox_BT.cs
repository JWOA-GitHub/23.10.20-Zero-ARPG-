using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class Anglerox_BT : BehaviorTree
    {
        [SerializeField] private Transform[] waypoints;

        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float chaseSpeed = 3f;
        [SerializeField] private float fovRange = 6f;  // 6f
        [SerializeField] private float shortAttackRange = 4f;
        [SerializeField] private float longAttackRange = 6f;

        [SerializeField, Tooltip("靠太近需要后撤的距离")] private float retreatRange = 1.8f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private LayerMask enemyLayerMask;

        protected override Node OnSetupTree()
        {
            // Debug.Log((int)enemyLayerMask);
            Blackboard.Add<float>("speed", speed);
            Blackboard.Add<float>("chaseSpeed", chaseSpeed);
            Blackboard.Add<float>("fovRange", fovRange);
            Blackboard.Add<float>("shortAttackRange", shortAttackRange);
            Blackboard.Add<float>("longAttackRange", longAttackRange);
            Blackboard.Add<int>("attackDamage", attackDamage);
            Blackboard.Add<int>("enemyLayerMask", enemyLayerMask);
            Blackboard.Add<float>("retreatRange", retreatRange);

            Root = new Selector(new List<Node>{
                new CheckDeath(transform),
                new CheckHurt(transform),
                new Selector(new List<Node>{
                    // new CheckDistance(transform),
                    new TaskRetreat(transform),
                    new Sequence(new List<Node>{
                        new CheckEnemyInShortAttackRange(transform),
                        new TaskAttack(transform),
                    }),
                }),

                new Sequence(new List<Node>{
                    new CheckEnemyInFOVRange(transform),
                    new TaskGoToTarget(transform),
                }),
                new TaskPatrol(transform, waypoints),
            });
            return Root;
        }
    }
}
