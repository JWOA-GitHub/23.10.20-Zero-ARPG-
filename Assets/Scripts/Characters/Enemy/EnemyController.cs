using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyController : CharactersBase
    {
        [SerializeField] private float hideTime = 3f;
        [SerializeField] private int exp = 50;

        //// 定义一个委托类型，用于定义怪物死亡时的事件
        //public delegate void EnemyDeathAction(int experienceValue);
        //// 定义一个事件，当怪物死亡时触发
        //public static event EnemyDeathAction OnEnemyDeath;
        public Player player;

        private bool hasGivenExperience = false;

        public EnemyController(string name, int startingLevel) : base(name, startingLevel)
        {
            CharacterName = "Enemy";
            Level = 1;
        }

        private void Start()
        {
           player = FindAnyObjectByType<Player>();
        }


        protected override void Dead()
        {
            Invoke(nameof(HideGameObject), hideTime);

            // 触发怪物死亡事件，并传递经验值参数
            if (!hasGivenExperience  && player != null)
            {
                Debug.Log(" 怪物"+name+"经验"+exp);   
                player.GainExp(exp);
                hasGivenExperience = true;
    }
        }

        private void HideGameObject()
        {
            gameObject.SetActive(false);
        }


        protected new void Update()
        {
            base.Update();
        }


    }
}
