using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerNormalAttack : PlayerAttackState
    {
        public PlayerNormalAttack(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

        }
        #endregion

        #region Main Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            // stateMachine.Player.Input.PlayerActions.Attack.Started += OnAttackCombo();
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
        }
        #endregion

        #region Input Methods
        private void OnAttackCombo(InputAction context)
        {

        }
        #endregion
    }
}
