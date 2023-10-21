namespace JWOAGameSystem
{
    public interface IState
    {
        void Enter();
        void Exit();

        /// <summary>
        /// 处理输入逻辑
        /// </summary>
        void HandleInput();

        /// <summary>
        /// 运行非物理相关的逻辑,相当于Update
        /// </summary>
        void LogicUpdate();

        /// <summary>
        /// 运行物理相关的逻辑
        /// </summary>
        void PhysicsUpdate();
    }
}