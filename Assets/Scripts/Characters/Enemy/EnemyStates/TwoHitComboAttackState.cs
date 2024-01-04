using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/TwoHitComboAttack", fileName = "EnemyState_TwoHitComboAttack")]
    public class TwoHitComboAttackState : EnemyStates
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsAnimationFinished)
            {
                enemyStateMachine.EnemyAI.isTwoHitComboAttacking = false;
            }
        }
    }
}