using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerInput : MonoBehaviour
    {

        [Tooltip("输入动作表InputActions")]public PlayerInputActions InputActions { get; private set; }
        [Tooltip("玩家输入动作表 Player")]public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

        private void Awake()
        {
            InputActions = new PlayerInputActions();

            PlayerActions = InputActions.Player;
        }

        private void OnEnable()
        {
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }

    }
}
