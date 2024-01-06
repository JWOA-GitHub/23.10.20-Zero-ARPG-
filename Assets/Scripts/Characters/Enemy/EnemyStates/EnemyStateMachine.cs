using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyStateMachine : EnemyStateMachineBase
    {
        [SerializeField] public EnemyStates[] enemyStates;
        private Animator _animator;
        public Animator Animator
        {
            get => _animator;
        }


        CharactersBase _charactersBase;
        public CharactersBase CharactersBase
        {
            get => _charactersBase;
        }

        CharacterController _characterController;
        public CharacterController CharacterController
        {
            get => _characterController;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _charactersBase = GetComponent<CharactersBase>();
            _characterController = GetComponent<CharacterController>();

            // enemyAI = GetComponent<EnemyAI>();

            stateTable = new Dictionary<System.Type, IEnemyState>(enemyStates.Length);

            foreach (EnemyStates enemyState in enemyStates)
            {
                enemyState.Initialize(_animator, this, CharactersBase);
                stateTable.Add(enemyState.GetType(), enemyState);

                // stateTypeTable.Add(enemyState, enemyState.GetType());
            }
        }

        private void Start()
        {
            InitState(stateTable[typeof(IdleState)]);
        }


    }
}