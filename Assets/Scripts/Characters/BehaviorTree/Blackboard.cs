using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class Blackboard : MonoBehaviour
    {
        private Dictionary<string, object> _dataContext = new();

        public T Get<T>(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value)) return (T)value;
            return default;
        }

        public void Add<T>(string key, T value)
        {
            _dataContext[key] = value;
        }

        public bool Remove(string key)
        {
            bool hasKey = _dataContext.ContainsKey(key);
            if (hasKey) _dataContext.Remove(key);
            return hasKey;
        }
    }
}