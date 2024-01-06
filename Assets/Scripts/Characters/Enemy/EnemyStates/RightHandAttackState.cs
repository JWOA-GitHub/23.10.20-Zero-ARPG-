using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/RightHandAttack", fileName = "EnemyState_RightHandAttack")]
    public class RightHandAttackState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isShortRangeAttacking = true;
            base.Enter();
        }
    }
}