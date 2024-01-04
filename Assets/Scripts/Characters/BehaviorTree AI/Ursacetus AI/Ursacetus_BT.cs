using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class Ursacetus_BT : BehaviorTree
    {
        [SerializeField] private Transform[] waypoints;

        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float chaseSpeed = 3f;
        [SerializeField] private float fovRange = 6f;  // 6f
        [SerializeField] private float shortAttackRange = 5f;
        [SerializeField] private float longAttackRange = 12f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private LayerMask enemyLayerMask;

        protected override Node OnSetupTree()
        {
            Blackboard.Add<float>("speed", speed);
            Blackboard.Add<float>("chaseSpeed", chaseSpeed);
            Blackboard.Add<float>("fovRange", fovRange);
            Blackboard.Add<float>("shortAttackRange", shortAttackRange);
            Blackboard.Add<float>("longAttackRange", longAttackRange);
            Blackboard.Add<int>("attackDamage", attackDamage);
            Blackboard.Add<int>("enemyLayerMask", enemyLayerMask);

            Root = new Selector(new List<Node>{
                // new CheckDeath(transform),
                // new CheckHurt(transform),
                new Sequence(new List<Node>{
                    new CheckEnemyInShortAttackRange_State(transform),
                    new TaskShortRangeAttack_State(transform),
                }),
                new Sequence(new List<Node>{
                    new CheckEnemyInFOVRange_State(transform),
                    // new TaskGoToTarget_State(transform),
                }),
                new TaskPatrol_State(transform, waypoints),
            });
            return Root;
        }
    }
}
