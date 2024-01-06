using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/TwoHitComboAttack", fileName = "EnemyState_TwoHitComboAttack")]
    public class TwoHitComboAttackState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isTwoHitComboAttacking = true;
            base.Enter();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // if (IsAnimationFinished)
            // {
            //     EnemyAI.isAttacking = false;
            //     EnemyAI.isTwoHitComboAttacking = false;
            // }
        }
    }
}