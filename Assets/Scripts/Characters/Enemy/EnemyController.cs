using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyController : CharactersBase
    {
        [SerializeField] private float hideTime = 3f;
        public EnemyController(string name, int startingLevel) : base(name, startingLevel)
        {
            CharacterName = "Enemy";
            Level = 1;
        }

        protected override void Dead()
        {
            Invoke(nameof(HideGameObject), hideTime);
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
