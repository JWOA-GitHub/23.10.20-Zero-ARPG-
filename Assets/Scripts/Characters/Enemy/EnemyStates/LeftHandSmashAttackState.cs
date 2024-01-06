using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/LeftHandSmashAttack", fileName = "EnemyState_LeftHandSmashAttack")]
    public class LeftHandSmashAttackState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isLongRangeAttacking = true;
            base.Enter();

            SetAnimationMoveBase(enemyStateMachine.transform.forward, 1);
        }

        // public override void PhysicsUpdate()
        // {
        //     base.PhysicsUpdate();
        // }

    }
}