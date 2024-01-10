using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyAI : CharactersBase
    {
        public EnemyStateMachine enemyStateMachine;
        [SerializeField] private CharacterController _character;
        CharactersBase charactersBase;

        public static bool isAttacking = false;
        public static bool isShortRangeAttacking = false;
        public static bool isLongRangeAttacking = false;
        // public static bool _isWaitingForAnimation = false;


        public EnemyAI(string name, int startingLevel) : base(name, startingLevel)
        {
            name = "Boss";
            startingLevel = 100;
            Debug.LogError("Boss:   " + gameObject.name);
        }

        private void Start()
        {
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            charactersBase = GetComponent<CharactersBase>();
        }

        private new void Update()
        {

        }


    }
}
