using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [CreateAssetMenu(fileName = "EnemyStateData_", menuName = "Custom/Characters/EnemyStateData", order = 1)]
    public class EnemyStateData : ScriptableObject
    {
        public List<string> states = new List<string>(); // 存储状态名称
        public string currentState; // 存储当前状态

        public bool ContainsState(string state)
        {
            return states.Contains(state);
        }

        public void SetCurrentState(string state)
        {
            if (ContainsState(state))
            {
                currentState = state;
            }
            else
            {
                Debug.LogWarning("State not found: " + state);
            }
        }

        public string GetCurrentState()
        {
            return currentState;
        }
    }
}

