using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerMovementState : IState
    {
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }
        public virtual void Exit()
        {

        }


        public virtual void HandleInput()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }



        public virtual void PhysicsUpdate()
        {
            
        }
    }
}
