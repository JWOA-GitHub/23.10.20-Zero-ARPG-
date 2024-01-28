using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        /// <summary> 对象池cache
        /// </summary>
        private Dictionary<string, List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>   通过对象池 创建对象
        /// </summary>
        /// <param name="key">类别</param>
        /// <param name="prefab">需要创建实例的预制件</param>
        /// <param name="pos">位置</param>
        /// <param name="rotate">旋转</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            GameObject go = FindUsableObject(key);

            if (go == null)
            {
                go = AddObject(key, prefab);
            }

            // 使用对象
            UseObject(pos, rotate, go);
            return go;
        }

        /// <summary>  使用对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rotate"></param>
        /// <param name="go"></param>
        private void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);
        }

        /// <summary> 添加对象到池中
        /// </summary>
        /// <param name="key">对象类型的key</param>
        /// <param name="prefab">对象预制件</param>
        /// <returns></returns>
        private GameObject AddObject(string key, GameObject prefab)
        {
            // 创建对象
            GameObject go = Instantiate(prefab);

            // 加入池中
            // 如果池中没有key 则添加记录 有key则直接增加一个
            if (!cache.ContainsKey(key))
                cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        /// <summary>   查找制定类别中 可以使用的对象
        /// </summary>
        /// <param name="key">指定类别 key 的名字</param>
        /// <returns></returns>
        private GameObject FindUsableObject(string key)
        {
            // if (cache.ContainsKey(key))
            //     go = cache[key].Find(g => !g.activeInHierarchy);
            // return go;
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;
        }

        /// <summary> 回收对象（可延迟回收，默认为0）
        /// </summary>
        /// <param name="go">需要被回收的游戏对象</param>
        /// <param name="delay">延迟回收时间，默认为0</param>
        public void CollectObject(GameObject go, float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go, delay));
        }

        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }
    }

}
