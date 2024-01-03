using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyStateMachine : EnemyStateMachineBase
    {
        [SerializeField] private EnemyStates[] enemyStates;
        Animator animator;
        CharactersBase charactersBase;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            charactersBase = GetComponentInChildren<CharactersBase>();

            stateTable = new Dictionary<System.Type, IEnemyState>(enemyStates.Length);
            foreach (EnemyStates enemyState in enemyStates)
            {
                enemyState.Initialize(animator, this, charactersBase);
                stateTable.Add(enemyState.GetType(), enemyState);
                Debug.Log(enemyState.GetType().Name);
            }
        }

        private void Start()
        {
            InitState(stateTable[typeof(IdleState)]);
        }
    }
}