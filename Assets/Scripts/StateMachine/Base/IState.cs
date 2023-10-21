namespace JWOAGameSystem
{
    public interface IState
    {
        public void Enter();
        public void Exit();

        /// <summary>
        /// 处理输入逻辑
        /// </summary>
        public void HandleInput();

        /// <summary>
        /// 运行非物理相关的逻辑,相当于Update
        /// </summary>
        public void LogicUpdate();

        /// <summary>
        /// 运行物理相关的逻辑
        /// </summary>
        public void PhysicsUpdate();
    }
}