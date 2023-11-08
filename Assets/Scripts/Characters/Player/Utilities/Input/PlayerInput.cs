using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JWOAGameSystem
{
    public class PlayerInput : MonoBehaviour
    {

        [Tooltip("输入动作表InputActions")] public PlayerInputActions InputActions { get; private set; }
        [Tooltip("玩家输入动作表 Player")] public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

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

        /// <summary> 禁用按某操作/按键持续几秒钟
        /// </summary>
        /// <param name="action">禁用的按键</param>
        /// <param name="seconds">禁用的时间（秒）</param>
        public void DisableActionFor(InputAction action, float seconds)
        {
            StartCoroutine(DisableAction(action, seconds));
        }

        /// <summary> 禁用操作几秒后恢复
        /// </summary>
        /// <param name="action">禁用的按键</param>
        /// <param name="seconds">禁用的时间（秒）</param>
        /// <returns></returns>
        private IEnumerator DisableAction(InputAction action, float seconds)
        {
            action.Disable();

            yield return new WaitForSeconds(seconds);

            action.Enable();
        }


    }
}
