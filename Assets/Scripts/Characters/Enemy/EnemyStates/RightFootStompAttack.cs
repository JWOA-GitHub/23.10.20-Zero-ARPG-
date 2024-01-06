using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/RightFootStompAttack", fileName = "EnemyState_RightFootStompAttack")]
    public class RightFootStompAttack : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isShortRangeAttacking = true;
            base.Enter();
        }
    }
}