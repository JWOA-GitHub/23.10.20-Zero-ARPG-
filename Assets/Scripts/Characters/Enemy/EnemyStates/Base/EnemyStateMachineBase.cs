using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyStateMachineBase : MonoBehaviour
    {
        private IEnemyState currentState;
        protected Dictionary<System.Type, IEnemyState> stateTable;

        #region StateMachine Methods
        private void Update()
        {
            currentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            currentState.PhysicsUpdate();
        }

        protected void InitState(IEnemyState enemyState)
        {
            currentState = enemyState;
            currentState.Enter();
        }

        protected void SetState(IEnemyState enemyState)
        {
            currentState = enemyState;
            currentState.Enter();
        }

        protected void ChangeState(IEnemyState newEnemyState)
        {
            currentState.Exit();
            SetState(newEnemyState);
        }

        // 重载为当前状态参数！（字典：<System.Type, IState>
        public void ChangeState(System.Type newStateType)
        {
            ChangeState(stateTable[newStateType]);
        }
        #endregion
    }
}