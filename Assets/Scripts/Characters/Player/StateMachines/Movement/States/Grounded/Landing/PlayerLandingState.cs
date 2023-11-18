using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }


        #region Input Methods
        // protected override void OnMovementCanceled(InputAction.CallbackContext context)
        // {
        //     // 只想在动画结束时过渡到“Idle”状态
        //     // base.OnMovementCanceled(context);
        // }
        #endregion
    }
}
