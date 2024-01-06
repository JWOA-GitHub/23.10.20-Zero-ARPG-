using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/LeftHandAttack", fileName = "EnemyState_LeftHandAttack")]
    public class LeftHandAttackState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isShortRangeAttacking = true;
            base.Enter();
        }

    }
}