using System;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyStateMachineBase : MonoBehaviour
    {
        private IEnemyState currentState;
        public IEnemyState CurrentState
        {
            get => currentState;
        }
        protected Dictionary<System.Type, IEnemyState> stateTable;
        public Dictionary<System.Type, IEnemyState> StateTable
        {
            get => stateTable;
        }
        // protected Dictionary<IEnemyState, System.Type> stateTypeTable;
        // public Dictionary<IEnemyState, System.Type> StateTypeTable
        // {
        //     get => stateTypeTable;
        // }

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

        public EnemyStates GetEnemyState(System.Type stateType)
        {
            if (stateTable.ContainsKey(stateType))
            {
                IEnemyState state = stateTable[stateType];

                if (state is EnemyStates enemyStates)
                    return enemyStates;
                else
                    // 如果无法转换为 EnemyStates 类型，可能需要处理异常情况
                    throw new InvalidCastException($"Unable to cast state of type {state.GetType()} to EnemyStates.");
            }

            throw new KeyNotFoundException($"State of type {stateType} not found in the dictionary.");
        }

        public EnemyStates GetEnemeyStatesFromIEnemyState(IEnemyState iEnemyState)
        {
            if (currentState is EnemyStates enemyStates)
                // 如果能转换为 EnemyStates 类型，就返回对应的对象
                return enemyStates;
            else
            {
                // 如果无法转换为 EnemyStates 类型，可能需要处理异常情况
                throw new InvalidCastException($"Unable to cast state of type {iEnemyState.GetType()} to EnemyStates.");
            }
        }

        public IEnemyState GetCurrentState()
        {
            return CurrentState;
        }

        protected void SetState(IEnemyState enemyState)
        {
            currentState = enemyState;
            currentState.Enter();
        }

        public void ChangeState(IEnemyState newEnemyState)
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