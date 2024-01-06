using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class EffectData
    {
        public string tag; // 特效的标签
        public GameObject prefab; // 特效的预制件
                                  // 可以添加其他特效属性
    }

    public class EffectManager : MonoBehaviour
    {
        private static EffectManager instance;
        public static EffectManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<EffectManager>();
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject("EffectManagerSingleton");
                        instance = singleton.AddComponent<EffectManager>();
                    }
                }
                return instance;
            }
        }
        // public ObjectPoolManager poolManager; // 引用对象池管理器

        public List<EffectData> effects = new List<EffectData>(); // 特效配置列表

        private void Awake()
        {
            // 如果存在多个实例，则销毁重复的实例
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // 防止切换场景时销毁对象池管理器
            }
        }

        void Start()
        {
            // 根据特效配置文件创建对象池
            foreach (EffectData effect in effects)
            {
                ObjectPoolManager.Instance.CreatePool(effect.tag, effect.prefab, 10);
            }

            // 使用特效
            // SpawnEffect("Fire", Vector3.zero);
            // SpawnEffect("Ice", new Vector3(1f, 0f, 0f));
            // 可以根据配置使用更多特效
        }

        // 公开一个静态方法，在游戏开始时手动设置 EffectManager 的实例
        public static void InitializeInstance(EffectManager newInstance)
        {
            if (instance == null)
            {
                instance = newInstance;
            }
            else
            {
                // 如果已经存在一个实例，则销毁新的实例
                Destroy(newInstance.gameObject);
            }
        }

        public void SpawnEffect(string tag, Vector3 position)
        {
            // 从对象池管理器获取特效
            GameObject effect = ObjectPoolManager.Instance.GetObjectFromPool(tag);

            // 在指定位置使用特效
            effect.transform.position = position;
            // 可以设置其他特效属性
        }
    }

}
