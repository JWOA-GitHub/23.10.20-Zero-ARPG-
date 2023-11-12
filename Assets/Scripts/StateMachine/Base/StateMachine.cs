using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public abstract class StateMachine
    {
        protected IState currentState;
        public void InitState(IState state)
        {
            currentState = state;

            currentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            currentState.Exit();

            currentState = newState;

            currentState.Enter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }

        public void LogicUpdate()
        {
            currentState.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            currentState.PhysicsUpdate();
        }

        public void OnAnimationEnterEvent()
        {
            currentState.OnAnimationEnterEvent();
        }


        /// <summary> 当动画进入到某一帧时的事件，可用于“进入某一帧则转换到其他状态”等 如Dash状态后若没有输入移动 会进入僵直“硬停止”状态
        /// </summary>
        public void OnAnimationExitEvent()
        {
            currentState.OnAnimationExitEvent();
        }

        /// <summary> 当动画进入到某一帧时的事件，可用于“进入某一帧则转换到其他状态”等 如Dash状态后若没有输入移动 会进入僵直“硬停止”状态
        /// </summary>
        public void OnAnimationTransitionEvent()
        {
            currentState.OnAnimationTransitionEvent();
        }

        public void OnTriggerEnter(Collider collider)
        {
            currentState.OnTriggerEnter(collider);
        }
    }
}
