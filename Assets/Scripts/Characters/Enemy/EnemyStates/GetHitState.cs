using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/GetHit", fileName = "EnemyState_GetHit")]
    public class GetHitState : EnemyStates
    {
        public override void Enter()
        {
            base.Enter();
            SoundManger.Instance.PlayAudio(Globals.S_Hurting);
        }

        // public override void Exit()
        // {

        // }

        // public override void LogicUpdate()
        // {

        // }

        // public override void PhysicsUpdate()
        // {

        // }
    }
}