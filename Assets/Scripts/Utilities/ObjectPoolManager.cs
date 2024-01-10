using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static ObjectPoolManager instance;
        public static ObjectPoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ObjectPoolManager>();
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject("ObjectPoolManagerSingleton");
                        instance = singleton.AddComponent<ObjectPoolManager>();
                    }
                }
                return instance;
            }
        }
        private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
        private Transform parentTransform; // 管理所有特效的父对象
        public Transform ParentTransform
        {
            get => parentTransform;
        }

        void Awake()
        {
            // 如果存在多个实例，则销毁重复的实例
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                // DontDestroyOnLoad(gameObject); // 防止切换场景时销毁对象池管理器
            }

            // 创建一个空对象作为所有特效的父对象
            parentTransform = new GameObject("EffectParent").transform;
        }

        public void CreatePool(string tag, GameObject prefab, int poolSize)
        {
            if (!pools.ContainsKey(tag))
            {
                pools[tag] = new List<GameObject>();
                prefabs[tag] = prefab;

                for (int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(prefab, parentTransform);
                    obj.SetActive(false);
                    pools[tag].Add(obj);
                }
            }
        }

        public GameObject GetObjectFromPool(string tag, Vector3 _pos, Quaternion _rotation)
        {
            if (pools.ContainsKey(tag))
            {
                foreach (var obj in pools[tag])
                {
                    if (!obj.activeInHierarchy)
                    {
                        obj.transform.position = _pos;
                        obj.transform.rotation = _rotation;
                        obj.SetActive(true);
                        obj.transform.SetParent(parentTransform);
                        return obj;
                    }
                }

                // 如果池中没有可用对象，则创建一个新对象并添加到池中
                if (prefabs.ContainsKey(tag))
                {
                    GameObject newObj = Instantiate(prefabs[tag], _pos, _rotation);
                    newObj.transform.SetParent(parentTransform);
                    newObj.SetActive(true);
                    pools[tag].Add(newObj);
                    return newObj;
                }
                else
                {
                    Debug.LogWarning("Prefab with tag " + tag + " is not registered.");
                }
            }
            else
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        public void ReturnObjectToPool(string tag, GameObject obj)
        {
            if (pools.ContainsKey(tag))
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            }
        }
    }

}
