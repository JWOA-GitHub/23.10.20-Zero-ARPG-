using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/WalkForward", fileName = "EnemyState_WalkForward")]
    public class WalkForwardState : EnemyStates
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("     Walk  ENter");
        }

        // public override void Exit()
        // {

        // }

        // public override void LogicUpdate()
        // {

        // }

        public override void PhysicsUpdate()
        {
            Debug.Log("     Walk  update");
        }
    }
}