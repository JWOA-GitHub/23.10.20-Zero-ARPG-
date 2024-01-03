using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class EnemyAI : CharactersBase
    {
        public EnemyAI(string name, int startingLevel) : base(name, startingLevel)
        {
            name = "Boss";
            startingLevel = 100;
            Debug.LogError("Boss:   " + gameObject.name);
        }


    }
}
