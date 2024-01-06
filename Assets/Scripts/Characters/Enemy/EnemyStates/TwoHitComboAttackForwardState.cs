using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/TwoHitComboAttackForward", fileName = "EnemyState_TwoHitComboAttackForward")]
    public class TwoHitComboAttackForwardState : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isLongRangeAttacking = true;
            base.Enter();
        }
    }
}