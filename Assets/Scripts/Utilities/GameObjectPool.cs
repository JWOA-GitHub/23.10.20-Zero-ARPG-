using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary> 可重置的接口 （调用如在生成对象时需要更新发射目标点等时  继承该接口
    /// </summary>
    public interface IResetable
    {
        void OnReset();
    }

    /// <summary>
    /// 使用方式：
    ///     1.所有频繁创建/销毁的物体，都通过对象池创建/回收。
    ///         GameObjectPool.Instance.CreateObject("类别", "预制件", "位置", "旋转")
    ///         GameObjectPool.Instance.CollectObject(游戏对象);
    ///     2.需要通过对象池创建的物体，如需每次创建时执行，则让脚本实现IResetable接口。
    /// </summary>
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
                go = AddObject(key, prefab);    // 创建物体
            }

            // 使用对象
            UseObject(pos, rotate, go);     // 设置位置/旋转
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

            // 设置目标点
            // MARKER： 抽象！！！
            // go.GetComponent<IResetable>().OnReset();
            // 遍历执行物体中所有需要重置的逻辑
            foreach (var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }

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


        /// <summary> 清空某个类别的对象
        /// </summary>
        /// <param name="key">类别</param>
        public void Clear(string key)
        {
            #region JWOA
            // JWOA
            // while (cache.ContainsKey(key))
            //     Destroy(FindUsableObject(key));
            #endregion

            // Destroy(游戏对象);
            // cache[key] --> List<GameObject>()

            // for (int i = 0; i < cache[key].Count; i++)
            // {
            //     Destroy(cache[key][i]);
            // }

            if (!cache.ContainsKey(key)) return;

            // MARKER： 因为List中删除一个对象后 后面的会补全前面的 建议最好是倒序删除！！   不过由于这里删除的是Instantiate 的对象 而非直接删除List中的对象！！ 因为倒序正序都可以！
            for (int i = cache[key].Count - 1; i >= 0; i--)
            {
                Destroy(cache[key][i]);
            }

            // foreach (var item in cache[key])
            // {
            //     Destroy(item);
            // }

            // 类别
            cache.Remove(key);
        }


        /// <summary> 清空全部
        /// </summary>
        public void ClearAll()
        {
            // 遍历 字典 集合  （ 只读元素）
            // foreach (var key in cache.Keys) // 异常：无效的记录
            // {
            //     Clear(key); // 删除字典记录  cache.Remove(key);
            // }

            // 将字典中所有键 存入List集合中
            List<string> keyList = new List<string>(cache.Keys);
            // 遍历 List 集合
            foreach (var key in keyList)
            {
                Clear(key);
            }
        }
    }

}
