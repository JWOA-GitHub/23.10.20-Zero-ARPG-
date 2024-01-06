using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/RightHandSmashAttack", fileName = "EnemyState_RightHandSmashAttack")]
    public class RightHandSmashAttackState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isLongRangeAttacking = true;
            base.Enter();
        }

    }
}