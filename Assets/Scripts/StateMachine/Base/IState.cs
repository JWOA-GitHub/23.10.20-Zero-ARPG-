namespace JWOAGameSystem
{
    public interface IState
    {
        void Enter();
        void Exit();

        /// <summary> 处理输入逻辑
        /// </summary>
        void HandleInput();

        /// <summary> 运行非物理相关的逻辑,相当于Update
        /// </summary>
        void LogicUpdate();

        /// <summary> 运行物理相关的逻辑,相当于FixedUpdate 
        /// </summary>
        void PhysicsUpdate();

        /// <summary> 进入动画事件第一帧所触发的事件，可用于“当动画进入第一帧时使玩家免受伤害”之类的
        /// </summary>
        public void OnAnimationEnterEvent();
        /// <summary> 退出动画事件最后一帧所触发的事件，可用于“当动画进入最后一帧时使玩家再次容易受到伤害”之类的
        /// </summary>
        public void OnAnimationExitEvent();
        /// <summary> 当动画进入到某一帧时的事件，可用于“进入某一帧则转换到其他状态”等 如Dash状态后若没有输入移动 会进入僵直“硬停止”状态
        /// </summary>
        public void OnAnimationTransitionEvent();
    }
}