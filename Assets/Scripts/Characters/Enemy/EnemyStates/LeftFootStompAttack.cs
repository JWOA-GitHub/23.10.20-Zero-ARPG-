using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/LeftFootStompAttack", fileName = "EnemyState_LeftFootStompAttack")]
    public class LeftFootStompAttack : EnemyStates
    {
        public override void Enter()
        {
            EnemyAI.isAttacking = true;
            EnemyAI.isShortRangeAttacking = true;
            base.Enter();
        }
    }
}