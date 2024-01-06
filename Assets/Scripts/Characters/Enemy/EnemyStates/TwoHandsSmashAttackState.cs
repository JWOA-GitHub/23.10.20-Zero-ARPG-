using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/TwoHandsSmashAttack", fileName = "EnemyState_TwoHandsSmashAttack")]
    public class TwoHandsSmashAttackState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isLongRangeAttacking = true;
            base.Enter();

            SetAnimationMoveBase(enemyStateMachine.transform.forward, 1);
        }
    }
}