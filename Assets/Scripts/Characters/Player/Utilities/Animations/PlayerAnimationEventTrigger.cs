using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            player = transform.parent.GetComponent<Player>();
        }

        public void TriggerOnMovementStateAnimationEnterEvent()
        {
            // 处于“过渡”过程中时，仍然可以调用“动画事件”,因此判断当前“状态”与“动画”是否同一个
            if (IsInAnimationTransition())
            {
                return;
            }

            player.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnMovementStateAnimationExitEvent()
        {
            player.OnMovementStateAnimationExitEvent();
        }

        public void TriggerOnMovementStateAnimationTranstionEvent()
        {
            player.OnMovementStateAnimationTransitionEvent();
        }

        /// <summary> 判断当前播放的“动画”是否在当前“状态”所在的“层”的索引，防止“过渡”动画尚未播放完，代码已经执行到下一个“状态”了
        /// </summary>
        /// <param name="layerIndex">当前动画控制器的“层”的索引是否与所在“状态”相同</param>
        /// <returns></returns>
        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return player.Animator.IsInTransition(layerIndex);
        }
    }
}
