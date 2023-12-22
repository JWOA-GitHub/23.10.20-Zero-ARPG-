using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyController : CharactersBase
    {
        public EnemyController(string name, int startingLevel) : base(name, startingLevel)
        {
            CharacterName = "Enemy";
            Level = 1;
        }


    }
}
